using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TerminalComponent : MonoBehaviour
{
    [SerializeField] private string labelKey;
    [SerializeField] private string contentKey;

    [Space]
    public UnityEvent onPress;

    public string LabelKey { get => labelKey; }
    public string ContentKey { get => contentKey; }
}