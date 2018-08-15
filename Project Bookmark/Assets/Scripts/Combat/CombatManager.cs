using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Player_DEF, Player_ATK, AI_DEF, AI_ATK };

public class CombatManager : MonoBehaviour {

	public GameObject CardPrefab;
	public PlayArea playArea;

	public Deck PlayerDeck;
	public Deck EnemyDeck;

	public Transform AttackDeck;
	public Transform PlayerCardReveal;
	public Transform EnemyCardReveal;
	public Transform PlayerDiscard;
	public Transform EnemyDiscard;

	public CharacterData Enemy;
	public PlayerData Player;
	public Hand PlayerHand;
	public Hand EnemyHand;

	public static bool CanDrag;

	public static CombatState CurrentState { get; private set; }

	int PlayerATK;
	int PlayerDEF;
	int AI_ATK;
	int AI_DEF;

	PlayerData player;
	EnemyAI AI;

	public static CombatManager instance;
	delegate void NextStep();
	NextStep OnMoveEnd;

	bool isPlayerStarting;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

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
		player = FindObjectOfType<PlayerData>();

		StartCoroutine("SetupBoard");

		if (isPlayerStarting == false)
		{
			OnMoveEnd = AI.Attack;
			CurrentState = CombatState.AI_ATK;
		}
		else
        {
            OnMoveEnd = null;
			CurrentState = CombatState.Player_ATK;
        }
	}

	IEnumerator SetupBoard()
	{
		int playerCard = player.CardsInHand;
		int enemyCard = Enemy.CardsInHand;

		for (int i = 0; i < Mathf.Max(playerCard, enemyCard); i++)
		{
			yield return new WaitForSeconds(0.5f);

			if (i < playerCard)
			{
				PlayerHand.DrawCard();
			}
			if (i < enemyCard)
			{
				EnemyHand.DrawCard();
			}


		}

	}

	// TEMPORARY: Generate generic deck
    void CreateDeck()
	{      
		PlayerDeck.CreateDeck(new int[] { 1, 2, 3, 4, 5, 0, 0, 1, 2, 3, 4, 5 });
	}

	void CreateAIDeck()
	{
		EnemyDeck.CreateDeck(new int[] { 1, 2, 3, 4, 5, 0, 0, 1, 2, 3, 4, 5 });
	}

    // Figure out where to go next
    public void Evaluate()
	{
		if (CurrentState == CombatState.Player_ATK)
			FinalizePlayerAttack();
		else if (PlayerCardReveal.childCount > 0 || EnemyCardReveal.childCount > 0)
			EvaluateCombat();
		else if (CurrentState == CombatState.AI_DEF && AttackDeck.transform.childCount > 0)
			DrawFromAttackDeck();
		else if (CurrentState == CombatState.AI_DEF)
			FinalizeAIDefense();
		else if (CurrentState == CombatState.AI_ATK && playArea.transform.childCount == 0)
			AI.Attack();
		else if (CurrentState == CombatState.AI_ATK)
			FinalizeAIAttack();
		else if (CurrentState == CombatState.Player_DEF && AttackDeck.transform.childCount > 0)
			DrawFromAttackDeck();
		else if (CurrentState == CombatState.Player_DEF)
			FinalizePlayerDefense();

		Debug.Log(CurrentState);
	}


    // Lock in atk power/dmg against AI
    // Take all cards in play area and put in attack deck
    void FinalizePlayerAttack()
	{
		CurrentState = CombatState.AI_DEF;

		if (playArea.transform.childCount > 0)
        {
            CreateAttackDeck();
			OnMoveEnd = DrawFromAttackDeck;
        }

		StartCoroutine("FillHand");

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
		CurrentState = CombatState.Player_DEF;

		if (playArea.transform.childCount > 0)
        {
            CreateAttackDeck();
			OnMoveEnd = DrawFromAttackDeck;
        }

		StartCoroutine("FillHand");
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

		for (int i = childCount - 1; i >= 0; i--)
        {
            // Take top card
            Card temp = playArea.transform.GetChild(i).GetComponent<Card>();

			temp.transform.SetParent(AttackDeck);
			temp.RegisterToMove(Vector3.zero);

			if (temp.isPlayer == true)
			    temp.RegisterToFlip();

        }
	}

    void DrawFromAttackDeck()
	{
		Card child = AttackDeck.GetChild(AttackDeck.childCount-1).GetComponent<Card>();
		OnMoveEnd = null;
		// Take top card off attack deck and move it to appropriate reveal area
		if (CurrentState == CombatState.AI_DEF)
		{
			
			child.transform.SetParent(PlayerCardReveal);


			CardsInCombat[0] = child;

			AI.Defend(child.AP);

		}

		else 
		{

			child.transform.SetParent(EnemyCardReveal);

            CardsInCombat[1] = child;

		}
		child.RegisterToMove(Vector3.zero);
		child.RegisterToFlip();
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
				Enemy.TakeDamage(CardsInCombat[0].ATK - CardsInCombat[1].DEF);
            }
            else
            {
                CardsInCombat[1] = null;
				Enemy.TakeDamage(CardsInCombat[0].ATK);
            }

			// TODO: Figure out damage equation


			//if (CardsInCombat[0] != null)
			//    Debug.Log("Player AttacK: " + CardsInCombat[0].ATK.ToString());
			//if (CardsInCombat[1] != null)
    			//Debug.Log("Enemy Defense: " + CardsInCombat[1].DEF.ToString());
		}

		else if (CurrentState == CombatState.Player_DEF)
		{
			if (PlayerCardReveal.childCount > 0)
            {
                CardsInCombat[0] = PlayerCardReveal.GetChild(0).GetComponent<Card>();
				Player.TakeDamage(CardsInCombat[1].ATK - CardsInCombat[0].DEF);
            }
            else
            {
                CardsInCombat[0] = null;
				Player.TakeDamage(CardsInCombat[1].ATK);
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
			CardsInCombat[0].RegisterToMove(Vector3.zero);
			CardsInCombat[0].RegisterToScale();
			CardsInCombat[0].GetComponent<CanvasGroup>().blocksRaycasts = false;
			PlayerDeck.DiscardCard(CardsInCombat[0]);
		}
		if (CardsInCombat[1] != null)
        {
			CardsInCombat[1].transform.SetParent(EnemyDiscard);
			CardsInCombat[1].RegisterToMove(Vector3.zero);
            CardsInCombat[1].RegisterToScale();
			EnemyDeck.DiscardCard(CardsInCombat[0]);
        }
		Evaluate();
	}

    public void DoneMoving()
	{
		if (OnMoveEnd != null)
		{
			OnMoveEnd();
			OnMoveEnd = null;
		}
        
	}

	IEnumerator FillHand()
	{
		if (CurrentState == CombatState.Player_DEF)
		{
			while (PlayerHand.transform.childCount < player.CardsInHand)
			{
				PlayerHand.DrawCard();

				yield return new WaitForSeconds(0.5f);

				if (PlayerDeck.transform.childCount == 0)
					break;
			}
		}

		else if (CurrentState == CombatState.AI_DEF)
		{
			while (EnemyHand.transform.childCount < Enemy.CardsInHand)
            {
				EnemyHand.DrawCard();

                yield return new WaitForSeconds(0.5f);

				if (EnemyDeck.transform.childCount == 0)
                    break;
            }
		}
		else
			Debug.LogError("Should not be filling hand in state " + CurrentState);
	}

	void Here()
	{
		Debug.Log("Here");
	}
}
