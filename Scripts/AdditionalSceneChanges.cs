using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalSceneChanges : MonoBehaviour
{ 
    [SerializeField] private GameObject[] scenery2, scenery3, scenery4;
    [SerializeField] private float fadeOutTime;
    [SerializeField] private int karlOrderInLayer;
    [SerializeField] private bool sceneChangeShader;

    private bool controlBool2, controlBool3, controlBool4;

    private void Update()
    {
        if (!controlBool2)
        {
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("sceneChange2")).value == "go")
            {
                ChangeScene(scenery2);
                controlBool2 = true;
            }
        }
        if (!controlBool3)
        {
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("sceneChange3")).value == "go")
            {
                ChangeScene(scenery3);
                controlBool3 = true;
            }
        }
        if (!controlBool4)
        {
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("sceneChange4")).value == "go")
            {
                ChangeScene(scenery4);
                controlBool4 = true;
            }
        }
    }
    private void ChangeScene(GameObject[] scenery)
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
        else if (!sceneChangeShader)
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
}
