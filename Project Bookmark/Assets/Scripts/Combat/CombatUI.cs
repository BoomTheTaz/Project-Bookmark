using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUI : MonoBehaviour {

	delegate void MoveCards();
	MoveCards CardsToMove;
	MoveCards CardsToFlip;
	MoveCards CardsToScale;


	public static CombatUI instance;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}
   
	// Update is called once per frame
	void Update () {
		
		if (CardsToMove != null)
			CardsToMove();


		if (CardsToFlip != null)
			CardsToFlip();
		
		if (CardsToScale != null)
			CardsToScale();
	}

    public void RegisterCardToMove(Card c)
	{
		CardsToMove += c.Move;
	}
    
	public void UnregisterCardToMove(Card c)
    {
        CardsToMove -= c.Move;

		if (CardsToFlip == null && CardsToMove == null && CardsToScale == null)         
            CombatManager.instance.DoneMoving();
        
    }

	public void RegisterCardToFlip(Card c)
    {
		CardsToFlip += c.Flip;
    }

    public void UnregisterCardToFlip(Card c)
    {
		CardsToFlip -= c.Flip;
		if (CardsToFlip == null && CardsToMove == null && CardsToScale == null)
            CombatManager.instance.DoneMoving();
    }

	public void RegisterCardToScale(Card c)
    {
		CardsToScale += c.Scale;
    }

    public void UnregisterCardToScale(Card c)
    {

		CardsToScale -= c.Scale;
		if (CardsToFlip == null && CardsToMove == null && CardsToScale == null)
            CombatManager.instance.DoneMoving();
    }
}
