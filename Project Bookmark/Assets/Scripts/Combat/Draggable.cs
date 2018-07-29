using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	Vector2 offset;
	Vector3 TargetPosition;
	Dropzone previousDropzone;
	Dropzone currentDropzone;

	public void OnBeginDrag(PointerEventData eventData)
	{
		TargetPosition = transform.position;

		offset = eventData.position - (Vector2)transform.position;

		eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;

		SetCurrentDropzone(transform.parent.GetComponent<Dropzone>());
		previousDropzone = transform.parent.GetComponent<Dropzone>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position - offset;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
        
		Dropped();      
	}

	public void SetTargetPosition(Vector3 v)
	{
		TargetPosition = v;
	}

	public void SetCurrentDropzone(Dropzone d)
	{
		if (currentDropzone == d)
			return;
        
		currentDropzone = d;
	}

    public void GoToDropzone()
	{
		GetComponent<RectTransform>().pivot = transform.parent.GetComponent<RectTransform>().pivot;


		currentDropzone.ReorganizeCards();
	}
    
    void Dropped()
	{
		if (previousDropzone != currentDropzone)
		{
			transform.SetParent(currentDropzone.transform);
            
			previousDropzone.ReorganizeCards();
			previousDropzone.RemoveCardAP(GetComponent<Card>().AP);
			currentDropzone.AddCardAP(GetComponent<Card>().AP);
		}

		GoToDropzone();
	}
}
