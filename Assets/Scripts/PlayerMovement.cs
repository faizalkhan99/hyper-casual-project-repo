using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float _movespeed;
    public float maxSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        PlayerMoving();
    }
    public void PlayerMoving()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            return;
        }
        rb.AddForce(new Vector3(_movespeed * Time.fixedDeltaTime, 0, 0), ForceMode.Acceleration);
    }
}
