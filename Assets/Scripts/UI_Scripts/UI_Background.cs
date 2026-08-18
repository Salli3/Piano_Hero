using UnityEngine;
using UnityEngine.UI;

public class UI_Background : MonoBehaviour
{
    [SerializeField] private Image background;

    [SerializeField] private Sprite normalBG;
    [SerializeField] private Color backgroundColor;

    [SerializeField] private Sprite bossBG;
    [SerializeField] private Color bossBackgroundColor;

    private void Start()
    {
        if (Game_Manager.instance.IsBossRound == true)
        {
            background.sprite = bossBG;
            background.color = bossBackgroundColor;
        }
        else
        {
            background.sprite = normalBG;
            background.color = backgroundColor;
        }
    }
}
