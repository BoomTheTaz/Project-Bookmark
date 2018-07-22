using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {

	public GameObject CardPrefab;

	Deck playerDeck;

	// Use this for initialization
	void Start () {
		playerDeck = FindObjectOfType<Deck>();

		CreateDeck();
	}

	// TEMPORARY: Generate generic deck
    void CreateDeck()
	{
		int numCards = 10;

		GameObject[] temp = new GameObject[numCards];

		for (int i = 0; i < numCards; i++)
		{
			temp[i] = Instantiate(CardPrefab, playerDeck.transform.position, Quaternion.identity,playerDeck.transform);
			temp[i].transform.Rotate(new Vector3(0, 180, 0));

			temp[i].GetComponent<Card>().Flip();

			temp[i].name = "Card " + i.ToString();
		}

		playerDeck.Shuffle();
	}


}
