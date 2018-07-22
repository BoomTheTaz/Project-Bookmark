using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	Vector2 offset;
	Vector3 TargetPosition;

	public void OnBeginDrag(PointerEventData eventData)
	{
		TargetPosition = transform.position;

		offset = eventData.position - (Vector2)transform.position;

		eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position - offset;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void SetTargetPosition(Vector3 v)
	{
		TargetPosition = v;
	}
}
