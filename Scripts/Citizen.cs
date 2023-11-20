using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : MonoBehaviour
{
     public GameObject targetFirst;
     public GameObject targetSecond;
     public GameObject targetThird;
    public bool firstMovement, secondMovement, thirdMovement;
    [SerializeField] private float speed;
    private NavMeshAgent agent;
    [HideInInspector] public bool cameraStopMovement;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.enabled = false;
    }
    private void OnEnable()
    {
        AllMovementStop();
        firstMovement = true;
    }
    private void AllMovementStop()
    {
        firstMovement = false;
        secondMovement = false;
        thirdMovement = false;
    }
    private void FirstMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetFirst.transform.position, speed * Time.deltaTime);
        if(transform.position == targetFirst.transform.position)
        {
            AllMovementStop();
            secondMovement = true;
        }
    }
    private void SecondMovement()
    {
        agent.enabled = true;
        agent.SetDestination(targetSecond.transform.position);
        if (Mathf.Abs(targetSecond.transform.position.x - this.transform.position.x) <= 0.05f && Mathf.Abs(targetSecond.transform.position.y - this.transform.position.y) <= 0.05f)
        {
            AllMovementStop();
            thirdMovement = true;
        }
    }
    private void ThirdMovement()
    {
        agent.enabled = false;
        transform.position = Vector2.MoveTowards(transform.position, targetThird.transform.position, speed * Time.deltaTime);
        if(transform.position == targetThird.transform.position)
        {
            AllMovementStop();
            CitizenPool.Instance.ReturnToPool(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "follower")
        {
            CitizenPool.Instance.ReturnToPool(this);
        }
    }
    private void Update()
    {
        if (firstMovement && !cameraStopMovement)
            FirstMovement();
        else if (secondMovement && !cameraStopMovement)
            SecondMovement();
        else if (thirdMovement && !cameraStopMovement)
            ThirdMovement();
        else
            return;
    }
}
