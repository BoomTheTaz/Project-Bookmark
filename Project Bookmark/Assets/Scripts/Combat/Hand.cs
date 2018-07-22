using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: Eliminate this class, unnecessary
public class Hand : Dropzone {


	public GameObject CardPrefab;

    // Add a card to the hand
    public void AddCard()
	{
		Instantiate(CardPrefab, this.transform);
		base.ReorganizeCards();
	}

     
}
