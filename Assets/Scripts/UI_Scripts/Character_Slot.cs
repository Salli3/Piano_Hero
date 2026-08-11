using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Menu_Manager menuManager;
    [SerializeField] private Player_SO playerSO;
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMP_Text statsText;
    [SerializeField] private TMP_Text notesText;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SetCharacter);
        infoPanel.gameObject.SetActive(false);
    }

    public void SetCharacterOption(Player_SO playerSO)
    {
        this.playerSO = playerSO;
        characterImage.sprite = playerSO.playerSprite;
        characterName.text = playerSO.playerName;
        SetCharacterInfo();
    }

    private void SetCharacterInfo()
    {
        statsText.text =
            $"HP: {playerSO.playerHP}\n" +
            $"Damage: {playerSO.playerDamage}\n" +
            $"Money: {playerSO.startingMoney}\n" +
            $"Attack move:";

        notesText.text = "";
        foreach (var note in playerSO.attackTypes)
        {
            notesText.text += 
                $"\n-{note.noteName}:" +
                $"\n{note.GetDescription(0)}\n";
        }
    }

    private void SetCharacter()
    {
        Game_Manager.instance.PickCharacter(playerSO);
        menuManager.OpenChooseDifficultyMenu();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.gameObject.SetActive(false);
    }
}
