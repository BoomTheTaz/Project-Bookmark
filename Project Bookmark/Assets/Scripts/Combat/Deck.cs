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
    
	List<Card> Draw;
	List<Card> Hand;
	List<Card> Discard;
	List<Card> Trash;

    
	public void CreateDeck(int[] cards)
	{
		Draw = new List<Card>();
		Hand = new List<Card>();
		Discard = new List<Card>();
		Trash = new List<Card>();

		if (isPlayer == true)
		{
			for (int i = 0; i < cards.Length; i++)
			{
				Card temp = Instantiate(CardPrefab, transform.position, Quaternion.identity, transform).GetComponent<Card>();

				Draw.Add(temp);
                
				temp.FlipInstant();

				temp.name = "Card " + i.ToString();

				temp.SetupCard(CardTemplate.GetTemplate(cards[i]),data);

				temp.isPlayer = true;
			}
		}

		else
		{
			for (int i = 0; i < cards.Length; i++)
            {
				Card temp = Instantiate(CardPrefab, transform.position, Quaternion.identity, transform).GetComponent<Card>();

				Draw.Add(temp);

                temp.GetComponent<CanvasGroup>().blocksRaycasts = false;
                
                temp.FlipInstant();
                
                temp.name = "Enemy Card " + i.ToString();

				temp.SetupCard(CardTemplate.GetTemplate(cards[i]), data);
                temp.SetEnemyBack();
            }
		}
        
		Shuffle();
	}

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
		{
			Card toDraw = transform.GetChild(0).GetComponent<Card>();
			Draw.Remove(toDraw);
			Hand.Add(toDraw);

			return toDraw.gameObject;
		}
		else
		{
			Debug.Log("Out of cards to draw.");
			return null;
		}
	}
    
	public void TrashAndShuffle(Card toTrash)
	{
		if (toTrash.isPlayer == false || playArea.CanShuffle() == true)
		{
			Hand.Remove(toTrash);
			Trash.Add(toTrash);

			// TODO: Animate something for this card, maybe it just fades out or burns up or something
			toTrash.transform.SetParent(TrashLocation);
			toTrash.gameObject.SetActive(false);

			// Move all cards in hand back to deck
			foreach (var c in Hand)
			{
				c.transform.SetParent(transform);
				c.RegisterToFlip();
				c.RegisterToScale();
				c.RegisterToMove(Vector3.zero);
			}

			// Move all cards in discard back to deck
			foreach (var c in Discard)
			{
				c.transform.SetParent(transform);
				c.RegisterToFlip();
				c.RegisterToMove(Vector3.zero);
			}

			Shuffle();
			Debug.Log("TRASHIN AND SHUFFLIN");
		}
		else
			Debug.Log("CANNOT SHUFFLE AT THIS TIME");
	}

    public void DiscardCard(Card c)
	{
		Hand.Remove(c);
		Discard.Add(c);
	}

}
