using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	Vector2 offset;
	Dropzone previousDropzone;
	Dropzone currentDropzone;

	Transform targetTransform;
	Vector3 previousPosition;

    // What to do at the beginning of the drag
	public void OnBeginDrag(PointerEventData eventData)
	{
		// Set target transform to current transform
		previousPosition = transform.position;

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

	public void SetNewTransform(Transform t)
	{
		targetTransform = t;
	}

	public void SetCurrentDropzone(Dropzone d)
	{
		if (currentDropzone == d)
			return;
        
		currentDropzone = d;

		SetNewTransform(d.GetRelevantTransform());
	}
    
    public void GoToDropzone()
	{
		//GetComponent<RectTransform>().pivot = transform.parent.GetComponent<RectTransform>().pivot;
		// if null


		if (currentDropzone == previousDropzone)
			transform.position = previousPosition;
		else
		    transform.position = targetTransform.position;

        
		if (currentDropzone.transform == targetTransform )
		    currentDropzone.ReorganizeCards();
	}
    
    void Dropped()
	{
		if (previousDropzone != currentDropzone)
		{
			transform.SetParent(targetTransform);

            currentDropzone.AddCardAP(GetComponent<Card>().AP);
            
			previousDropzone.ReorganizeCards();
			previousDropzone.RemoveCardAP(GetComponent<Card>().AP);
            
		}

		GoToDropzone();
	}
}
