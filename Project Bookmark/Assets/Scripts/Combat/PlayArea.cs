using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : Dropzone {

	public int CurrentAP { get; protected set; }
	public int MaxAP;
	public Transform PlayerReveal;
	public Transform EnemyReveal;
	public Dropzone PlayerHand;

    public CardManager PlayerCM;
    
	private new void Start()
	{
		base.Start();
        MaxAP = PlayerCM.CurrentAP();

    }


    protected override bool CanAddCard(int ap)
	{
		if (PlayerCM.CurrentAP() >= ap /*|| CombatManager.CurrentState == CombatState.Player_DEF*/)
        {
			// DO THIS SHIT

    		if (CombatManager.CurrentState == CombatState.Player_ATK)
            {
				return true;
            }

			// FIXME: Probably redundant state check
			if (CombatManager.CurrentState == CombatState.Player_DEF && ap <= EnemyReveal.GetChild(0).GetComponent<Card>().AP)
            {
                return true;
            }
            else
                Debug.Log("Not a fast enough card");

		}
		return false;
	}

  	public void AddCardToEnemyReveal(Transform t)
	{
		t.SetParent(EnemyReveal);
		t.GetComponent<Card>().RegisterToMove(Vector3.zero);
		t.GetComponent<Card>().RegisterToFlip();

	}

	public override void PlaceCard(Card card)
    {

		if (CombatManager.CurrentState == CombatState.Player_ATK)
        {            
            // Set parent
			card.transform.SetParent(transform);

			// Reorganize to take care of movement
			ReorganizeCards();
        }
        else if (CombatManager.CurrentState == CombatState.Player_DEF)
        {
			// If already card in player reveal, replace it
            if (PlayerReveal.childCount > 0)
            {
				UnplayCard(PlayerReveal.GetChild(0).GetComponent<Card>());
            }
            
			// Set Parent
			if (card.isPlayer == true)
				card.transform.SetParent(PlayerReveal);
			else
				card.transform.SetParent(EnemyReveal);
            
			// Go to local zero
			card.RegisterToMove(Vector3.zero);


        }
		else
		    Debug.LogError("Cannot get relevant transform in this state.");

    }

	public bool CanShuffle()
	{
		if (CurrentAP == MaxAP)
			return true;
		return false;
	}

    public override void PlayCard(Card card)
    {
        PlayerCM.UseAP(card.AP);
        PlaceCard(card);
    }

    public override void UnplayCard(Card card)
    {
        PlayerHand.PlayCard(card);
        PlayerCM.GetBackAP(card.AP);
    }
}
