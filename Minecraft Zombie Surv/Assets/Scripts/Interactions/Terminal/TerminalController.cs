using System.Collections;
using UnityEngine;

public class TerminalController : AInteractableComponent
{
    [SerializeField] private string terminalLabelKey;

    [Space]
    [SerializeField] private TerminalComponent[] components;

    private UIManager uiManager;

    public string TerminalLabelKey { get => terminalLabelKey; }
    public TerminalComponent[] Components { get => components; }

    protected override void Start()
    {
        base.Start();

        uiManager = ServiceLocator.GetService<UIManager>();
    }

    protected override void OnSuccessInteract(PlayerController player)
    {
        uiManager.ChangeScreen("terminal");
        ServiceLocator.GetService<UITerminalManager>().OpenTerminal(this);
    }
}