using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Deck deck;
	public Hand hand;
	public PlayArea playArea;

	int MaxCardsInHand = 5;

	Dropzone handDZ;

	// Use this for initialization
	void Start () {
		handDZ = hand.GetComponent<Dropzone>();

		while (hand.transform.childCount < MaxCardsInHand)
        {
            if (deck.transform.childCount == 0)
                break;
            handDZ.AddAICard(deck.DrawCard());
        }

		handDZ.ReorganizeCards();
	}
   
    public void Defend()
	{
		Debug.Log("Time to defend");

		while(hand.transform.childCount < 5)
		{
			if (deck.transform.childCount == 0)
				break;

			handDZ.AddAICard(deck.DrawCard());
		}

		playArea.AddCardToEnemyReveal(hand.GetHighestDEF().transform);

	}

	public void Attack()
    {
        Debug.Log("Time to attack");
		playArea.AddAICard(hand.GetHighestATK().gameObject);
    }
}
