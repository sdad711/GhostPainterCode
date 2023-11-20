using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopPool : MonoBehaviour
{
    [SerializeField] private Cop copPrefab;
    private Queue<Cop> cops = new Queue<Cop>();

    public static CopPool Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public Cop Get()
    {
        if (cops.Count == 0)
        {
            AddCop(1);
        }
        return cops.Dequeue();
    }
    private void AddCop(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Cop cop = Instantiate(copPrefab);
            cop.gameObject.SetActive(false);
            cops.Enqueue(cop);
        }
    }
    public void ReturnToPool(Cop cop)
    {
        cop.gameObject.SetActive(false);
        CopSpawner.Instance.cops.Remove(cop.gameObject);
        cops.Enqueue(cop);
        CopSpawner.Instance.Add();
    }
}
