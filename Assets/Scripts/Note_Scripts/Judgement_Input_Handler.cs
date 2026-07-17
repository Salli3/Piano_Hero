using UnityEngine;

public class Judgement_Input_Handler : MonoBehaviour
{
    [SerializeField] private Judgement_Box[] judgementBoxes;

    [SerializeField] private KeyCode[] triggerKeys;

    private void OnValidate()
    {
        if (judgementBoxes == null) return;

        if (triggerKeys == null || triggerKeys.Length != judgementBoxes.Length)
        {
            triggerKeys = new KeyCode[judgementBoxes.Length];
        }
    }

    private void Update()
    {
        for (int i = 0; i < judgementBoxes.Length; i++)
        {
            if (Input.GetKeyDown(triggerKeys[i]))
            {
                judgementBoxes[i].TryHitNote();
            }
        }
    }
}
