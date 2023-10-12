using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    [SerializeField] float _UpwardForce , _range;
    [SerializeField] LayerMask _whatIsGrounded;

    bool _isgrounded;
    int _doubleJumpCounter;
    float _jumpForce;
    Rigidbody rb;
    Vector3 startTouchPos;
    Vector3 endTouchPos;
    RaycastHit hit;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
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

        if(endTouchPos.y > startTouchPos.y )
        {
            startTouchPos = Vector3.zero;
            endTouchPos = Vector3.zero;
            if(_doubleJumpCounter <= 0)
            {
                _doubleJumpCounter++;
                rb.AddForce(Vector3.up * _jumpForce * 1000 * Time.fixedDeltaTime, ForceMode.Force);
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
