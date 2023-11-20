using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndQuitGame : MonoBehaviour
{
    [SerializeField] private float quitDelayTime;
    private void Start()
    {
        Invoke("QuitGame", quitDelayTime);
    }
    private void QuitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
