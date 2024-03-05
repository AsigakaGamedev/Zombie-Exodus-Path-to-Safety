using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNoteInteract : AInteractableComponent
{
    [TextArea(1, 15), SerializeField] private string noteText;

    private UIManager uiManager;

    public string NoteText { get => noteText; }

    protected override void Start()
    {
        base.Start();

        uiManager = ServiceLocator.GetService<UIManager>();
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        uiManager.ChangeScreen("note");
        ServiceLocator.GetService<UINoteManager>().ReadNote(this);
    }
}
