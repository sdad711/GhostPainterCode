using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outry : MonoBehaviour
{
    public GameObject insidePosition;
    [HideInInspector] public bool insideFollower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "follower")
        {
            insideFollower = true;
            CitizenSpawner.Instance.outriesInFollower.Add(this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "follower")
        {
            insideFollower = false;
            CitizenSpawner.Instance.outriesInFollower.Remove(this.gameObject);
        }
    }
}
