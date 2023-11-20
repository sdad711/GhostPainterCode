using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class OnlyDialogue : MonoBehaviour
{
    [SerializeField] private TextAsset dialogue;
    private void Start()
    {
        DialogueManager.Instance.EnterDialogueMode(dialogue);
    }
}
