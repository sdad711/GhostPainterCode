using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenSpawner : MonoBehaviour
{
    public List<GameObject> entries = new List<GameObject>();
    public List<GameObject> outries = new List<GameObject>();
    [HideInInspector] public List<GameObject> entriesInFollower = new List<GameObject>();
    [HideInInspector] public List<GameObject> outriesInFollower = new List<GameObject>();
     public List<GameObject> currentCitizens = new List<GameObject>();

    public static CitizenSpawner Instance { get; private set; }
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
        foreach (GameObject entry in entriesInFollower)
        {
            var spawnedCitizen = CitizenPool.Instance.Get();
            currentCitizens.Add(spawnedCitizen.gameObject);
            spawnedCitizen.transform.position = entry.transform.position;
            var target = entry.transform.GetComponent<Entry>().outsidePosition;
            spawnedCitizen.targetFirst = target;
            spawnedCitizen.targetSecond = outriesInFollower[Random.Range(0, outriesInFollower.Count)];
            spawnedCitizen.targetThird = spawnedCitizen.targetSecond.GetComponent<Outry>().insidePosition;
            spawnedCitizen.gameObject.SetActive(true);
        }
    }
    public void Add()
    {
        StartCoroutine(AddCitizen());
    }
    private IEnumerator AddCitizen()
    {
        yield return new WaitForSeconds(0);
        var spawnedCitizen = CitizenPool.Instance.Get();
        currentCitizens.Add(spawnedCitizen.gameObject);

        var randomEntryPosition = entriesInFollower[Random.Range(0, entriesInFollower.Count)].gameObject;
        spawnedCitizen.transform.position = randomEntryPosition.transform.position;
        var target = randomEntryPosition.transform.GetComponent<Entry>().outsidePosition;
        spawnedCitizen.targetFirst = target;
        spawnedCitizen.targetSecond = outriesInFollower[Random.Range(0, outriesInFollower.Count)];
        spawnedCitizen.targetThird = spawnedCitizen.targetSecond.GetComponent<Outry>().insidePosition;
        spawnedCitizen.gameObject.SetActive(true);
    }
    public void HideCurrentCitizens()
    {
        foreach (GameObject citizen in currentCitizens)
        {
            citizen.transform.GetComponent<Citizen>().cameraStopMovement = true;
            citizen.transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    public void ShowCurrentCitizens()
    {
        foreach (GameObject citizen in currentCitizens)
        {
            citizen.transform.GetComponent<Citizen>().cameraStopMovement = false;
            citizen.transform.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
