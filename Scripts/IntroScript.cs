using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private VideoPlayer introVideo;
    [SerializeField] private string loadScene;
    private void Start()
    {
        Destroy(GameObject.FindWithTag("carryingAudioManager"));
        introVideo.loopPointReached += LoadNextScene;
    }
    private void LoadNextScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(loadScene);
    }
}
