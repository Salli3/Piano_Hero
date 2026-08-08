using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    [SerializeField] private GameObject blockStatus;
    [SerializeField] private TMP_Text blockText;

    [SerializeField] private GameObject stackingDamageStatus;
    [SerializeField] private TMP_Text stackingDamageText;

    [SerializeField] private GameObject boostAttackStatus;
    [SerializeField] private TMP_Text boostAttackText;

    public void UpdateCombatStatus(int blockNumber, int stackingDamageNumber, int boostAttackNumber)
    {
        UpdateStatus(blockStatus, blockText, blockNumber);
        UpdateStatus(stackingDamageStatus, stackingDamageText, stackingDamageNumber);
        UpdateStatus(boostAttackStatus, boostAttackText, boostAttackNumber);
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
