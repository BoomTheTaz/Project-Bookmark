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
        
        // Check if have enough AP to play card
		if (PlayerCM.CurrentAP() >= ap /*|| CombatManager.CurrentState == CombatState.Player_DEF*/)
        {
            
            // Above check should suffice on offense
            if (CombatManager.CurrentState == CombatState.Player_ATK)
                return true;

            // Check if player added viable card
            else if (CombatManager.CurrentState == CombatState.AI_ATK)
            {
                Debug.Log(ap);
                // Must have equal or lower AP value to enemy card
                if (EnemyReveal.GetChild(0).GetComponent<Card>().AP >= ap)
                    return true;
                return false;
            }
            
            else
                Debug.Log("Not a fast enough card");

		}
		return false;
	}

  	public void AddCardToEnemyReveal(Card c)
	{
		c.transform.SetParent(EnemyReveal);
		c.RegisterToMove(Vector3.zero);
		c.RegisterToFlip();

	}

    public void AddCardToPlayerReveal(Card c)
    {
        c.transform.SetParent(PlayerReveal);
        c.RegisterToMove(Vector3.zero);
    }

    public override void PlaceCard(Card card)
    {

        // Player playing offensive card
        if (card.isPlayer == true)
        {
            AddCardToPlayerReveal(card);
        }

        // AI playing defensive card
        if (card.isPlayer == false)
        {
            AddCardToEnemyReveal(card);
        }

       

        /*
         * 
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

        */
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
