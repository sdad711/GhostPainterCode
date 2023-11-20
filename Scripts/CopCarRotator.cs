using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopCarRotator : MonoBehaviour
{
    [SerializeField] private CopCar copCar;
    [SerializeField] private float rotationSpeed;
    private void Update()
    {
        Quaternion toRotation = Quaternion.LookRotation(copCar.transform.forward, copCar.velocity);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
