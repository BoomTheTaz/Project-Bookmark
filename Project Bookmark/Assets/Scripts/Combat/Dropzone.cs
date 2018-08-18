using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Dropzone : MonoBehaviour, IPointerEnterHandler {

	protected float Width;
	protected float Height;
	protected float usableSpace;


    // Use this for initialization
    protected void Start()
    {
        Width = GetComponent<RectTransform>().rect.width;
        Height = GetComponent<RectTransform>().rect.height;
		usableSpace = Width - Card.Width;
        
    }

    

	//public void OnDrop(PointerEventData eventData)
	//{        
	//	Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
           
	//	if (draggable != null)
	//	{
	//		if (CanAddCard(draggable.GetComponent<Card>().AP))
	//		{
	//			Debug.Log("WE HAVE ENOUGH AP.");
	//			draggable.transform.SetParent(transform);
	//			draggable.SetCurrentDropzone(this);
	//		}
	//		else
	//		{
	//			Debug.Log("AHHHH!!! WE'RE OUT OF AP");
	//		}



	//	}
	//}

	public void OnPointerEnter(PointerEventData eventData)
	{
		// return if no dragging object
		if (eventData.pointerDrag == null)
			return;
		Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();

        // if there is a draggable
        if (draggable != null)
        {
			// and has enough AP to add
			if (CanAddCard(draggable.GetComponent<Card>().AP))
			{
				//Debug.Log("WE HAVE ENOUGH AP.");
                //draggable.transform.SetParent(transform);
                draggable.SetCurrentDropzone(this);
            }
            else
            {
                //Debug.Log("AHHHH!!! WE'RE OUT OF AP");
            }



        }
	}
      
    public void ReorganizeCards()
	{
		
		// FIXME: this should not need to happen, part of loading combat
		if (usableSpace == Width)
		{
			usableSpace = Width - Card.Width;
		}

		int numChildren = transform.childCount;
        float xShift = usableSpace / numChildren;
        float mid = numChildren / 2f;
        
        // Organize by sibling index
        for (int i = 0; i < numChildren; i++)
        {
			transform.GetChild(i).GetComponent<Card>().RegisterToMove(new Vector3((i - mid + .5f) * xShift, 0, 0));

			//transform.GetChild(i).transform.localPosition = new Vector3((i - mid + .5f) * xShift, 0, 0);
			//transform.GetChild(i).transform.position = transform.position + new Vector3((i - mid + .5f) * xShift, 0, 0);
        }

	}

    // Add a card to this dropzone
	public void AddCard(GameObject card)
	{
		if (card == null)
			return;

		card.transform.SetParent(transform);
        
        // Only flip player cards
		if (card.GetComponent<Card>().isPlayer == true)
		    card.GetComponent<Card>().RegisterToFlip();
		
		card.GetComponent<Card>().RegisterToScale(true);

		card.GetComponent<Draggable>().SetCurrentDropzone(this);
        
		ReorganizeCards();

  
	}

	public void AddAICard(GameObject card)
	{
		if (card == null)
            return;
        
        card.transform.SetParent(transform);
        
		card.GetComponent<Card>().RegisterToScale();
        ReorganizeCards();
	}

	protected virtual bool CanAddCard(int ap)
	{
		return true;
	}

	public virtual void IncreaseAP(int ap)
	{
		return;
	}

	public virtual void DecreaseAP(int ap)
    {
        return;
    }

	public virtual Transform GetRelevantTransform()
    {
		return transform;
    }


    // Place card in proper spot when placed on dropzone
	// NOTE: Maybe add bool parameters for flip and scale
	public virtual void PlaceCard(Card card)
	{
        // Set the parent
		card.transform.SetParent(transform);
		card.GetComponent<Draggable>().SetCurrentDropzone(this);

		// Reorganize dropzone, takes care of movement
		ReorganizeCards();

	}
}
