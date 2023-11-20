using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    private Vector3 position;
    private Vector3 whereToGo;
    //private void Awake()
    //{
        
    //}
    //private void OnEnable()
    //{

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "textStop")
        {
            Dialogue.Instance.RemoveFromList(this.gameObject);
            SpeechBubblePool.Instance.ReturnToPool(this);
        }
    }
    //public void MoveUp(Vector3 currentPosition, Vector3 up)
    //{
    //    position = currentPosition;
    //    whereToGo = up;
    //}
    //private void Update()
    //{
        
    //    //transform.position = Vector2.MoveTowards(transform.position, whereToGo, 1 * Time.deltaTime);
    //}
}
