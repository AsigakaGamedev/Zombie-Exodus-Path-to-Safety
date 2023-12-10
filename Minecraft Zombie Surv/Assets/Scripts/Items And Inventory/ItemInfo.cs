using System.Collections;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private Sprite itemCellSprite;

    [Space]
    [SerializeField] private string itemNameKey;
    [SerializeField] private string itemDescriptionKey;

    public Sprite ItemCellSprite { get => itemCellSprite; }
    public string ItemNameKey { get => itemNameKey; }
    public string ItemDescriptionKey { get => itemDescriptionKey; }
}