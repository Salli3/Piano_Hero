using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judgement_Input_Handler : MonoBehaviour
{
    [SerializeField] private Judgement_Box[] judgementBoxes;
    private void Update()
    {
        foreach (var box in judgementBoxes)
        {
            if (Input.GetKeyDown(box.triggerKey))
            {
                box.GetOverLappingNote();
            }
        }
    }
}
