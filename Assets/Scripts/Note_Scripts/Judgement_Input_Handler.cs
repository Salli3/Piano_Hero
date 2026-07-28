using UnityEngine;

public class Judgement_Input_Handler : MonoBehaviour
{
    [SerializeField] private Judgement_Box[] judgementBoxes;
    [SerializeField] private string[] triggerKeys;

    private void OnValidate()
    {
        if (judgementBoxes == null) return;
        if (triggerKeys == null || triggerKeys.Length != judgementBoxes.Length)
        {
            triggerKeys = new string[judgementBoxes.Length];
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        for (int i = 0; i < judgementBoxes.Length; i++)
        {
            if (string.IsNullOrEmpty(triggerKeys[i])) continue;
            if (Input.GetKeyDown(triggerKeys[i]))
            {
                judgementBoxes[i].TryHitNote();
            }
        }
    }
}