using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Player_DEF, Player_ATK, AI_DEF, AI_ATK };
public enum EffectTypes { NONE, GainAP, TakeAP, DrawCard, DiscardCard };


public class CombatManager : MonoBehaviour {

	public GameObject CardPrefab;
	public PlayArea playArea;

	Transform AttackDeck;
	public Transform PlayerCardReveal;
	public Transform EnemyCardReveal;

	public CharacterData Enemy;
	public PlayerData Player;

	public static bool CanDrag;

	public static CombatState CurrentState { get; private set; }

	PlayerData player;
	EnemyAI AI;

	public static CombatManager instance;

	delegate void NextStep();
	NextStep OnMoveEnd;

	bool isPlayerStarting;

	public CardManager PlayerCardMgr;
	public CardManager EnemyCardMgr;


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
		CreateDecks();

		// TEMP: Start with player attacking
        //      Compare Speeds in future to decide who initiates
		CurrentState = CombatState.Player_ATK;

        // Grab necessary Components
		AI = FindObjectOfType<EnemyAI>();
		player = FindObjectOfType<PlayerData>();

		SetupUI();

        PlayerCardMgr.FillHand();
		EnemyCardMgr.FillHand(); 

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

    void SetupUI()
	{
		CombatUI.instance.ChangeAP(player.MaxAP, true);
		CombatUI.instance.ChangeAP(Enemy.MaxAP, false);
		CombatUI.instance.ChangeHealth(player.CurrentHealth, true);
		CombatUI.instance.ChangeHealth(Enemy.MaxHealth, false);
        
	}

	// TEMPORARY: Generate generic deck
	void CreateDecks()
	{
		PlayerCardMgr.CreateDeck(new int[] { 1, 2, 3, 4, 5, 0, 0, 1, 2, 3, 4, 5 }, CardPrefab);
		EnemyCardMgr.CreateDeck(new int[] { 1, 2, 3, 4, 5, 0, 0, 1, 2, 3, 4, 5 }, CardPrefab);
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
		CurrentState = CombatState.AI_DEF;
		EnemyCardMgr.FillHand();
        
        // If player is attacking, create the attack deck
		if (playArea.transform.childCount > 0)
        {
			
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
		PlayerCardMgr.FillHand();
        PlayerCardMgr.ResetAP();

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
        int DMG;
      
        // Calculate damage based on current state
        // AI is defending against player attack
		if (CurrentState == CombatState.AI_DEF)
		{
            // Check if AI is defending, take damage accordingly
			if (EnemyCardReveal.childCount > 0)
            {
                CardsInCombat[1] = EnemyCardReveal.GetChild(0).GetComponent<Card>();
                DMG = CardsInCombat[0].ATK - CardsInCombat[1].DEF;

            }
            else
            {
                CardsInCombat[1] = null;
                DMG = CardsInCombat[0].ATK;
            }

            // Take damage calculated above
            Enemy.TakeDamage(DMG, false);

            // Evaluate any card effects
            if (DMG > 0 && CardsInCombat[0].hasAttackEffect == true)
                CardsInCombat[0].AttackEffect();
            else if (DMG <= 0 && CardsInCombat[1].hasDefenseEffect == true)
                CardsInCombat[1].DefenseEffect();

            // TODO: Figure out damage equation


            if (AttackDeck.childCount == 0)
				OnMoveEnd = AI.Attack;

			
		}
        // Player is defending against AI attack
		else if (CurrentState == CombatState.Player_DEF)
		{
			// Check if player is defending, take damage accordingly
			if (PlayerCardReveal.childCount > 0)
            {
                CardsInCombat[0] = PlayerCardReveal.GetChild(0).GetComponent<Card>();
                DMG = CardsInCombat[1].ATK - CardsInCombat[0].DEF;
            }
            else
            {
                CardsInCombat[0] = null;
                DMG = CardsInCombat[1].ATK;
            }

            Player.TakeDamage(DMG, true);

            if (DMG > 0 && CardsInCombat[1].hasAttackEffect == true)
                CardsInCombat[1].AttackEffect();
            else if (DMG <= 0 && CardsInCombat[0].hasDefenseEffect == true)
                CardsInCombat[0].DefenseEffect();
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
			if (ShouldTrash(CardsInCombat[0]) == true)
				PlayerCardMgr.TrashCard(CardsInCombat[0]);
			else
				PlayerCardMgr.DiscardCard(CardsInCombat[0]);
            
            
        }
        if (CardsInCombat[1] != null)
        {
			if (ShouldTrash(CardsInCombat[1]) == true)
				EnemyCardMgr.TrashCard(CardsInCombat[1]);
            else
				EnemyCardMgr.DiscardCard(CardsInCombat[1]);
   
        }
	}

    bool ShouldTrash(Card c)
	{
		// Boolean  to return
		bool toReturn = false;

        // Only care if certain card types, i.e. magic
		// NOTE: Combat should only be evaluated on Defensive States
		switch (c.Type)
		{
			// Case: magic attack
			case CardType.ATK_Mag:
				if (CurrentState == CombatState.AI_DEF)
					toReturn =  true;
				else if (CurrentState == CombatState.Player_DEF)
				    toReturn = false;
				break;

			// Case: magic defense
			case CardType.DEF_Mag:
				if (CurrentState == CombatState.Player_DEF)
					toReturn = true;
				else if (CurrentState == CombatState.AI_DEF)
					toReturn = false;
				break;

			default:
				
				return false;
		}

        // Previous switch based on player results, flip for Enemy results
		if (c.isPlayer == false)
			toReturn = !toReturn;

		return toReturn;
	}
   
	public void DoneMoving()
	{
		if (OnMoveEnd != null)
		{
			OnMoveEnd();
			OnMoveEnd = null;
		}
        
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


    public void EndGame(bool isPlayer)
	{
		// Player lost
		if (isPlayer == true)
		{
			Debug.Log("Player has died.");
			GameManager.instance.CombatDefeat();
		}

        // Player won
		else
		{
			Debug.Log("Player has won!!!");
			GameManager.instance.CombatVictory();
		}
	}

	void Here()
	{
		Debug.Log("Here");
	}

    // ===== FOR CARD EFFECTS =====

    public void GainAP(int i, bool player)
    {
        Debug.Log("Gaining AP");
        if (player == true)
        {
            PlayerCardMgr.GetBackAP(i);
        }
        else
        {
            EnemyCardMgr.GetBackAP(i);
        }
    }

    public void TakeAP(int i, bool player)
    {
        Debug.Log("Taking AP");
        if (player == true)
        {
            EnemyCardMgr.UseAP(i);
        }
        else
        {
            PlayerCardMgr.UseAP(i);
        }
    }

    public void GainCard(int i, bool player)
    {
        Debug.Log("Gaining Card");
        if (player == true)
        {
            StartCoroutine(PlayerCardMgr.DrawCards(i));
        }
        else
        {
            StartCoroutine(EnemyCardMgr.DrawCards(i));
        }
    }

    public void DiscardCard(int i, bool player)
    {
        Debug.Log("Discarding Card");
        if (player == true)
        {
            StartCoroutine(EnemyCardMgr.ForceDiscard(i));
        }
        else
        {
            StartCoroutine(PlayerCardMgr.ForceDiscard(i));
        }
    }
}
