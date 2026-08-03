using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO")]
public class Player_SO : ScriptableObject
{
    public string playerName;
    public Sprite playerSprite;
    public int playerHP;
    public int playerDamage;
    public int startingMoney;
    public Note_SO[] attackTypes;
}
