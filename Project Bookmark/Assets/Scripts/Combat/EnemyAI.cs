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

		//while (hand.transform.childCount < MaxCardsInHand)
  //      {
  //          if (deck.transform.childCount == 0)
  //              break;
  //          handDZ.AddAICard(deck.DrawCard());
  //      }

		//handDZ.ReorganizeCards();
	}


	int APLimit;
	   
    public void Defend(int ap)
	{
		APLimit = ap;
		StartCoroutine("DecideDefense");

	}

	IEnumerator DecideDefense()
	{   
		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		// TODO: ADD ACTUAL LOGIC TO THIS
		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         
		//while (hand.transform.childCount < 5)
   //     {
   //         if (deck.transform.childCount == 0)
   //             break;

			//hand.DrawCard();

			//yield return new WaitForSeconds(0.5f);
        //}

		yield return new WaitForSeconds(Random.Range(3, 10) / 10f);

		playArea.AddCardToEnemyReveal(hand.GetHighestDEF(APLimit).transform);
		hand.Reorganize();

	}

	public void Attack()
    {
        //Debug.Log("Time to attack");
		StartCoroutine("DecideAttack");
    }

	IEnumerator DecideAttack()
	{
		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // TODO: ADD ACTUAL LOGIC TO THIS
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~      

		yield return new WaitForSeconds(Random.Range(5, 15) / 10f);
		playArea.AddCard(hand.GetHighestATK());
		hand.Reorganize();

        // SHOULD EVALUATE TO CreateAttackDeck, which will then register draw
		CombatManager.instance.EvaluateOnEndMove();

	}
}
