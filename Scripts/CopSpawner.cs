using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopSpawner : MonoBehaviour
{
    public List<GameObject> locations = new List<GameObject>();
    [HideInInspector] public List<GameObject> cops = new List<GameObject>();
    [SerializeField] private CopCar copCar, copCar2;

    public static CopSpawner Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Invoke("FirstSpawn", 1f);
    }
    public void FirstSpawn()
    {
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "few")
        {
            Debug.Log("few");
            for (int i = 0; i < locations.Count; i++)
            {
                var spawnedCop = CopPool.Instance.Get();
                cops.Add(spawnedCop.gameObject);
                spawnedCop.transform.position = locations[i].transform.position;
                spawnedCop.target = locations[Random.Range(0, locations.Count)];
                spawnedCop.gameObject.SetActive(true);
            }
        }
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsNumbers")).value == "many")
        {
            Debug.Log("many");
            foreach (GameObject location in locations)
            {
                var spawnedCop = CopPool.Instance.Get();
                cops.Add(spawnedCop.gameObject);
                spawnedCop.transform.position = location.transform.position;
                spawnedCop.target = locations[Random.Range(0, locations.Count)];
                spawnedCop.gameObject.SetActive(true);
            }
            foreach (GameObject location in locations)
            {
                var spawnedCop = CopPool.Instance.Get();
                cops.Add(spawnedCop.gameObject);
                spawnedCop.transform.position = location.transform.position;
                spawnedCop.target = locations[Random.Range(0, locations.Count)];
                spawnedCop.gameObject.SetActive(true);
            }
        }
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsCar")).value == "yes")
        {
            copCar.gameObject.SetActive(true);
            copCar2.gameObject.SetActive(true);
        }
    }
    public void Add()
    {
        StartCoroutine(AddCop());
    }
    private IEnumerator AddCop()
    {
        yield return new WaitForSeconds(1);
        var spawnedCop = CopPool.Instance.Get();
        cops.Add(spawnedCop.gameObject);
        spawnedCop.transform.position = locations[Random.Range(0, locations.Count)].transform.position;
        spawnedCop.target = locations[Random.Range(0, locations.Count)];
        spawnedCop.gameObject.SetActive(true);
    }
    public void EnlargeCurrentCops()
    {
        foreach (GameObject cop in cops)
        {
            //ENLARGE IT
        }
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsCar")).value == "yes")
        {
            
        }

    }
    public void ShrinkCurrentCops()
    {
        foreach (GameObject cop in cops)
        {
            //RETURN TO REGULAR SIZE
        }
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("copsCar")).value == "yes")
        {
           
        }
    }
}

