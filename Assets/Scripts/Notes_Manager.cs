using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes_Manager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float width;
    [SerializeField] private float height;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnPoints[0].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[1].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[2].position, new Vector3(1, 1, 0));
        Gizmos.DrawWireCube(spawnPoints[3].position, new Vector3(1, 1, 0));
    }
}
