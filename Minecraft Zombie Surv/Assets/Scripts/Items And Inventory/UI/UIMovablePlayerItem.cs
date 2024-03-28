using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UIMovablePlayerItem : UIMovableInventoryItem
{
    private UIPopupsManager popupsManager;

    [Inject]
    private void Construct(UIPopupsManager popupsManager)
    {
        this.popupsManager = popupsManager;
    }

    protected override void OnClick(PointerEventData eventData)
    {
        UISelectedItemPopup selectedItemPopup = popupsManager.OpenPopup<UISelectedItemPopup>("selected_item");
        selectedItemPopup.SelectItem(item);
    }
}