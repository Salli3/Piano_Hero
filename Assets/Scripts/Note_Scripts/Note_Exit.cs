using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note_Exit : MonoBehaviour
{
    public static event Action<Note_SO> OnNoteExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnNoteExit?.Invoke(collision.gameObject.GetComponent<Note>().noteSO);
        Destroy(collision.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, 0));
    }
}
