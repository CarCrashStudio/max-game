using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public Canvas canvas;
    protected RectTransform rectTransform;
    protected CanvasGroup canvasGroup;
    protected CanvasGroup[] canvasGroups;

    private void Awake()
    {
        if (TryGetComponent<RectTransform>(out var rt)) { rectTransform = rt; }
        if (TryGetComponent<CanvasGroup>(out var cg)) { canvasGroup = cg; }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //Debug.Log($"{eventData.delta} {canvas.scaleFactor}");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }
}