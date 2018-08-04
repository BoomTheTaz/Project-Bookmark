using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : Dropzone {

	public int CurrentAP { get; protected set; }
	public int MaxAP;
	public Transform PlayerReveal;
	public Transform EnemyReveal;
	public Dropzone PlayerHand;
    
	private new void Start()
	{
		base.Start();

		MaxAP = FindObjectOfType<Player>().AP;

	}

    
	protected override bool CanAddCard(int ap)
	{
		if (MaxAP - CurrentAP >= ap)
		{
			return true;
		}
		return false;
	}
   
	public override void RemoveCardAP(int ap)
	{
		CurrentAP -= ap;
		Debug.Log("REMOVING AP. Current AP: " + CurrentAP.ToString());
	}
    
	public override void AddCardAP(int ap)
    {
        CurrentAP += ap;
        Debug.Log("ADDING AP. Current AP: " + CurrentAP.ToString());
    }

	//public override Transform GetRelevantTransform()
	//{

	//	if (CombatManager.CurrentState == CombatState.Player_ATK)
	//	{
	//		return transform;
	//	}
	//	if (CombatManager.CurrentState == CombatState.Player_DEF)
	//	{
	//		return PlayerReveal;
	//	}
	//	Debug.LogError("Cannot get relevant transform in this state.");
	//	return transform;
			

	//}

    public void ResetAP()
	{
		CurrentAP = 0;
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
				RemoveCardAP(PlayerReveal.GetChild(0).GetComponent<Card>().AP);
				PlayerHand.PlaceCard(PlayerReveal.GetChild(0).GetComponent<Card>());
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
}
