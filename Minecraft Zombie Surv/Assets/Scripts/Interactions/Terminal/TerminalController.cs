using System.Collections;
using UnityEngine;
using Zenject;

public class TerminalController : AInteractableComponent
{
    [SerializeField] private string terminalLabelKey;

    [Space]
    [SerializeField] private TerminalComponent[] components;

    private UIManager uiManager;
    private UITerminalManager uiTerminalManager;

    public string TerminalLabelKey { get => terminalLabelKey; }
    public TerminalComponent[] Components { get => components; }

    [Inject]
    private void Construct(UIManager uiManager, UITerminalManager uiTerminalManager)
    {
        this.uiManager = uiManager;
        this.uiTerminalManager = uiTerminalManager;
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        uiManager.ChangeScreen("terminal");
        uiTerminalManager.OpenTerminal(this);
    }
}