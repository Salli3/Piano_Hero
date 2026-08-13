using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    [SerializeField] private GameObject blockStatus;
    [SerializeField] private TMP_Text blockText;

    [SerializeField] private GameObject damageStackStatus;
    [SerializeField] private TMP_Text damageStackText;

    [SerializeField] private GameObject attackBoostStatus;
    [SerializeField] private TMP_Text attackBoostText;

    public void UpdateCombatStatus(Status status)
    {
        UpdateStatus(blockStatus, blockText, status.block);
        UpdateStatus(damageStackStatus, damageStackText, status.damageStack);
        UpdateStatus(attackBoostStatus, attackBoostText, status.attackBoost);
    }

    private void UpdateStatus(GameObject status, TMP_Text text, int num)
    {
        if (num <= 0)
        {
            status.SetActive(false);
        }
        else
        {
            status.SetActive(true);
            text.text = num.ToString();
        }
    }
}
