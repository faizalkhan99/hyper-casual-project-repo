using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _dashMultiplier;
    public bool _isDashing;
    public float _movespeed;

    private void FixedUpdate()
    {
        PlayerMoving();
    }
    public void PlayerMoving()
    {
        if (_isDashing)
        {
            transform.Translate(Vector3.right * _movespeed * _dashMultiplier * Time.fixedDeltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _movespeed * Time.fixedDeltaTime);
        }
    }
}
