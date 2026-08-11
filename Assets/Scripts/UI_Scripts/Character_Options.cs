using UnityEngine;

public class Character_Options : MonoBehaviour
{
    [SerializeField] private Character_Slot[] characterSlots;
    [SerializeField] private Player_SO[] playerSOs;

    private void OnValidate()
    {
        if (characterSlots == null) return;
        if (playerSOs == null || playerSOs.Length != characterSlots.Length)
        {
            playerSOs = new Player_SO[characterSlots.Length];
        }

        for (int i = 0; i < characterSlots.Length; i++)
        {
            characterSlots[i].SetCharacterOption(playerSOs[i]);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < characterSlots.Length; i++)
        {
            characterSlots[i].SetCharacterOption(playerSOs[i]);
        }
    }
}
