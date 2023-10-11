using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float _movespeed;
    [SerializeField] float maxSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        Debug.Log(rb.velocity.magnitude);
        if(rb.velocity.magnitude > maxSpeed)
        {
            return;
        }
        rb.AddForce(new Vector3(_movespeed * Time.fixedDeltaTime,0,0) , ForceMode.Acceleration);
    }
}
