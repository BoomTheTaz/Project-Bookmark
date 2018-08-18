using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {


	// TODO: create deck in here, store cards in list/array, track discarded/trashed
	public GameObject CardPrefab;
	public bool isPlayer;
	public CharacterData data;
	public Transform TrashLocation;
	public PlayArea playArea;


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
	public Card DrawCard()
	{
		if (transform.childCount > 0)
		{	
			return transform.GetChild(0).GetComponent<Card>();
		}
		else
		{
			Debug.Log("Out of cards to draw.");
			return null;
		}
	}
    
   
}
