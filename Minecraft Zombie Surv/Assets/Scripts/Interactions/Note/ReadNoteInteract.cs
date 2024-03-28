using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ReadNoteInteract : AInteractableComponent
{
    [TextArea(1, 15), SerializeField] private string noteText;

    private UIManager uiManager;
    private UINoteManager uiNoteManager;

    public string NoteText { get => noteText; }

    [Inject]
    private void Construct(UIManager uiManager, UINoteManager uiNoteManager)
    {
        this.uiManager = uiManager;
        this.uiNoteManager = uiNoteManager;
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        uiManager.ChangeScreen("note");
        uiNoteManager.ReadNote(this);
    }
}
