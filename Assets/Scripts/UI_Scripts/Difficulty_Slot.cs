using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Difficulty_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text difficultyName;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMP_Text infoText;

    private int enemyPerRound;
    private float noteSpeed; 
    private float enemyHpMultiplier;
    private float enemyDamageMultiplier;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SetDifficulty);
        infoPanel.gameObject.SetActive(false);
    }

    public void SetDifficultyOption(Difficulty_Options.Difficulty_Setting difficultySetting)
    {
        difficultyName.text = difficultySetting.difficultyName;
        infoPanel.GetComponent<Image>().color = difficultySetting.difficultyColor; 
        enemyPerRound = difficultySetting.enemyPerRound;
        noteSpeed = difficultySetting.noteSpeed;
        enemyHpMultiplier = difficultySetting.enemyHpMultiplier;
        enemyDamageMultiplier = difficultySetting.enemyDamageMultiplier;
        SetDifficultyInfo();
    }

    private void SetDifficultyInfo()
    {
        infoText.text = 
            $"-Note Speed: {noteSpeed*100}%\n" +
            $"\n-Enemy Per Round: {enemyPerRound}\n" +
            $"\n-Enemy HP Multiplier: {enemyHpMultiplier*100}%\n" +
            $"\n-Enemy Damage Multiplier: {enemyDamageMultiplier*100}%\n";
    }

    private void SetDifficulty()
    {
        Game_Manager.instance.SetDifficulty(enemyPerRound, noteSpeed, enemyHpMultiplier, enemyDamageMultiplier);
        Game_Manager.instance.fadeUI.FadeOut();
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
