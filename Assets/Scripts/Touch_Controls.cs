using System.Collections;
using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    [SerializeField] float _UpwardForce, _dashTimer , _doubleSwipeTimer , _duckTimer , _dashDuration , _leftSwipeTimer , _speedDecrease__Speed , _waitTimerForSpeedDecreaser;
    [SerializeField] LayerMask _whatIsGrounded;
    [SerializeField] BoxCollider groundColl;
    
    bool _isgrounded , _isUndergrounded;
    int _doubleJumpCounter, dashCounter , _speedDecreaseCounter , duckCounter;
    float _jumpForce,_range , _moveSpeed;
    float _nextDashTimer , _nextDuckTimer , _nextspeedDecreaseTimer;
    Rigidbody rb;
    PlayerMovement playerMove;
    Animator _playerAnim;
    Vector2 startTouchPos , endTouchPos;
    RaycastHit hit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMovement>();
        _playerAnim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        _jumpForce = _UpwardForce;
        _range = transform.localScale.y;
        duckCounter = 0;
        _isUndergrounded = false;
        _moveSpeed = playerMove._movespeed;
    }

    private void Update()
    {
        Raycast();
        Touch();

        if (_isgrounded)
        {
            _doubleJumpCounter = 0;
        }

        if (dashCounter == 2)
        {
            StartCoroutine(DashTimer());
            dashCounter = 0;
        }

        if(_speedDecreaseCounter == 2)
        {
            StartCoroutine(SpeedDecreaseTimer());
            _speedDecreaseCounter = 0;
        }

        // this is for dash restore timer
        if (Time.time > _nextDashTimer)
        {
            dashCounter = 0;
            _nextDashTimer = Time.time + _dashTimer;
        }

        // this is for duck timer restore.
        if (Time.time > _nextDuckTimer)
        {
            duckCounter = 0;
            _nextDuckTimer = Time.time + _duckTimer;
        }
        if (Time.time > _nextspeedDecreaseTimer)
        {
            _speedDecreaseCounter = 0;
            _nextspeedDecreaseTimer = Time.time + _leftSwipeTimer;
        }
    }

    // It can handle all the touch control.
    void Touch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;
        }
        if (Mathf.Abs(endTouchPos.x - startTouchPos.x) > Mathf.Abs(endTouchPos.y - startTouchPos.y))
        {
            if ((endTouchPos.x > startTouchPos.x) && startTouchPos.x != 0 && endTouchPos.x != 0)
            {
                if (dashCounter <= 1)
                {
                    dashCounter++;
                }
            }
            else if((startTouchPos.x > endTouchPos.x) && startTouchPos.x != 0 && endTouchPos.x != 0)
            {
                if (_speedDecreaseCounter <= 1)
                {
                    _speedDecreaseCounter++;
                }
                
            }
            startTouchPos = Vector2.zero;
            endTouchPos = Vector2.zero;
        }
        else if(Mathf.Abs(endTouchPos.x - startTouchPos.x) < Mathf.Abs(endTouchPos.y - startTouchPos.y))
        {

            if ((endTouchPos.y > startTouchPos.y) && startTouchPos.y != 0 && endTouchPos.y != 0)
            {
                if (_doubleJumpCounter <= 0 && !_isUndergrounded)
                {
                    _doubleJumpCounter++;

                    _playerAnim.SetTrigger("Jump");
                    rb.AddForce(Vector3.up * _jumpForce, ForceMode.Force);
                }
                else if (_isUndergrounded)
                {
                    _playerAnim.SetTrigger("IsJumpAttack");
                    transform.position = new Vector3(transform.position.x, 0, 0);
                    transform.GetChild(0).eulerAngles = new Vector3(0, 90, 0);
                    _isUndergrounded = false;
                }
                startTouchPos = Vector2.zero;
                endTouchPos = Vector2.zero;
            }
            else if ((endTouchPos.y < startTouchPos.y) && startTouchPos.y != 0 && endTouchPos.y != 0)
            {
                duckCounter++;
                StopCoroutine(TimerForDuck());
                StartCoroutine(TimerForDuck());
                startTouchPos = Vector2.zero;
                endTouchPos = Vector2.zero;
            }
        }

    }

    /// this will check is player grounded or not.
    void Raycast()
    {       
        if(Physics.Raycast(transform.position , Vector3.down , out hit, _range, _whatIsGrounded))
        {
            _isgrounded = true;
        }
        else
        {
            _isgrounded = false;

        }
    }

    IEnumerator TimerForDuck()
    {
        yield return new WaitForSeconds(_doubleSwipeTimer);
        if(duckCounter >= 2) 
        {
            _playerAnim.SetTrigger("IsDiving");
            Debug.Log("UnderGround");
            groundColl.enabled = false;
            transform.GetChild(0).eulerAngles = new Vector3(0, 90, 0);
            transform.position = new Vector3(transform.position.x , -3.15f , 0);
            _playerAnim.SetTrigger("IsSwimming");
            _isUndergrounded = true;
            groundColl.enabled = true;
        }
        else if(duckCounter == 1)
        {
            Debug.Log("Duck");
            GetComponent<CapsuleCollider>().height = 0.8f;
            _playerAnim.SetTrigger("IsDuck");
        }
        duckCounter = 0;
    }

    IEnumerator DashTimer()
    {
        playerMove._isDashing = true;
        yield return new WaitForSeconds(_dashDuration);
        playerMove._isDashing = false;
    }
    IEnumerator SpeedDecreaseTimer()
    {
        playerMove._movespeed = _speedDecrease__Speed;
        yield return new WaitForSeconds(_waitTimerForSpeedDecreaser);
        playerMove._movespeed = _moveSpeed;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BreakableWall"))
        {
            if (playerMove._isDashing)
            {
                Destroy(other.gameObject);
            }
        }
    }
    void DuckEnd()
    {
        GetComponent<CapsuleCollider>().height = 1.319362f;
    }

}
