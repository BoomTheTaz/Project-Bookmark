using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Dropzone : MonoBehaviour, IDropHandler {

	protected float Width;
	protected float Height;
	protected float usableSpace;


    // Use this for initialization
    void Start()
    {
        
        Width = GetComponent<RectTransform>().rect.width;
        Height = GetComponent<RectTransform>().rect.height;
		usableSpace = Width - Card.Width;
        
    }

    

	public void OnDrop(PointerEventData eventData)
	{        
		Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
           
		if (draggable != null)
		{
			draggable.transform.SetParent(transform);
			ReorganizeCards();
		}
	}

    protected void ReorganizeCards()
	{
		
		// FIXME: this should not need to happen, part of loading combat
		if (usableSpace == Width)
		{
			Debug.Log("HERE");
			usableSpace = Width - Card.Width;
		}

		int numChildren = transform.childCount;
        float xShift = usableSpace / numChildren;
        float mid = numChildren / 2f;

        // Organize by sibling index
        for (int i = 0; i < numChildren; i++)
        {
			transform.GetChild(i).transform.localPosition = new Vector3((i - mid + .5f) * xShift, Height/2, 0);
        }

	}
}
