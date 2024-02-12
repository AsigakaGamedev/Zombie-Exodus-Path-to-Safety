using UnityEngine;
using UnityEngine.UI;

public class UINeedProgressBar : MonoBehaviour
{
    [SerializeField] private NeedsController needsController;
    [SerializeField] private Slider slider;

    [Space]
    [SerializeField] private string needID;

    private NeedData linkedNeed;

    private void Start()
    {
        linkedNeed = needsController.GetNeed(needID);

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