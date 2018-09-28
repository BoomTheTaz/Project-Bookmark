using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

	public Deck deck;
	public Hand hand;
	public Transform discard;
	public Transform trash;
	public bool isPlayer;
	public PlayArea playArea;

	CharacterData data;
    int currentAP;
    CombatUI UI;

	List<Card> DrawList;
	List<Card> HandList;
	List<Card> DiscardList;
	List<Card> TrashList;

	const float TimeBetweenDraws = 0.5f;

	private void Awake()
	{
        UI = FindObjectOfType<CombatUI>();
        data = GetComponent<CharacterData>();
        
        UI.ChangeAP(currentAP, isPlayer);
	}

    
	public void CreateDeck(int[] cards, GameObject CardPrefab)
	{
        currentAP = data.MaxAP;
        DrawList = new List<Card>();
        HandList = new List<Card>();
        DiscardList = new List<Card>();
        TrashList = new List<Card>();

		for (int i = 0; i < cards.Length; i++)
        {
            Card temp = Instantiate(CardPrefab, deck.transform.position, Quaternion.identity, deck.transform).GetComponent<Card>();

            DrawList.Add(temp);

            temp.FlipInstant();

            temp.SetupCard(CardTemplate.GetTemplate(cards[i]), data);

            temp.isPlayer = isPlayer;

			if (isPlayer == false)
            {
                temp.GetComponent<CanvasGroup>().blocksRaycasts = false;
                temp.name = "Enemy Card " + i.ToString();
                temp.SetEnemyBack();
            }
			else
				temp.name = "Card " + i.ToString();

        }

		deck.Shuffle();
  
	}
    
	// Fill hand to limit
	public void FillHand()
	{
        // Try for max hand size, break if no cards to draw
        int c = data.CardsInHand - HandList.Count;
        StartCoroutine(DrawCards(c));
	}

    // Draw a card from the deck and place in hand
    public void DrawCard()
	{
		Card temp = deck.DrawCard();
		DrawList.Remove(temp);
		HandList.Add(temp);

		hand.AddCard(temp);
	}

    public IEnumerator DrawCards(int c)
    {
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < c; i++)
        {
            DrawCard();
            
            yield return new WaitForSeconds(TimeBetweenDraws);
        }
    }
 
	public void TrashAndShuffle(Card toTrash)
    {
        if (isPlayer == false || playArea.CanShuffle() == true)
        {
			HandList.Remove(toTrash);
			TrashList.Add(toTrash);

            // TODO: Animate something for this card, maybe it just fades out or burns up or something
			toTrash.transform.SetParent(trash);
            toTrash.gameObject.SetActive(false);

            // Move all cards in hand back to deck
			HandList.Remove(toTrash);
            foreach (var c in HandList)
            {
                c.transform.SetParent(deck.transform);
                c.RegisterToFlip();
                c.RegisterToScale();
                c.RegisterToMove(Vector3.zero);
            }

            // Move all cards in discard back to deck
			foreach (var c in DiscardList)
            {
                c.transform.SetParent(deck.transform);
                c.RegisterToFlip();
                c.RegisterToMove(Vector3.zero);
            }

			deck.Shuffle();
        }
        else
            Debug.Log("CANNOT SHUFFLE AT THIS TIME");
    }

	public void DiscardCard(Card c)
	{
		HandList.Remove(c);
		DiscardList.Add(c);

		c.GetComponent<CanvasGroup>().blocksRaycasts = false;
		c.transform.SetParent(discard);
		c.RegisterToMove(Vector3.zero);
		c.RegisterToScale();
	}

    public void TrashCard(Card c)
	{
		HandList.Remove(c);
		TrashList.Add(c);

		c.GetComponent<CanvasGroup>().blocksRaycasts = false;
		c.transform.SetParent(trash);
		c.transform.localPosition = Vector3.zero;
		c.gameObject.SetActive(false);

        
	}

	public IEnumerator ForceDiscard(int n)
	{
        Debug.Log("FORCING DISCARD");
		for (int i = 0; i < n; i++)
		{
			Card temp = deck.DrawCard();

			// TODO: Figure out how to handle if no cards to draw
			// MAYBE: just be okay with it, more of a milling strategy
			if (temp == null)
				break;

			DrawList.Remove(temp);
			DiscardList.Add(temp);

			temp.GetComponent<CanvasGroup>().blocksRaycasts = false;
            temp.transform.SetParent(discard);
            temp.GetComponent<Canvas>().overrideSorting = true;
            temp.GetComponent<Canvas>().sortingOrder = i+1;
            temp.RegisterToMove(Vector3.zero);
            temp.RegisterToFlip();

			yield return new WaitForSeconds(TimeBetweenDraws);
		}
	}

    public int CurrentAP()
    {
        return currentAP;
    }

    public void UseAP(int i)
    {
        currentAP -= i;

        UI.ChangeAP(currentAP, isPlayer);
    }

    public void GetBackAP(int i)
    {
        currentAP += i;

        if (currentAP > data.MaxAP)
            currentAP = data.MaxAP;

        UI.ChangeAP(currentAP, isPlayer);
    }

    public void ResetAP()
    {
        currentAP = data.MaxAP;

        UI.ChangeAP(currentAP, isPlayer);

    }

    public List<Card> GetHand()
    {
        return HandList;
    }

    public void NewTurn()
    {
        // Add AP
        GetBackAP(data.TurnAP);
        FillHand();
    }
}
