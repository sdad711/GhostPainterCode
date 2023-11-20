using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private bool loadWithTimer;
    [SerializeField] private float loadingDelayTime;
    private void Start()
    {
        if(loadWithTimer)
        {
            Invoke("LoadSceneWithTimer", loadingDelayTime);
        }
    }
    private void LoadSceneWithTimer()
    {
        SceneManagerScript.Instance.LoadSceneFromGame(sceneName);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl")
        {
            SceneManagerScript.Instance.LoadSceneFromGame(sceneName);
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
