using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note_Exit : MonoBehaviour
{
    [SerializeField] private Player_HP playerHP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.gameObject.SetActive(false);
        playerHP.ChangeHP(0);
        Destroy(collision.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, 0));
    }
}
