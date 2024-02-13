using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadNoteInteract : AInteractableComponent
{
    [SerializeField] private string noteText;
    [Space]

    private UIManager uiManager;
    public string NoteText { get => noteText; }

    protected override void Start()
    {
        base.Start();

        uiManager = ServiceLocator.GetService<UIManager>();
    }

    protected override void OnInteract(PlayerController player)
    {
        uiManager.ChangeScreen("note");
        ServiceLocator.GetService<UINoteManager>().ReadNote(this);
    }
}
