using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Button[] _buttons;
    void Update()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i <= PlayerPrefs.GetInt("LevelSelect"))
            {
                _buttons[i].interactable = true;
            }
            else
            {
                _buttons[i].interactable = false;
            }
        }

    }
}
