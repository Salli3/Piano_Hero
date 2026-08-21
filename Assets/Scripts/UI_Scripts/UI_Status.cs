using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Status : MonoBehaviour
{
    [SerializeField] private Status_Icon[] statusIcons;
    [SerializeField] private Sprite[] statusSprites;

    private void OnValidate()
    {
        if (statusIcons == null) return;
        if (statusSprites == null || statusSprites.Length != statusIcons.Length)
        {
            statusSprites = new Sprite[statusIcons.Length];
        }
    }

    public void UpdateCombatStatus(Status status)
    {
        UpdateStatus(statusIcons[0], statusSprites[0], status.block);
        UpdateStatus(statusIcons[1], statusSprites[1], status.damageStack);
        UpdateStatus(statusIcons[2], statusSprites[2], status.attackBoost);
    }

    private void UpdateStatus(Status_Icon status, Sprite sprite, int num)
    {
        if (num <= 0)
        {
            status.gameObject.SetActive(false);
        }
        else
        {
            status.gameObject.SetActive(true);
            status.UpdateStatusIcon(sprite, num.ToString());
        }
    }
}
