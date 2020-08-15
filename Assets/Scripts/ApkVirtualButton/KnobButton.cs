using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnobButton : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public float maxCircleRadius = 40f;
    public float curCircleRadius;
    public float curEuler;

    public bool isEndDrag;

    private RectTransform childrectTransform;
    private RectTransform parentRectTransform;

    void Start()
    {
        parentRectTransform = GetComponent<RectTransform>();
        childrectTransform = parentRectTransform.GetChild(0).GetComponent<RectTransform>();
        childrectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isEndDrag = false;
        Vector2 pos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, Camera.main, out pos);
        curEuler = CalculateEuler(pos);
        if (!IsInCircle(pos))
        {
            pos = CalculatePos(curEuler);
        }
        curCircleRadius = CalculateRadius(pos);
        childrectTransform.anchoredPosition = pos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (childrectTransform != null)
            childrectTransform.anchoredPosition = Vector3.zero;
        isEndDrag = true;
        curEuler = 0f;
        curCircleRadius = 0f;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        ;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isEndDrag = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, Camera.main, out pos);
        curEuler = CalculateEuler(pos);
        if (!IsInCircle(pos))
        {
            pos = CalculatePos(curEuler);
        }
        curCircleRadius = CalculateRadius(pos);
        childrectTransform.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (childrectTransform != null)
            childrectTransform.anchoredPosition = Vector3.zero;
        isEndDrag = true;
        curEuler = 0f;
        curCircleRadius = 0f;
    }

    private bool IsInCircle(Vector2 pos)
    {
        return (pos.x * pos.x + pos.y * pos.y - maxCircleRadius * maxCircleRadius <= 0.01f);
    }

    private float CalculateEuler(Vector2 pos)
    {
        float radius = pos.magnitude;
        float euler = Mathf.Acos(pos.x / radius) * 180f / Mathf.PI;
        if (pos.y < 0f)
            euler = -euler;
        return euler;
    }

    private Vector2 CalculatePos(float euler)
    {
        return new Vector2(maxCircleRadius * Mathf.Cos(euler * Mathf.PI / 180f), maxCircleRadius * Mathf.Sin(euler * Mathf.PI / 180f));
    }

    private float CalculateRadius(Vector2 pos)
    {
        return pos.magnitude;
    }

    
}
