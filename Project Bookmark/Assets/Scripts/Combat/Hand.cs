﻿using System.Collections;
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
    
	public Card GetHighestATK()
	{
		Card result = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			if (result == null)
				result = transform.GetChild(i).GetComponent<Card>();
			else if (transform.GetChild(i).GetComponent<Card>().ATK > result.ATK)
				result = transform.GetChild(i).GetComponent<Card>();
		}

		return result;
	}

	public Card GetHighestDEF(int APLimit)
    {
        Card result = null;
        for (int i = 0; i < transform.childCount; i++)
        {
			Card test = transform.GetChild(i).GetComponent<Card>();

			if (test.AP > APLimit)
				continue;
			
            if (result == null)
                result = transform.GetChild(i).GetComponent<Card>();
            else if (transform.GetChild(i).GetComponent<Card>().DEF > result.DEF)
                result = transform.GetChild(i).GetComponent<Card>();
        }

        return result;
    }

    public void Reorganize()
	{
		HandDropzone.ReorganizeCards();
	}
}
