using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Player_DEF, Player_ATK, AI_DEF, AI_ATK };

public class CombatManager : MonoBehaviour {

	public GameObject CardPrefab;
	public PlayArea playArea;

	public Deck PlayerDeck;
	public Deck AIDeck;

	public Transform AttackDeck;
	public Transform PlayerCardReveal;
	public Transform EnemyCardReveal;
	public Transform PlayerDiscard;
	public Transform EnemyDiscard;

	public static bool CanDrag;

	public static CombatState CurrentState { get; private set; }

	int PlayerATK;
	int PlayerDEF;
	int AI_ATK;
	int AI_DEF;


	EnemyAI AI;

	// Use this for initialization
	void Start () {
        
		// TEMP: create Decks for player and AI
		CreateDeck();
		CreateAIDeck();

		// TEMP: Start with player attacking
        //      Compare Speeds in future to decide who initiates
		CurrentState = CombatState.Player_ATK;

        // Grab necessary Components
		AI = FindObjectOfType<EnemyAI>();
	}

	// TEMPORARY: Generate generic deck
    void CreateDeck()
	{
		int numCards = 10;

		GameObject[] temp = new GameObject[numCards];

		for (int i = 0; i < numCards; i++)
		{
			temp[i] = Instantiate(CardPrefab, PlayerDeck.transform.position, Quaternion.identity,PlayerDeck.transform);

			temp[i].GetComponent<Card>().FlipInstant();

			temp[i].name = "Card " + i.ToString();

			temp[i].GetComponent<Card>().SetupCard(CardTemplate.GetTemplate(i % 6));
		}

		PlayerDeck.Shuffle();
	}

	void CreateAIDeck()
	{
		int numCards = 10;

        GameObject[] temp = new GameObject[numCards];
        
        for (int i = 0; i < numCards; i++)
        {
			temp[i] = Instantiate(CardPrefab, AIDeck.transform.position, Quaternion.identity, AIDeck.transform);
            temp[i].transform.Rotate(new Vector3(0, 180, 0));

			temp[i].GetComponent<CanvasGroup>().blocksRaycasts = false;

			temp[i].GetComponent<Card>().FlipInstant();

            temp[i].name = "Enemy Card " + i.ToString();

            temp[i].GetComponent<Card>().SetupCard(CardTemplate.GetTemplate(i % 6));
			temp[i].GetComponent<Card>().SetEnemyBack();
        }

		AIDeck.Shuffle();
	}

    // Figure out where to go next
    public void Evaluate()
	{
		if (CurrentState == CombatState.Player_ATK)
			FinalizePlayerAttack();
		else if (PlayerCardReveal.childCount > 0 || EnemyCardReveal.childCount > 0)
			EvaluateCombat();
		else if (CurrentState == CombatState.AI_DEF && AttackDeck.transform.childCount > 0)
			DrawFromAttackDeck(true);
		else if (CurrentState == CombatState.AI_DEF)
			FinalizeAIDefense();
		else if (CurrentState == CombatState.AI_ATK && playArea.transform.childCount == 0)
			AI.Attack();
		else if (CurrentState == CombatState.AI_ATK)
			FinalizeAIAttack();
		else if (CurrentState == CombatState.Player_DEF && AttackDeck.transform.childCount > 0)
			DrawFromAttackDeck(false);
		else if (CurrentState == CombatState.Player_DEF)
			FinalizePlayerDefense();

		Debug.Log(CurrentState);
	}


    // Lock in atk power/dmg against AI
    // Take all cards in play area and put in attack deck
    void FinalizePlayerAttack()
	{
		if(playArea.transform.childCount > 0)
		{
			CreateAttackDeck();
		}

		CurrentState = CombatState.AI_DEF;
	}

    // Resolve player attacks against AI defense
    // Draw from attack deck one by one and prompt AI for defense card
	void FinalizeAIDefense()
    {
        CurrentState = CombatState.AI_ATK;
    }

    // Lock in atk power/dmg against player
    // Have AI choose attack cards, then move all in play area to attack deck
	void FinalizeAIAttack()
    {

		if (playArea.transform.childCount > 0)
        {
            CreateAttackDeck();
        }

		CurrentState = CombatState.Player_DEF;
		playArea.ResetAP();
    }

    // Resolve AI attacks against player
    // Draw from attack deck one by one, prompting player for defense card
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

		CurrentState = CombatState.Player_ATK;
    }
   
    void CreateAttackDeck()
	{
		// Get all cards in play area
        // Parent and relocate cads to attack deck
		int childCount = playArea.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            // Take top card
            Card temp = playArea.transform.GetChild(0).GetComponent<Card>();

			temp.transform.SetParent(AttackDeck);
			temp.transform.position = AttackDeck.position;

        }
	}

    void DrawFromAttackDeck(bool isPlayerAttack)
	{
		Transform child = AttackDeck.GetChild(0);

		// Take top card off attack deck and move it to appropriate reveal area
		if (isPlayerAttack == true)
		{
			
			child.position = PlayerCardReveal.position;
			child.SetParent(PlayerCardReveal);

			CardsInCombat[0] = child.GetComponent<Card>();

			AI.Defend();

		}

		else 
		{

			child.position = EnemyCardReveal.position;
			child.SetParent(EnemyCardReveal);

            CardsInCombat[1] = child.GetComponent<Card>();

		}
	}

    // Array to store relevant cards in combat
	Card[] CardsInCombat = new Card[2];

    void EvaluateCombat()
	{
		// Compare cards in reveal areas and compute damage/other effects
      
        // Calculate damage based on current state
		if (CurrentState == CombatState.AI_DEF)
		{
			if (EnemyCardReveal.childCount > 0)
            {
                CardsInCombat[1] = EnemyCardReveal.GetChild(0).GetComponent<Card>();
            }
            else
            {
                CardsInCombat[1] = null;
            }

			// TODO: Figure out damage equation

			if (CardsInCombat[0] != null)
			    Debug.Log("Player AttacK: " + CardsInCombat[0].ATK.ToString());
			if (CardsInCombat[1] != null)
    			Debug.Log("Enemy Defense: " + CardsInCombat[1].DEF.ToString());
		}

		else if (CurrentState == CombatState.Player_DEF)
		{
			if (PlayerCardReveal.childCount > 0)
            {
                CardsInCombat[0] = PlayerCardReveal.GetChild(0).GetComponent<Card>();
            }
            else
            {
                CardsInCombat[0] = null;
            }

			// TODO: Figure out damage equation
			if (CardsInCombat[0] != null)
                Debug.Log("Player Defense: " + CardsInCombat[0].DEF.ToString());
            if (CardsInCombat[1] != null)
				Debug.Log("Enemy Attack: " + CardsInCombat[1].ATK.ToString());
		}
		else
			Debug.LogError("Invalid State to evaluate combat.");

        // Move cards to discard pile
		if (CardsInCombat[0] != null)
		{
			CardsInCombat[0].transform.SetParent(PlayerDiscard);
			CardsInCombat[0].transform.position = PlayerDiscard.position;
			CardsInCombat[0].transform.localScale = Vector3.one;
			CardsInCombat[0].GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
		if (CardsInCombat[1] != null)
        {
			CardsInCombat[1].transform.SetParent(EnemyDiscard);
			CardsInCombat[1].transform.position = EnemyDiscard.position;
			CardsInCombat[1].transform.localScale = Vector3.one;

        }
		Evaluate();
	}

}
