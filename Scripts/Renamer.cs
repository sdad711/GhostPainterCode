using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Renamer : MonoBehaviour
{
    public GameObject[] copLocations;
    private void Start()
    {
        for (int i = 0; i < copLocations.Length; i++)
        {
            copLocations[i].name = "CopLocation" + i;
        }
    }
}
