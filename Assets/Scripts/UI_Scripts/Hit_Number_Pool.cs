using System.Collections.Generic;
using UnityEngine;

public class Hit_Number_Pool : MonoBehaviour
{
    [SerializeField] private GameObject hitNumberPrefab;
    [SerializeField] private int initialPoolSize = 10;

    private Queue<Hit_Number> hitNumberPool = new Queue<Hit_Number>();

    private void Awake()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Hit_Number hitNumber = Instantiate(hitNumberPrefab, transform).GetComponent<Hit_Number>();
            hitNumber.gameObject.SetActive(false);
            hitNumberPool.Enqueue(hitNumber);
        }
    }

    public Hit_Number GetHitNumber()
    {
        if (hitNumberPool.Count == 0)
        {
            Hit_Number extra = Instantiate(hitNumberPrefab, transform).GetComponent<Hit_Number>();
            return extra;
        }
        return hitNumberPool.Dequeue();
    }

    public void ReturnToPool(Hit_Number hitNumber)
    {
        hitNumber.gameObject.SetActive(false);
        hitNumberPool.Enqueue(hitNumber);
    }
}