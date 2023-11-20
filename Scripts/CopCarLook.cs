using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopCarLook : MonoBehaviour
{
    [SerializeField] private CopCar copCar;
    private float alertTimer = 2;
    private float safeTimer = 5;
    private bool seeKarl;
    private bool karlGoneAway;
    private GameObject karl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl")
        {
            copCar.target = collision.gameObject;
            karl = collision.gameObject;
            karlGoneAway = false;
            seeKarl = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl")
        {
            copCar.target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl")
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
                copCar.target = copCar.previousTarget;
                karlGoneAway = false;
            }
        }
    }
}
