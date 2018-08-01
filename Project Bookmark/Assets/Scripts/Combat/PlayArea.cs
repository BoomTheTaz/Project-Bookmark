using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : Dropzone {

	public int CurrentAP { get; protected set; }
	public int MaxAP;
	public Transform PlayerReveal;
    
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

	public override Transform GetRelevantTransform()
	{

		if (CombatManager.currentState == CombatState.Player_ATK)
		{
			return transform;
		}
		if (CombatManager.currentState == CombatState.Player_DEF)
		{
			return PlayerReveal;
		}
		Debug.LogError("Cannot get relevant transform in this state.");
		return transform;
			

	}

    public void ResetAP()
	{
		CurrentAP = 0;
	}
}
