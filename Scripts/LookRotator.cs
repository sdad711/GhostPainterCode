using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRotator : MonoBehaviour
{
    [SerializeField] private Cop cop;
    [SerializeField] private float rotationSpeed;
    private void Update()
    {
        Quaternion toRotation = Quaternion.LookRotation(cop.transform.forward, cop.velocity);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
