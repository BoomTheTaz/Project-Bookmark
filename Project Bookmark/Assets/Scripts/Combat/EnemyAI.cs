using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Deck deck;
	public Hand hand;
	public PlayArea playArea;
    public Transform PlayerCardReveal;

	int MaxCardsInHand = 5;

    CardManager CM;

    BaseAI AI;

    Card[] AttackPlan;
    int attackCounter;
    // DIVIDE BY AP COST..... maybe


	// Use this for initialization
	void Start () {

        // TEMPORARY HARD CODE OF AI
        // TODO: Decide AI when setting up battle
        SetAI(new AttackAI());
        CM = GetComponent<CardManager>();
        attackCounter = 0;
        AttackPlan = null;
	}


	int APLimit;
	   
    public void SetAI(BaseAI a)
    {
        AI = a;
    }

    public void Defend(int ap)
	{
		APLimit = ap;
		StartCoroutine("DecideDefense");

	}

	IEnumerator DecideDefense()
	{
		yield return new WaitForSeconds(Random.Range(3, 10) / 10f);

        Card defense = AI.DecideDefense(PlayerCardReveal.GetChild(0).GetComponent<Card>(),CM.GetHand());

        if (defense == null)
            NoDefense();

        else
        {
            playArea.AddCardToEnemyReveal(defense);
            hand.Reorganize();
        }
	}

	public void Attack()
    {
        //Debug.Log("Time to attack");
        if (AttackPlan == null)
            AttackPlan = AI.DecideAttack(CM.CurrentAP(),CM.GetHand());

		StartCoroutine("DecideAttack");
    }

	IEnumerator DecideAttack()
	{
        // Delay while "Deciding" attack
		yield return new WaitForSeconds(Random.Range(5, 15) / 10f);

        // Pass turn if nothing to do
        if (AttackPlan == null || attackCounter == AttackPlan.Length)
        {
            PassTurn();
        }
        else
        {
            // Play next attack card and increment counter
            playArea.PlaceCard(AttackPlan[attackCounter]);
            attackCounter++;

            // Make sure hand is reorganized
            hand.Reorganize();
        }
	}

    void NoDefense()
    {
        CombatManager.instance.NoDefense();
    }

    void PassTurn()
    {
        // TODO: Tell combat manager, which will handle drawing, adding ap, etc.
        Debug.Log("Passing Turn");
        AttackPlan = null;
        attackCounter = 0;
    }
}
