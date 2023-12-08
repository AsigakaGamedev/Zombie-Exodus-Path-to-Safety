using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIMovableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] private Image iconImg;

    [Space]
    [SerializeField] private Transform originParent;
    [SerializeField] private Transform dragParent;

    [Space]
    [Range(0, 1), SerializeField] private float dragAlpha = 0.6f;

    private Vector3 defaultPos;

    public void SetParents(Transform origin, Transform drag)
    {
        originParent = origin;
        dragParent = drag;
    }

    public void SetIcon(Sprite icon)
    {
        iconImg.sprite = icon;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        defaultPos = transform.position;
        transform.SetParent(dragParent);
        group.alpha = dragAlpha;
        group.blocksRaycasts = false;
        OnBegin();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originParent);
        group.alpha = 1;
        transform.position = defaultPos;
        group.blocksRaycasts = true;
        OnEnd();
    }

    protected virtual void OnBegin() { }
    protected virtual void OnDrag() { }
    protected virtual void OnEnd() { }
}
