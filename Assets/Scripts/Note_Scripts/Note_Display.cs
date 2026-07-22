using UnityEngine;
using TMPro;

public class Note_Display : MonoBehaviour
{
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private Note_SO noteData;
    [SerializeField] private string sortingLayer = "Default";
    [SerializeField] private int sortingOrder = 100;

    private void OnValidate()
    {
        Renderer rend = nameText.GetComponent<Renderer>();
        rend.sortingLayerName = sortingLayer;
        rend.sortingOrder = sortingOrder;

        if (noteData != null)
            SetNote(noteData);
    }

    public void SetNote(Note_SO note)
    {
        noteData = note;
        nameText.text = note.noteName;
    }
}