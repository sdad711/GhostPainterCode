using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destinations : MonoBehaviour
{
    [SerializeField] private GameObject eastLocation, northLocation, westLocation;
    private GameObject karlDestination;
    private GameObject enlargeDestination;
    public static Destinations Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CloseAllLocations();
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "east")
        {
            karlDestination = eastLocation;
            enlargeDestination = karlDestination.transform.Find("Enlarge").gameObject;
            karlDestination.SetActive(true);
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "north")
        {
            karlDestination = northLocation;
            enlargeDestination = karlDestination.transform.Find("Enlarge").gameObject;
            karlDestination.SetActive(true);
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("finalDestination")).value == "west")
        {
            karlDestination = westLocation;
            enlargeDestination = karlDestination.transform.Find("Enlarge").gameObject;
            karlDestination.SetActive(true);
        }
    }
    private void CloseAllLocations()
    {
        eastLocation.SetActive(false);
        northLocation.SetActive(false);
        westLocation.SetActive(false);
    }
    public void EnlargeDestination()
    {
        enlargeDestination.SetActive(true);
    }
    public void ShrinkDestination()
    {
        enlargeDestination.SetActive(false);
    }
}
