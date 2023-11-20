using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class ChangeDialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogueTarget;
    [SerializeField] private TextAsset dialogueToChangeInkJSON;
    [SerializeField] private bool automaticDialogue;
    [SerializeField] private bool repeatConversation;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "karl" && !DialogueManager.Instance.dialogueIsPlaying)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
            dialogueTarget.inkJSON = dialogueToChangeInkJSON;
            dialogueTarget.automaticDialogue = automaticDialogue;
            dialogueTarget.repeatConversation = repeatConversation;
            dialogueTarget.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
