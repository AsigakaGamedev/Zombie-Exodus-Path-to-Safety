using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMovablePlayerItem : UIMovableInventoryItem
{
    protected override void OnClick(PointerEventData eventData)
    {
        UISelectedItemPopup selectedItemPopup = ServiceLocator.GetService<UIPopupsManager>().OpenPopup<UISelectedItemPopup>("selected_item");
        selectedItemPopup.SelectItem(item);
    }
}