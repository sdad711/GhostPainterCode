using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private GameObject[] scenery;
    [SerializeField] private GameObject drawIntroScreen;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private int karlOrderInLayer;
    [SerializeField] private bool sceneChangeShader;
    [SerializeField] private bool drawIntro;
    
    private bool controlBool;
   
    private void Update()
    {
        if (!controlBool)
        {
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("sceneChange")).value == "go")
            {
                ChangeScene();
                controlBool = true;
            }
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("drawIntro")).value == "go" && drawIntro)
            {
                DrawIntroCut();
                controlBool = true;
            }
        }
    }
    private void ChangeScene()
    {
        for (int i = 0; i < scenery.Length; i++)
        {
            StartCoroutine(FadeOut(scenery[i]));
        }
        if (!DialogueManager.Instance.naratorOnly)
        {
            GameObject karl = Karl_AnimationManager.Instance.animator.gameObject;
            karl.GetComponent<MeshRenderer>().sortingOrder = karlOrderInLayer;
        }
    }
    IEnumerator FadeOut(GameObject scenery)
    {
        if (sceneChangeShader)
        {
            Material material = scenery.gameObject.GetComponent<SpriteRenderer>().material;
            float fade = 1;
            while (material.GetFloat("_Fade") > 0)
            {
                fade -= Time.deltaTime / fadeOutTime;
                material.SetFloat("_Fade", fade);
                if (material.GetFloat("_Fade") <= 0)
                    material.SetFloat("_Fade", 0);
                yield return null;
            }
            scenery.gameObject.SetActive(false);
        }
        else if(!sceneChangeShader)
        {
            Color color = scenery.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 2;
            while (color.a > 0)
            {
                color.a -= Time.deltaTime / fadeOutTime;
                scenery.gameObject.GetComponent<SpriteRenderer>().color = color;
                if (color.a <= 0)
                    color.a = 0;
                yield return null;
            }
            scenery.gameObject.SetActive(false);
        }
    }
    private void DrawIntroCut()
    {
        drawIntroScreen.SetActive(false);
    }
}
