using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard2Look : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl" && !Karl_AnimationManager.Instance.invisible && !Karl_AnimationManager.Instance.INKinvisible)
        {
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Karl_AnimationManager.Instance.FreezeMotherMucker();
            Guard2_AnimationManager.Instance.UnderArrest();
            DialogueManager.Instance.CloseDialogueRunningInBackground();

        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "karl")
    //    {
    //        Karl_AnimationManager.Instance.freeze = false;
    //        Karl_AnimationManager.Instance.freeze = false;
    //    }
    //}
}