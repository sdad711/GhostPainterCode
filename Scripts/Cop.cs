using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cop : MonoBehaviour
{
    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject previousTarget;
    private NavMeshAgent agent;
    [SerializeField] private float agentSpeedMin, agentSpeedMax;
    [SerializeField] private GameObject lookShort, lookLong;
    [HideInInspector] public Vector2 velocity;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        CheckForCopSpeed();
        CheckForCopSight();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        previousTarget = target;
        
    }
    private void CheckForCopSpeed()
    {
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSpeed")).value == "slow")
            agent.speed = agentSpeedMin;
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSpeed")).value == "fast")
            agent.speed = agentSpeedMax;
    }
    private void CheckForCopSight()
    {
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSight")).value == "short")
        {
            lookShort.SetActive(true);
            lookLong.SetActive(false);
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsSight")).value == "long")
        {
            lookShort.SetActive(false);
            lookLong.SetActive(true);
        }
    }
    private void Moving()
    {
        agent.SetDestination(target.transform.position);
        velocity = agent.velocity;
        if(target.gameObject.tag != "karl")
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
