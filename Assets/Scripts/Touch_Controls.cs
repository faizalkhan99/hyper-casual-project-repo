using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    bool _isgrounded;
    Rigidbody rb;
    int _doubleJumpCounter;
    float _jumpForce;
    PlayerMovement playermove;
    [SerializeField] float _UpwardForce , _downwardForce;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
        playermove = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        _jumpForce = _UpwardForce;
    }

    private void Update()
    {
        Touch();
        if (_doubleJumpCounter == 2)
        {
            _doubleJumpCounter = 2;
        }

        if (!_isgrounded && _doubleJumpCounter == 2)
        {
            rb.AddForce(Vector3.down * _downwardForce * Time.deltaTime);
            _jumpForce = 0;
        }
        else
        {
            _jumpForce = _UpwardForce;
        }

    }
    void Touch()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Moved:
                    if (t.position.y > transform.position.y)
                    {
                        rb.AddForce(Vector3.up * _jumpForce * Time.deltaTime);
                    }
                    break;
                case TouchPhase.Ended:
                    
                    if (_doubleJumpCounter == 2)
                    {
                        _doubleJumpCounter = 2;
                    }
                    else if(_doubleJumpCounter != 2)
                    {
                        _doubleJumpCounter++;
                    }
                    break;

            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isgrounded = true;
            _doubleJumpCounter = 0;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isgrounded = false;
        }
    }
}
