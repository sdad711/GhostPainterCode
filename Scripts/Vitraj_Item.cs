using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vitraj_Item : MonoBehaviour
{
    [SerializeField] private GameObject correctItem;
    private bool sweetPoint;
    private bool goBaby;
    public InputAction vitrajItemButton;
    private void OnEnable()
    {
        vitrajItemButton.Enable();
    }
    private void OnDisable()
    {
        vitrajItemButton.Disable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == correctItem)
        {
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == correctItem)
        {
            if (Mathf.Abs(collision.gameObject.transform.position.x - this.transform.position.x) <= 0.5f && Mathf.Abs(collision.gameObject.transform.position.y - this.transform.position.y) <= 0.5f)
            {
                sweetPoint = true;
            }
            else
                sweetPoint = false;
            if (goBaby && sweetPoint)
            {
                collision.gameObject.transform.position = transform.position;
                collision.gameObject.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                collision.gameObject.transform.Find("Bloom").gameObject.SetActive(false);
                Vitraj_Moving.Instance.RemoveFromList(collision.gameObject);
                Vitraj_Moving.Instance.SelectItem();
                sweetPoint = false;
                goBaby = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        sweetPoint = false;
    }
    private void Update()
    {
        if (!DialogueManager.Instance.dialogueIsPlaying)
        {
            if (sweetPoint)
            {
                if (vitrajItemButton.WasPressedThisFrame())
                {
                    goBaby = true;
                }
            }
            if (vitrajItemButton.WasReleasedThisFrame())
            {
                goBaby = false;
            }
        }
    }
}

