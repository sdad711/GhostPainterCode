using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainIntroScript : MonoBehaviour
{
    [SerializeField] private GameObject train;
    private bool startTrain;
    [SerializeField] private float cameraWideDelay;
    [SerializeField] private float trainStartDelay;
    [SerializeField] private float trainSpeed;
    
    private void Start()
    {
        Invoke("CameraWide", cameraWideDelay);
        Invoke("StartTrain", trainStartDelay);
    }
    private void CameraWide()
    {
        SceneManagerScript.Instance.Camera1();
    }
    private void CameraCloseUp()
    {
        SceneManagerScript.Instance.Camera3();
    }
    private void StartTrain()
    {
        startTrain = true;
    }
    private void Update()
    {
        if (startTrain)
            train.transform.position += Vector3.right * Time.deltaTime * trainSpeed;
        if (train.transform.position.x >= 59f)
            CameraCloseUp();
        if (train.transform.position.x >= 61f)
            startTrain = false;
    }
}
