using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    
    public void Shuffle()
	{
		
		for (int i = 0; i < transform.childCount-1; i++)
		{

			int rand = Random.Range(i, transform.childCount);

			transform.GetChild(rand).SetSiblingIndex(i);
			transform.GetChild(i+1).SetSiblingIndex(rand);         
            
		}

	}

    
    // Draw a card from the deck
	public GameObject DrawCard()
	{
		if (transform.childCount > 0)
			return transform.GetChild(0).gameObject;
		else
		{
			Debug.Log("Out of cards to draw.");
			return null;
		}
	}

}
