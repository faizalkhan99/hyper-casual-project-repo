using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu, _credits , _settings, _levelSelector;

    private void Start()
    {
        _mainMenu.SetActive(true);
        _credits.SetActive(false);
        _settings.SetActive(false);
        _levelSelector.SetActive(false);
    }

    public void PlayBtn()
    {
        _levelSelector.SetActive(true);
        _mainMenu.SetActive(false);
        
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
