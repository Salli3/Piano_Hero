using System.Collections;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image playerImage;

    [SerializeField] private UI_Status uiStatus;
    [SerializeField] private Hit_Number_Pool hitNumberPool;

    private void OnEnable()
    {
        Combat_Manager.DamagePlayer += hitNumberPool.ShowHitNumber;
        Combat_Manager.PlayerStatusChange += uiStatus.UpdateCombatStatus;
    }

    private void OnDisable()
    {
        Combat_Manager.DamagePlayer -= hitNumberPool.ShowHitNumber;
        Combat_Manager.PlayerStatusChange -= uiStatus.UpdateCombatStatus;
    }

    private void Start()
    {
        SetPlayerUI();
    }

    private void SetPlayerUI()
    {
        if (Game_Manager.instance.statsManager.Player == null) return;
        playerImage.sprite = Game_Manager.instance.statsManager.Player.playerSprite;
        nameText.text = Game_Manager.instance.statsManager.Player.playerName;
    }
}
