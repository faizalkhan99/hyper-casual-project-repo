using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float _movespeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(_movespeed * Time.fixedDeltaTime,0,0) , ForceMode.Impulse);
    }
}
