using System.Collections;
using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    [SerializeField] float _UpwardForce, _dashTimer , _doubleSwipeTimer , _duckTimer;
    [SerializeField] LayerMask _whatIsGrounded;
    
    bool _isgrounded , _isDoubleSwipe ;
    int _doubleJumpCounter, _doubleSwipeDownCounter, dashCounter;
    float _range;
    float _jumpForce, _nextDashTimer , _nextDoubleSwipeTimer , duckCounter , _nextDuckTimer;
    Rigidbody rb;
    PlayerMovement playerMove;
    Vector2 startTouchPos , endTouchPos;
    RaycastHit hit;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        _jumpForce = _UpwardForce;
        _range = transform.localScale.y;
        duckCounter = 0f;
        _isDoubleSwipe = false;
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
            Debug.Log("Dash....");
            dashCounter = 0;
        }
        if (Time.time > _nextDashTimer)
        {
            dashCounter = 0;
            _nextDashTimer = Time.time + _dashTimer;
        }

        if (_doubleSwipeDownCounter == 2)
        {
            
            _doubleSwipeDownCounter = 0;
        }
        if (Time.time > _nextDoubleSwipeTimer)
        {
            _doubleSwipeDownCounter = 0;
            _nextDoubleSwipeTimer = Time.time + _doubleSwipeTimer;
        }

        if (Time.time > _nextDuckTimer)
        {
            duckCounter = 0;
            _nextDuckTimer = Time.time + _duckTimer;
        }
    }
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
                startTouchPos = Vector2.zero;
                endTouchPos = Vector2.zero;
            }
        }
        else if(Mathf.Abs(endTouchPos.x - startTouchPos.x) < Mathf.Abs(endTouchPos.y - startTouchPos.y))
        {

            if ((endTouchPos.y > startTouchPos.y) && startTouchPos.y != 0 && endTouchPos.y != 0)
            {
                if (_doubleJumpCounter <= 1)
                {
                    _doubleJumpCounter++;
                    rb.AddForce(Vector3.up * _jumpForce * 1000 * Time.deltaTime, ForceMode.Force);
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
            Debug.Log("UnderGround");
        }
        else if(duckCounter == 1)
        {
            Debug.Log("Duck");
        }
        duckCounter = 0;
    }

}
