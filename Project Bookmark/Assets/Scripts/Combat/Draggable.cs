﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	Vector2 offset;
	Dropzone previousDropzone;
	Dropzone currentDropzone;

	Transform previousTransform;

	#region Drag Functions
	// What to do at the beginning of the drag
	public void OnBeginDrag(PointerEventData eventData)
	{
		// Set target transform to current transform
		offset = eventData.position - (Vector2)transform.position;

		eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = false;

		previousDropzone = currentDropzone;
		
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

#endregion
 
	public void SetCurrentDropzone(Dropzone d)
	{
		currentDropzone = d;
		//SetNewTransform(d.GetRelevantTransform());
	}
    
 //   public void GoToDropzone()
	//{
	//	//GetComponent<RectTransform>().pivot = transform.parent.GetComponent<RectTransform>().pivot;
	//	// if null


	//	if (currentDropzone == previousDropzone)
	//		//transform.position = previousPosition;
	//		GetComponent<Card>().RegisterToMove(previousPosition);
	//	else
	//	{
	//		currentDropzone.ReorganizeCards();
	//	}
	//	    //transform.position = targetTransform.position;

        
	//	//if (currentDropzone.transform == targetTransform )
	//	    //currentDropzone.ReorganizeCards();
	//}
    
    void Dropped()
	{
		

		if (previousDropzone != currentDropzone)
		{
			//transform.SetParent(targetTransform);

			currentDropzone.AddCardAP(GetComponent<Card>().AP);
			// ========================== DO SOMETHING WITH THIS, 
			// ========================== GIVE PLAY AREA CARD AND LET IT DECIDE HOW THE CARD IS PLAYED
			currentDropzone.PlaceCard(GetComponent<Card>());
			// =======================

			previousDropzone.ReorganizeCards();
			previousDropzone.RemoveCardAP(GetComponent<Card>().AP);

			//currentDropzone = previousDropzone;


		}
		else if (transform.parent.GetComponent<Dropzone>() == null)
			GetComponent<Card>().RegisterToMove(Vector3.zero);
		else
		{
			previousDropzone.ReorganizeCards();
		}
		//GoToDropzone();
	}
}
