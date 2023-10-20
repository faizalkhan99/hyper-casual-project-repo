using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _dashMultiplier;
    [HideInInspector] public bool _isDashing;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Temp"))
        {
            NextLevel();
        }
    }
    void NextLevel()
    {
        if (PlayerPrefs.GetInt("LevelSelect") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("LevelSelect", SceneManager.GetActiveScene().buildIndex);
        }
    }
}
