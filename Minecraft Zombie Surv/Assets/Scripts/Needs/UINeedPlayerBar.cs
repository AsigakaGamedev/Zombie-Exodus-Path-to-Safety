using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UINeedPlayerBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [Space]
    [SerializeField] private string needID;

    private PlayerController player;
    private NeedData linkedNeed;

    [Inject]
    private void Construct(PlayerController player)
    {
        this.player = player;
    }

    private void Start()
    {
        linkedNeed = player.Needs.GetNeed(needID);

        slider.maxValue = linkedNeed.MaxValue;
        slider.value = linkedNeed.Value;

        linkedNeed.onNeedValueChange += OnNeedValueChange;
    }

    private void OnDestroy()
    {
        if (linkedNeed != null) linkedNeed.onNeedValueChange -= OnNeedValueChange;
    }

    private void OnNeedValueChange(float value)
    {
        slider.value = value;
    }
}