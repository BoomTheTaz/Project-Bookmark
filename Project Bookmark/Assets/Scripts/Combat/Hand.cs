using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: Eliminate this class, unnecessary
public class Hand : MonoBehaviour {

	public Deck deck;
	Dropzone HandDropzone;

	private void Start()
	{
		HandDropzone = GetComponent<Dropzone>();
	}

	   
    public void DrawCard()
	{
		GameObject card = deck.DrawCard();

		HandDropzone.AddCard(card);
	}

     
}
