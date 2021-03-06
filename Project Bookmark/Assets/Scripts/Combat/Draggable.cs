﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

	Vector2 offset;
	Dropzone previousDropzone;
	Dropzone currentDropzone;

	Transform previousTransform;

    Card card;

    private void Awake()
    {
        card = GetComponent<Card>();
        
    }

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
	}
    

    void Dropped()
	{
        if (previousDropzone != currentDropzone)
        {
            //transform.SetParent(targetTransform);

            /////currentDropzone.DecreaseAP(card.AP);
            // ========================== DO SOMETHING WITH THIS, 
            // ========================== GIVE PLAY AREA CARD AND LET IT DECIDE HOW THE CARD IS PLAYED
            /////currentDropzone.PlaceCard(card);
            // =======================

            currentDropzone.PlayCard(card);

            previousDropzone.UnplayCard(card);

            /////previousDropzone.ReorganizeCards();
           ///// previousDropzone.IncreaseAP(GetComponent<Card>().AP);

            //currentDropzone = previousDropzone;


        }
        // in card reveal area
        else if (transform.parent.GetComponent<Dropzone>() == null)
        {
            card.RegisterToMove(Vector3.zero);
            
        }
        else
        {
            previousDropzone.ReorganizeCards();
        }
	}
}
