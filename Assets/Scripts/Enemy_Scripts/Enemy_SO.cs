using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO")]
public class Enemy_SO : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public float enemyHP;
    public Note_SO[] attackTypes;
}
