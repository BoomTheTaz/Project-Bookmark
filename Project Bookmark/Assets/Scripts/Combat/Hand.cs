using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: Eliminate this class, unnecessary
public class Hand : MonoBehaviour {

	public Deck deck;
	Dropzone HandDropzone;

	private void Awake()
	{
		HandDropzone = GetComponent<Dropzone>();
	}

    public void AddCard(Card c)
	{
		HandDropzone.AddCard(c);
	}
	   
    public void DrawCard()
	{
		Card card = deck.DrawCard();

		HandDropzone.AddCard(card);
	}

    public void Reorganize()
	{
		HandDropzone.ReorganizeCards();
	}
}
