using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.DeleteAll();
            Destroy(GameObject.FindWithTag("carryingAudioManager"));
            SceneManagerScript.Instance.LoadSceneFromGame("train");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.DeleteAll();
            Destroy(GameObject.FindWithTag("carryingAudioManager"));
            SceneManagerScript.Instance.LoadSceneFromGame("weimar_street");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.DeleteAll();
            Destroy(GameObject.FindWithTag("carryingAudioManager"));
            SceneManagerScript.Instance.LoadSceneFromGame("weimar_square");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.DeleteAll();
            Destroy(GameObject.FindWithTag("carryingAudioManager"));
            SceneManagerScript.Instance.LoadSceneFromGame("weimar_square_stealth");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerPrefs.DeleteAll();
            Destroy(GameObject.FindWithTag("carryingAudioManager"));
            SceneManagerScript.Instance.LoadSceneFromGame("vitraj");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayerPrefs.DeleteAll();
            Destroy(GameObject.FindWithTag("carryingAudioManager"));
            SceneManagerScript.Instance.LoadSceneFromGame("zagreb_painting");
        }
    }
}
