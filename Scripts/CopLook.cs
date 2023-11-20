using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopLook : MonoBehaviour
{
    [SerializeField] private Cop cop;
    private float alertTimer = 2;
    private float safeTimer = 5;
    private bool seeKarl;
    private bool karlGoneAway;
    private GameObject karl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl")
        {
            cop.target = collision.gameObject;
            karl = collision.gameObject;
            karlGoneAway = false;
            seeKarl = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "karl")
        {
            cop.target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "karl")
        {
            seeKarl = false;
            karlGoneAway = true;
        }
    }
    private void Update()
    {
        if (seeKarl)
        {
            safeTimer = 5;
            alertTimer -= Time.deltaTime;
            if (alertTimer <= 0)
            {
                karl.GetComponent<Karl_Move>().freeze = true;
                SceneManagerScript.Instance.LoadSceneFromGame("zagreb_painting");

            }
        }
        if (!seeKarl)
        {
            alertTimer = 2;
        }
        if (karlGoneAway)
        {
            safeTimer -= Time.deltaTime;
            if (safeTimer <= 0)
            {
                cop.target = cop.previousTarget;
                karlGoneAway = false;
            }
        }
    }
}
