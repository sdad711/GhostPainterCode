using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public GameObject outsidePosition;
    [HideInInspector] public bool insideFollower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "follower")
        {
            insideFollower = true;
            CitizenSpawner.Instance.entriesInFollower.Add(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "follower")
        {
            insideFollower = false;
            CitizenSpawner.Instance.entriesInFollower.Remove(this.gameObject);
        }
    }
}
