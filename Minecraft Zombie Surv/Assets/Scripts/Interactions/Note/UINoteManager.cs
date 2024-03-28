using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINoteManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noteText;

    public void ReadNote(ReadNoteInteract noteData)
    {
        noteText.text = noteData.NoteText;
    }
}
