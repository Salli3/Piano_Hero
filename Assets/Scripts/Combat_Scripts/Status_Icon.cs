using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Status_Icon : MonoBehaviour
{
    [SerializeField] private Image statusImage;
    [SerializeField] private TMP_Text statusText;

    public void UpdateStatusIcon(Sprite sprite, string text)
    {
        statusImage.sprite = sprite;
        statusText.text = text;
    }
}
