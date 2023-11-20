using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForExtrasRight : MonoBehaviour
{
    [SerializeField] private GameObject extra;
    [SerializeField] private float timer;
    [SerializeField] private float speed;
    [SerializeField] private bool delay;
    [SerializeField] private float delayTime;
    private bool extraWalking;
    private void Start()
    {
        extra.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl" || collision.gameObject.tag == "extra")
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if (delay)
                Invoke("StartExtra", delayTime);
            else if (!delay)
                StartExtra();
        }
    }
    private void StartExtra()
    {
        extra.gameObject.SetActive(true);
        extraWalking = true;
        Invoke("Timer", timer);
    }
    private void Timer()
    {
        extraWalking = false;
        extra.gameObject.SetActive(false);
    }
    private void ExtraWalking()
    {
        if (extra.transform.localScale.x > 0)
            extra.transform.position += Vector3.right * speed * Time.deltaTime;
        else if (extra.transform.localScale.x < 0)
            extra.transform.position += Vector3.left * speed * Time.deltaTime;
    }
    private void Update()
    {
        if (extraWalking)
            ExtraWalking();
    }
}
