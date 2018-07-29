using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public Deck deck;
	public Hand hand;
	public PlayArea playArea;

	Dropzone handDZ;

	// Use this for initialization
	void Start () {
		handDZ = hand.GetComponent<Dropzone>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Defend()
	{
		Debug.Log("Time to defend");

		while(hand.transform.childCount < 5)
		{
			handDZ.AddCard(deck.DrawCard());
		}
	}

	public void Attack()
    {
        Debug.Log("Time to attack");
    }
}
