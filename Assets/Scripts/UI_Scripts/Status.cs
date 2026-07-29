using TMPro;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private GameObject blockStatus;
    [SerializeField] private TMP_Text blockText;

    [SerializeField] private GameObject stackingDamageStatus;
    [SerializeField] private TMP_Text stackingDamageText;

    [SerializeField] private GameObject curseStatus;
    [SerializeField] private TMP_Text curseText;

    public void UpdateCombatStatus(int blockNumber, int stackingDamageNumber, int curseNumber)
    {
        UpdateStatus(blockStatus, blockText, blockNumber);
        UpdateStatus(stackingDamageStatus, stackingDamageText, stackingDamageNumber);
        UpdateStatus(curseStatus, curseText, curseNumber);
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
