using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Lane_Trigger : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Enemy_Manager enemyManager;

    private void Start()
    {
        anim.Play("Start");
    }

    public void StartSpawningEnemy()
    {
        enemyManager.PickEnemy();
    }
}
