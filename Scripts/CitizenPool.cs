using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenPool : MonoBehaviour
{
    [SerializeField] private Citizen citizenPrefab;
    private Queue<Citizen> citizens = new Queue<Citizen>();

    public static CitizenPool Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public Citizen Get()
    {
        if(citizens.Count == 0)
        {
            AddCitizen(1);
        }
        return citizens.Dequeue();
    }
    private void AddCitizen(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Citizen citizen = Instantiate(citizenPrefab);
            citizen.gameObject.SetActive(false);
            citizens.Enqueue(citizen);
        }
    }
    public void ReturnToPool(Citizen citizen)
    {
        citizen.gameObject.SetActive(false);
        CitizenSpawner.Instance.currentCitizens.Remove(citizen.gameObject);
        citizens.Enqueue(citizen);
        CitizenSpawner.Instance.Add();
    }
}
