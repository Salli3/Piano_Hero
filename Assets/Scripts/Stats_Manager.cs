using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Manager : MonoBehaviour
{
    public static Stats_Manager instance;

    [Header("Player Stats")]
    public float damage;
    public float currentHP;
    public float maxHP;
    public Note_SO[] attackTypes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
