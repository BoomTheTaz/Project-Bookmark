using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Player_DEF, Player_ATK, AI_DEF, AI_ATK };

public class CombatManager : MonoBehaviour {

	public GameObject CardPrefab;
	public PlayArea playArea;

	public Deck playerDeck;
	public Deck AIDeck;

	CombatState currentState;

	int PlayerATK;
	int PlayerDEF;
	int AI_ATK;
	int AI_DEF;


	EnemyAI AI;

	// Use this for initialization
	void Start () {
        
		CreateDeck();
		CreateAIDeck();

		currentState = CombatState.Player_ATK;

		AI = FindObjectOfType<EnemyAI>();
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
			temp[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

			temp[i].GetComponent<Card>().Flip();

			temp[i].name = "Card " + i.ToString();

			temp[i].GetComponent<Card>().SetupCard(CardTemplate.GetTemplate(i % 6));
		}

		playerDeck.Shuffle();
	}

	void CreateAIDeck()
	{
		int numCards = 10;

        GameObject[] temp = new GameObject[numCards];

        for (int i = 0; i < numCards; i++)
        {
			temp[i] = Instantiate(CardPrefab, AIDeck.transform.position, Quaternion.identity, AIDeck.transform);
            temp[i].transform.Rotate(new Vector3(0, 180, 0));
            temp[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            temp[i].GetComponent<Card>().Flip();

            temp[i].name = "Card " + i.ToString();

            temp[i].GetComponent<Card>().SetupCard(CardTemplate.GetTemplate(i % 6));
        }

		AIDeck.Shuffle();
	}


    public void Evaluate()
	{
		if (currentState == CombatState.Player_ATK)
			FinalizePlayerAttack();
		else if (currentState == CombatState.AI_DEF)
			FinalizeAIDefense();
		else if (currentState == CombatState.AI_ATK)
			FinalizeAIAttack();
		else if (currentState == CombatState.Player_DEF)
			FinalizePlayerDefense();
	}


    // Lock in atk power/dmg against AI
    void FinalizePlayerAttack()
	{
		if(playArea.transform.childCount > 0)
		{
			PlayerATK = 0;
			for (int i = 0; i < playArea.transform.childCount; i++)
			{
				PlayerATK += playArea.transform.GetChild(i).GetComponent<Card>().ATK;
			}
		}

		Debug.Log("Locked in player attacks. ATK: " + PlayerATK.ToString()) ;
		currentState = CombatState.AI_DEF;
	}

    // Resolve player attacks against AI defense
	void FinalizeAIDefense()
    {
		AI.Defend();
        currentState = CombatState.AI_ATK;
    }

    // Lock in atk power/dmg against player
	void FinalizeAIAttack()
    {
		AI.Attack();
		currentState = CombatState.Player_DEF;
    }

    // Resolve AI attacks against player
	void FinalizePlayerDefense()
    {
		if (playArea.transform.childCount > 0)
        {
			PlayerDEF = 0;
			for (int i = 0; i < playArea.transform.childCount; i++)
            {
				PlayerDEF += playArea.transform.GetChild(i).GetComponent<Card>().DEF;
            }
        }

		Debug.Log("Locked in player defense. DEF: " + PlayerDEF.ToString());
		currentState = CombatState.Player_ATK;
    }

}
