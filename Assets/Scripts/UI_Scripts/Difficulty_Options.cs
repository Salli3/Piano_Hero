using UnityEngine;

public class Difficulty_Options : MonoBehaviour
{
    [SerializeField] private Difficulty_Slot[] difficultySlots;
    [SerializeField] private Difficulty_Setting[] difficultySettings;

    private void OnValidate()
    {
        if (difficultySlots == null) return;
        if (difficultySettings == null || difficultySettings.Length != difficultySlots.Length)
        {
            difficultySettings = new Difficulty_Setting[difficultySlots.Length];
        }

        for (int i = 0; i < difficultySlots.Length; i++)
        {
            difficultySlots[i].SetDifficultyOption(difficultySettings[i]);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < difficultySlots.Length; i++)
        {
            difficultySlots[i].SetDifficultyOption(difficultySettings[i]);
        }
    }

    [System.Serializable]
    public class Difficulty_Setting
    {
        public string difficultyName;
        public Color difficultyColor;
        public float noteSpeed;
        public int enemyPerRound;
        public int enemyHpMultiplier;
        public int enemyDamageMultiplier;
    }
}
