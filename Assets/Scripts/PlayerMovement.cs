using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 swipeStartPos;
    Vector2 swipeEndPos;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            swipeStartPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            swipeEndPos = Input.GetTouch(0).position;
            HandleSwipe();
        }
    }

    void HandleSwipe()
    {
        Vector2 swipeDirection = swipeEndPos - swipeStartPos;
        if (swipeDirection.y > 0 && swipeDirection.y > Mathf.Abs(swipeDirection.x))
        {
            Jump();
        }
    }

    [SerializeField] public float jumpForce;
    public Rigidbody rb;

    void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            Debug.Log("Jumped!");
        }
    }

    bool IsGrounded()
    {
        Vector3 feetPosition = transform.position - new Vector3(0f, 0.5f, 0f);
        float rayLength = 1f;
        RaycastHit hit;

        if (Input.GetMouseButton(0) && Physics.Raycast(feetPosition, Vector3.down, out hit, rayLength))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("Grounded!");
                return true;
            }
        }

        return false;
    }
}
