using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINoteManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noteText;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void ReadNote(ReadNoteInteract noteData)
    {
        noteText.text = noteData.NoteText;
    }
}
