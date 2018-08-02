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

	public Card GetHighestDEF()
    {
        Card result = null;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (result == null)
                result = transform.GetChild(i).GetComponent<Card>();
            else if (transform.GetChild(i).GetComponent<Card>().DEF > result.DEF)
                result = transform.GetChild(i).GetComponent<Card>();
        }
		Debug.Log(result.DEF);
        return result;
    }

}
