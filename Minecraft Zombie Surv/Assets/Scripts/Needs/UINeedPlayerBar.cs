using UnityEngine;
using UnityEngine.UI;

public class UINeedPlayerBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [Space]
    [SerializeField] private string needID;

    private PlayerController player;
    private NeedData linkedNeed;

    private void Start()
    {
        player = ServiceLocator.GetService<PlayerController>();
        linkedNeed = player.Needs.GetNeed(needID);

        slider.maxValue = linkedNeed.MaxValue;
        slider.value = linkedNeed.Value;

        linkedNeed.onNeedValueChange += OnNeedValueChange;
    }

    private void OnDestroy()
    {
        linkedNeed.onNeedValueChange -= OnNeedValueChange;
    }

    private void OnNeedValueChange(float value)
    {
        slider.value = value;
    }
}