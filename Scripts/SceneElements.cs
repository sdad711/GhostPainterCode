using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElements : MonoBehaviour
{
    [SerializeField] private GameObject[] elements;
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private bool startingScene;
    private bool controlBoolKarl, controlBoolCopsNumbers, controlBoolCopsSpeed, controlBoolCopsSight, controlBoolCopsCar, controlBoolFinalDestination;

    private void Start()
    {
    if (startingScene)
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("karlIs")).value == "walking")
            {
                foreach (GameObject element in elements)
                    if (element.name == "karl")
                        element.gameObject.SetActive(true);
            }
            else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("karlIs")).value == "running")
            {
                foreach (GameObject element in elements)
                {
                    if (element.name == "karl")
                        element.gameObject.SetActive(true);
                    if (element.name == "karlFast")
                        element.gameObject.SetActive(true);
                }
            }
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
        {
            foreach (GameObject element in elements)
                if (element.name == "copsFew")
                    element.gameObject.SetActive(true);
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
        {
            foreach (GameObject element in elements)
            {
                if (element.name == "copsFew")
                    element.gameObject.SetActive(true);
                if (element.name == "copsMany")
                    element.gameObject.SetActive(true);
            }
        }
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSpeed")).value == "slow")
        {
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
            {
                foreach (GameObject element in elements)
                    if (element.name == "copsFewSlow")
                        element.gameObject.SetActive(true);
            }
            else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
            {
                foreach (GameObject element in elements)
                {
                    if (element.name == "copsFewSlow")
                        element.gameObject.SetActive(true);
                    if (element.name == "copsManySlow")
                        element.gameObject.SetActive(true);
                }

            }
        }
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSight")).value == "short")
        {
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
            {
                foreach (GameObject element in elements)
                {
                    if (element.name == "copsFewShortSight")
                        element.gameObject.SetActive(true);
                }
            }
            else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
                foreach (GameObject element in elements)
                {
                    if (element.name == "copsManyShortSight")
                        element.gameObject.SetActive(true);
                }
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSight")).value == "long")
        {
            if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
            {
                foreach (GameObject element in elements)
                {
                    if (element.name == "copsFewLongSight") //izmjenit kad dode
                        element.gameObject.SetActive(true);
                }
            }
            else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
                foreach (GameObject element in elements)
                {
                    if (element.name == "copsManyLongSight")
                        element.gameObject.SetActive(true);
                }
        }
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsCar")).value == "yes")
        {
            foreach (GameObject element in elements)
                if (element.name == "copsCar")
                    element.gameObject.SetActive(true);
        }
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "east")
        {
            foreach (GameObject element in elements)
                if (element.name == "east")
                    element.gameObject.SetActive(true);
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "north")
        {
            foreach (GameObject element in elements)
                if (element.name == "north")
                    element.gameObject.SetActive(true);
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "west")
        {
            foreach (GameObject element in elements)
                if (element.name == "west")
                    element.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (!startingScene)
        {
            if (!controlBoolKarl)
                if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("karlIs")).value == "walking")
                {
                    foreach (GameObject element in elements)
                        if (element.name == "karl")
                        {
                            StartCoroutine(FadeIn(element));
                            controlBoolKarl = true;
                            Debug.Log(element);
                        }
                }
                else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("karlIs")).value == "running")
                {
                    foreach (GameObject element in elements)
                    {
                        if (element.name == "karl")
                        {
                            StartCoroutine(FadeIn(element));
                            controlBoolKarl = true;
                            Debug.Log(element);
                        }
                        if (element.name == "karlFast")
                        {
                            StartCoroutine(FadeIn(element));
                            controlBoolKarl = true;
                            Debug.Log(element);
                        }
                    }
                }
            if (!controlBoolCopsNumbers)
                if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
                {
                    foreach (GameObject element in elements)
                        if (element.name == "copsFew")
                        {
                            StartCoroutine(FadeIn(element));
                            controlBoolCopsNumbers = true;
                        }
                }
                else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
                {
                    foreach (GameObject element in elements)
                    {
                        if (element.name == "copsFew")
                        {
                            StartCoroutine(FadeIn(element));
                            controlBoolCopsNumbers = true;
                        }
                        if (element.name == "copsMany")
                        {
                            StartCoroutine(FadeIn(element));
                            controlBoolCopsNumbers = true;
                        }
                    }
                }
            if (!controlBoolCopsSpeed)
                if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSpeed")).value == "slow")
                {
                    if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
                    {
                        foreach (GameObject element in elements)
                            if (element.name == "copsFewSlow")
                            {
                                StartCoroutine(FadeIn(element));
                                controlBoolCopsSpeed = true;
                            }
                    }
                    else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
                    {
                        foreach (GameObject element in elements)
                        {
                            if (element.name == "copsFewSlow")
                            {
                                StartCoroutine(FadeIn(element));
                                controlBoolCopsSpeed = true;
                            }
                            if (element.name == "copsManySlow")
                            {
                                StartCoroutine(FadeIn(element));
                                controlBoolCopsSpeed = true;
                            }
                        }
                        
                    }
                }
            if (!controlBoolCopsSight)
                if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSight")).value == "short")
                {
                    if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
                        foreach (GameObject element in elements)
                        {
                            if (element.name == "copsFewShortSight")
                            {
                                StartCoroutine(FadeIn(element));
                                controlBoolCopsSight = true;
                            }
                        }
                    else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
                        foreach (GameObject element in elements)
                        {
                            if (element.name == "copsManyShortSight")
                            {
                                StartCoroutine(FadeIn(element));
                                controlBoolCopsSight = true;
                            }
                        }
                }
                else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSight")).value == "long")
                {
                    if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
                        foreach (GameObject element in elements)
                        {
                            if (element.name == "copsFewLongSight")
                            {
                                StartCoroutine(FadeIn(element));
                                controlBoolCopsSight = true;
                            }
                        }
                    else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
                        foreach (GameObject element in elements)
                        {
                            if (element.name == "copsManyLongSight")
                            {
                                StartCoroutine(FadeIn(element));
                                controlBoolCopsSight = true;
                            }
                        }
                }
            if (!controlBoolCopsCar)
                if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsCar")).value == "yes")
                {
                    foreach (GameObject element in elements)
                        if (element.name == "copsCar")
                        {
                            StartCoroutine(FadeIn(element));
                            controlBoolCopsNumbers = true;
                        }
                }
            if (!controlBoolFinalDestination)
                if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "east")
                {
                    foreach (GameObject element in elements)
                        if (element.name == "east")
                        {
                            StartCoroutine(FadeIn(element));
                            GameObject child = element.transform.Find("child").gameObject;
                            StartCoroutine(FadeIn(child));
                            controlBoolFinalDestination = true;
                        }
                }
                else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "north")
                {
                    foreach (GameObject element in elements)
                        if (element.name == "north")
                        {
                            StartCoroutine(FadeIn(element));
                            GameObject child = element.transform.Find("child").gameObject;
                            StartCoroutine(FadeIn(child));
                            controlBoolFinalDestination = true;
                        }
                }
                else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "west")
                {
                    foreach (GameObject element in elements)
                        if (element.name == "west")
                        {
                            StartCoroutine(FadeIn(element));
                            GameObject child = element.transform.Find("child").gameObject;
                            StartCoroutine(FadeIn(child));
                            controlBoolFinalDestination = true;
                        }
                }
        }
    }
    IEnumerator FadeIn(GameObject element)
    {
        Color color = element.gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        element.gameObject.GetComponent<SpriteRenderer>().color = color;
        element.gameObject.SetActive(true);
        float progress = 0f;
        while (progress < 1)
        {
            Color tmpColor = element.gameObject.GetComponent<SpriteRenderer>().color;
            element.gameObject.GetComponent<SpriteRenderer>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp(color.a, 255, progress));
            progress += Time.deltaTime / fadeInSpeed;
            yield return null;
        }
    }
}