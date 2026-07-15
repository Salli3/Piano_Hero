using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Exit : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, 0));
    }
}
