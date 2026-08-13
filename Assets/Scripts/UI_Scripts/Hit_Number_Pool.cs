using System.Collections.Generic;
using UnityEngine;

public class Hit_Number_Pool : MonoBehaviour
{
    [SerializeField] private GameObject hitNumberPrefab;
    [SerializeField] private int initialPoolSize = 10;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float hitNumberPositionOffset;
    [SerializeField] private float hitNumberAppearWidth;
    [SerializeField] private float hitNumberAppearHeight;

    private readonly Queue<Hit_Number> hitNumberPool = new Queue<Hit_Number>();

    private void Awake()
    {
        //Pre-populate pool
        for (int i = 0; i < initialPoolSize; i++)
        {
            Hit_Number hitNumber = CreateNewHitNumber();
            ReturnToPool(hitNumber);
        }
    }

    public void ShowHitNumber(int amount, bool isBlocked = false)
    {
        float randomWidth = Random.Range(-hitNumberAppearWidth * 0.5f, hitNumberAppearWidth * 0.5f);
        float randomHeight = Random.Range(-hitNumberAppearHeight * 0.5f, hitNumberAppearHeight * 0.5f);

        Vector3 randomOffset = new Vector3(randomWidth, randomHeight, 0);
        Vector3 baseOffset = new Vector3(hitNumberPositionOffset, 0, 0);

        Vector3 spawnPosition = spawnPoint.position + baseOffset + randomOffset;

        Hit_Number hitNumber = GetHitNumber();
        hitNumber.Show(amount, spawnPosition, this, isBlocked);
    }

    private Hit_Number GetHitNumber()
    {
        if (hitNumberPool.Count > 0)
        {
            return hitNumberPool.Dequeue();
        }
        //Expand pool if empty
        return CreateNewHitNumber();
    }

    private Hit_Number CreateNewHitNumber()
    {
        Hit_Number hitNumber = Instantiate(hitNumberPrefab, spawnPoint).GetComponent<Hit_Number>();
        hitNumber.gameObject.SetActive(false);
        return hitNumber;
    }

    public void ReturnToPool(Hit_Number hitNumber)
    {
        hitNumber.gameObject.SetActive(false);
        hitNumberPool.Enqueue(hitNumber);
    }

    private void OnDrawGizmos()
    {
        if (spawnPoint == null) return;
        Gizmos.color = Color.yellow;
        Vector3 baseOffset = new Vector3(hitNumberPositionOffset, 0, 0);
        Gizmos.DrawWireCube(spawnPoint.position + baseOffset, new Vector3(hitNumberAppearWidth, hitNumberAppearHeight, 0));
    }
}
