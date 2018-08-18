using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Player_DEF, Player_ATK, AI_DEF, AI_ATK };

public class CombatManager : MonoBehaviour {

	public GameObject CardPrefab;
	public PlayArea playArea;

	public Deck PlayerDeck;
	public Deck EnemyDeck;

	Transform AttackDeck;
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

		SetupUI();

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

    void SetupUI()
	{
		CombatUI.instance.ChangeAP(player.MaxAP, true);
		CombatUI.instance.ChangeAP(Enemy.MaxAP, false);
		CombatUI.instance.ChangeHealth(player.CurrentHealth, true);
		CombatUI.instance.ChangeHealth(Enemy.MaxHealth, false);
        
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

	bool AttackPending = false;
    // Figure out where to go next
    public void Evaluate()
	{
		// CASE: Attack has been pending, defense is finalized, evaluate combat
		if (AttackPending == true)
		{
			AttackPending = false;
			EvaluateCombat();
		}
		// CASE: cards are in the player attack deck, flip top one
		else if (PlayerCardReveal.childCount > 0)
		{
			DrawFromAttackDeck();
			AttackPending = true;
		}
		// CASE: cards are in the enemy attack deck, flip top one
        else if (EnemyCardReveal.childCount > 0)
		{
            DrawFromAttackDeck();
            AttackPending = true;
        }

        // ===== BASE STATE SWITCHES, OUT OF COMBAT =====
		else if (CurrentState == CombatState.Player_ATK)
            FinalizePlayerAttack();
		else if (CurrentState == CombatState.AI_DEF)
            FinalizeAIDefense();
		else if (CurrentState == CombatState.AI_ATK)
            FinalizeAIAttack();
		else if (CurrentState == CombatState.Player_DEF)
            FinalizePlayerDefense();


		//if (CurrentState == CombatState.Player_ATK)
		//	FinalizePlayerAttack();
		//else if (PlayerCardReveal.childCount > 0 || EnemyCardReveal.childCount > 0)
		//	EvaluateCombat();
		//else if (CurrentState == CombatState.AI_DEF && PlayerCardReveal.childCount > 0)
		//	DrawFromAttackDeck();
		
		//else if (CurrentState == CombatState.AI_ATK && playArea.transform.childCount == 0)
		//	AI.Attack();
		
		//else if (CurrentState == CombatState.Player_DEF && EnemyCardReveal.childCount > 0)
			//DrawFromAttackDeck();
		

		Debug.Log(CurrentState);
	}


    // Lock in atk power/dmg against AI
    // Take all cards in play area and put in attack deck
    void FinalizePlayerAttack()
	{
		StartCoroutine("FillHand");
        
        // If player is attacking, create the attack deck
		if (playArea.transform.childCount > 0)
        {
			CurrentState = CombatState.AI_DEF;
            CreateAttackDeck();
        }

		// If not, go to AI Attack
		else
		{
			FinalizeAIDefense();
		}



	}

    // Resolve player attacks against AI defense
    // Draw from attack deck one by one and prompt AI for defense card
	void FinalizeAIDefense()
    {
        CurrentState = CombatState.AI_ATK;
		OnMoveEnd = AI.Attack;
    }

    // Lock in atk power/dmg against player
    // Have AI choose attack cards, then move all in play area to attack deck
	public void FinalizeAIAttack()
    {
		CurrentState = CombatState.Player_DEF;
		StartCoroutine("FillHand");
        playArea.ResetAP();

		if (playArea.transform.childCount > 0)
        {
            CreateAttackDeck();
			OnMoveEnd = DrawFromAttackDeck;
        }
		else
		{
			FinalizePlayerDefense();
		}

    }

    // Resolve AI attacks against player
    // Draw from attack deck one by one, prompting player for defense card
	void FinalizePlayerDefense()
    {
		CurrentState = CombatState.Player_ATK;
    }
   
    void CreateAttackDeck()
	{
		if (CurrentState == CombatState.AI_DEF)
		{
			AttackDeck = PlayerCardReveal;
		}
		else if (CurrentState == CombatState.Player_DEF)
		{
			AttackDeck = EnemyCardReveal;
		}
		else
			Debug.LogError("Cannot create attack deck from this state: " + CurrentState);

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

		EvaluateOnEndMove();
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
        // AI is defending against player attack
		if (CurrentState == CombatState.AI_DEF)
		{
            // Check if AI is defending, take damage accordingly
			if (EnemyCardReveal.childCount > 0)
            {
                CardsInCombat[1] = EnemyCardReveal.GetChild(0).GetComponent<Card>();
				Enemy.TakeDamage(CardsInCombat[0].ATK - CardsInCombat[1].DEF, false);
            }
            else
            {
                CardsInCombat[1] = null;
				Enemy.TakeDamage(CardsInCombat[0].ATK, false);
            }

			// TODO: Figure out damage equation


			if (AttackDeck.childCount == 0)
				OnMoveEnd = AI.Attack;

			//if (CardsInCombat[0] != null)
			//    Debug.Log("Player AttacK: " + CardsInCombat[0].ATK.ToString());
			//if (CardsInCombat[1] != null)
    			//Debug.Log("Enemy Defense: " + CardsInCombat[1].DEF.ToString());
		}
        // Player is defending against AI attack
		else if (CurrentState == CombatState.Player_DEF)
		{
			// Check if player is defending, take damage accordingly
			if (PlayerCardReveal.childCount > 0)
            {
                CardsInCombat[0] = PlayerCardReveal.GetChild(0).GetComponent<Card>();
				Player.TakeDamage(CardsInCombat[1].ATK - CardsInCombat[0].DEF, true);
            }
            else
            {
                CardsInCombat[0] = null;
				Player.TakeDamage(CardsInCombat[1].ATK, true);
            }



			// TODO: Figure out damage equation
			if (CardsInCombat[0] != null)
                Debug.Log("Player Defense: " + CardsInCombat[0].DEF.ToString());
            if (CardsInCombat[1] != null)
				Debug.Log("Enemy Attack: " + CardsInCombat[1].ATK.ToString());
		}
		else
			Debug.LogError("Invalid State to evaluate combat.");


		DiscardCombatCards();


		Evaluate();
	}

    void DiscardCombatCards()
	{
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

	public void EvaluateOnEndMove()
	{
		StartCoroutine("DelayedEvaluate");
	}

	IEnumerator DelayedEvaluate()
	{
		yield return new WaitForEndOfFrame();
		OnMoveEnd = Evaluate;

	}

	void Here()
	{
		Debug.Log("Here");
	}
}
