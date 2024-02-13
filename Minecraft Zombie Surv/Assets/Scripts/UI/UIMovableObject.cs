using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UIMovableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private CanvasGroup group;
    [SerializeField] protected Image iconImg;

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

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        defaultPos = transform.position;
        transform.SetParent(dragParent);
        group.alpha = dragAlpha;
        group.blocksRaycasts = false;
        OnBegin(eventData);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originParent);
        group.alpha = 1;
        transform.position = defaultPos;
        group.blocksRaycasts = true;
        OnEnd(eventData);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnClick(eventData);
    }

    protected virtual void OnBegin(PointerEventData eventData) { }
    protected virtual void OnDrag(PointerEventData eventData) { }
    protected virtual void OnEnd(PointerEventData eventData) { }
    protected virtual void OnClick(PointerEventData eventData) { }
}
