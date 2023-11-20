using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopCar : MonoBehaviour
{
    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject previousTarget;
    private NavMeshAgent agent;
    [HideInInspector] public Vector2 velocity;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = CopSpawner.Instance.locations[Random.Range(0, CopSpawner.Instance.locations.Count)];
        previousTarget = target;

    }
    private void Moving()
    {
        agent.SetDestination(target.transform.position);
        velocity = agent.velocity;
        if (target.gameObject.tag != "karl")
            if (Mathf.Abs(target.transform.position.x - this.transform.position.x) <= 0.05f && Mathf.Abs(target.transform.position.y - this.transform.position.y) <= 0.05f)
            {
                ChangeTarget();
            }
    }
    private void ChangeTarget()
    {
        target = CopSpawner.Instance.locations[Random.Range(0, CopSpawner.Instance.locations.Count)].gameObject;
        previousTarget = target;
    }
    private void Update()
    {
            Moving();
    }
}
