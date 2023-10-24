using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu, _credits , _settings;

    private void Start()
    {
        _mainMenu.SetActive(true);
        _credits.SetActive(false);
        _settings.SetActive(false);
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitBtn()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void Credits()
    {
        _mainMenu.SetActive(false);
        _credits.SetActive(true);
    }
    public void Settings()
    {
        _mainMenu.SetActive(false);
        _settings.SetActive(true);
    }

    public void Back_Btn()
    {
        _mainMenu.SetActive(true);
        _credits.SetActive(false);
        _settings.SetActive(false);
    }
}
