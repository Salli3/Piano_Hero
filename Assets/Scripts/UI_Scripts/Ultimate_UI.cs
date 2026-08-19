using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ultimate_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text ultimateText;
    [SerializeField] private CanvasGroup ultimateCG;
    [SerializeField] private Image image;
    [SerializeField] private Animator anim;

    private void Start()
    {
        ultimateCG.alpha = 0;
    }

    public void ShowUltimateEffect(Sprite userSprite, string name, string ultName)
    {
        Game_Manager.instance.PauseNote();
        ultimateCG.alpha = 1;
        nameText.text = name;
        ultimateText.text = ultName;
        image.sprite = userSprite;
        anim.Play("Ult");
    }

    public void EndAnim()
    {
        ultimateCG.alpha = 0;
        Game_Manager.instance.ContinueNote();
    }
}
