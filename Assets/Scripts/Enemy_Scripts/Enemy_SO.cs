using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO")]
public class Enemy_SO : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public int enemyHP;
    public int damage;
    public int enemyMoneyReward;
    public Note_SO[] attackTypes;

    public int enemyDamage => damage *= Game_Manager.instance.enemyDamageMultiplier;
}
