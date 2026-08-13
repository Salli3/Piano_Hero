using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject chooseCharacterMenu;
    [SerializeField] private GameObject chooseDifficultyMenu;

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        chooseCharacterMenu.SetActive(false);
        chooseDifficultyMenu.SetActive(false);
    }

    public void OpenChooseCharacterMenu()
    {
        mainMenu.SetActive(false);
        chooseCharacterMenu.SetActive(true);
        chooseDifficultyMenu.SetActive(false);
    }

    public void OpenChooseDifficultyMenu()
    {
        mainMenu.SetActive(false);
        chooseCharacterMenu.SetActive(false);
        chooseDifficultyMenu.SetActive(true);
    }
}
