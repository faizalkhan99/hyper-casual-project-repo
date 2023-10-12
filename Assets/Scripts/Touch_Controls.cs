using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    [SerializeField] float _UpwardForce, _range, _dashTimer , _doubleSwipeTimer;
    [SerializeField] LayerMask _whatIsGrounded;

    bool _isgrounded ;
    int _doubleJumpCounter , _doubleSwipeDownCounter , dashCounter;
    float _jumpForce, _nextDashTimer , _nextDoubleSwipeTimer;
    Rigidbody rb;
    PlayerMovement playerMove;
    Vector3 startTouchPos;
    Vector3 endTouchPos;
    RaycastHit hit;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        _jumpForce = _UpwardForce;
    }

    private void Update()
    {
        Raycast();
        Touch();
        if(_isgrounded)
        {
            _doubleJumpCounter = 0;
        }

        if(Time.time > _nextDashTimer)
        {
            dashCounter = 0;
            _nextDashTimer = Time.time + _dashTimer;
        }
        if(dashCounter == 2)
        {
            Debug.Log("Dash....");
        }
/*
        if(_doubleSwipeDownCounter == 2)
        {
            Debug.Log("Player is underGround");
        }
        if(Time.time > _nextDoubleSwipeTimer)
        {
            _doubleSwipeDownCounter = 0;
            _nextDoubleSwipeTimer = Time.time + _doubleSwipeTimer;
        }*/
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

        if(endTouchPos.x > startTouchPos.x)
        {
            startTouchPos = Vector3.zero;
            endTouchPos = Vector3.zero;
            if (dashCounter <= 1)
            {
                dashCounter++;
            }
        }

        if (endTouchPos.y > startTouchPos.y)
        {
            startTouchPos = Vector3.zero;
            endTouchPos = Vector3.zero;
            if (_doubleJumpCounter <= 0)
            {
                _doubleJumpCounter++;
                Debug.Log("Hello");
                rb.AddForce(Vector3.up * _jumpForce * 1000 * Time.deltaTime, ForceMode.Force);
            }
        }
    }

    void Raycast()
    {
        if(Physics.Raycast(transform.position , transform.TransformDirection(Vector3.down) , out hit, _range, _whatIsGrounded))
        {
            _isgrounded = true;
        }
        else
        {
            _isgrounded = false;
        }
    }

}
