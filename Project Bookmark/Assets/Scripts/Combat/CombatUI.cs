﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour {

	delegate void MoveCards();
	MoveCards CardsToMove;
	MoveCards CardsToFlip;
	MoveCards CardsToScale;

	public Text PlayerAP;
	public Text EnemyAP;
	public Text PlayerHealth;
	public Text EnemyHealth;

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

        CheckDoneMoving();

    }

	public void RegisterCardToFlip(Card c)
    {
		CardsToFlip += c.Flip;
    }

    public void UnregisterCardToFlip(Card c)
    {
		CardsToFlip -= c.Flip;
        CheckDoneMoving();
    }

	public void RegisterCardToScale(Card c)
    {
		CardsToScale += c.Scale;
    }

    public void UnregisterCardToScale(Card c)
    {

		CardsToScale -= c.Scale;
        CheckDoneMoving();
    }

    public void CheckDoneMoving()
    {
        if (CardsToFlip == null && CardsToMove == null && CardsToScale == null)
            CombatManager.instance.DoNextThing();
    }

    public void ChangeHealth(int h, bool isPlayer)
	{
		if (isPlayer == true)
			PlayerHealth.text = h.ToString();
		else
			EnemyHealth.text = h.ToString();
	}

	public void ChangeAP(int ap, bool isPlayer)
    {
        if (isPlayer == true)
			PlayerAP.text = ap.ToString();
        else
			EnemyAP.text = ap.ToString();
    }
}
