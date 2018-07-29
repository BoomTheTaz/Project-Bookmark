using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : Dropzone {

	public int CurrentAP { get; protected set; }
	public int MaxAP;

    
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

	public void Reset()
	{
		CurrentAP = 0;
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
}
