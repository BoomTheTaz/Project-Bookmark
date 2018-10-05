using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { Player_DEF, Player_ATK, AI_DEF, AI_ATK };
public enum EffectTypes { NONE, GainAP, TakeAP, DrawCard, DiscardCard };


public class CombatManager : MonoBehaviour {

	public GameObject CardPrefab;
	public PlayArea playArea;

	public Transform PlayerCardReveal;
	public Transform EnemyCardReveal;

	public CharacterData Enemy;
	public CharacterData Player;

	public static bool CanDrag;

	public static CombatState CurrentState { get; private set; }

	EnemyAI AI;

	public static CombatManager instance;

	bool isPlayerStarting;

	public CardManager PlayerCM;
	public CardManager EnemyCM;


    private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

	// Use this for initialization
	void Start () {

        // ========== PROBABLY TEMPORARY, CANT THINK AT MOMENT =============
        
        Enemy = GameManager.instance.enemyData;
        Player = GameManager.instance.playerData;

        // TEMP: create Decks for player and AI
        CreateDecks();

		// TEMP: Start with player attacking
        //      Compare Speeds in future to decide who initiates
		CurrentState = CombatState.Player_ATK;

        // Grab necessary Components
		AI = FindObjectOfType<EnemyAI>();
		

		SetupUI();

        PlayerCM.FillHand();
		EnemyCM.FillHand(); 

		if (isPlayerStarting == false)
		{
			CurrentState = CombatState.AI_ATK;
		}
		else
        {
			CurrentState = CombatState.Player_ATK;
        }
	}

    public void DoNextThing()
    {
        //Debug.Log("DoingNextThing: " + CurrentState.ToString());
        
        // Enemy played defensive card
        if (EnemyCardReveal.childCount > 0 && CurrentState == CombatState.Player_ATK)
            EvaluateCombat();

        // Player played defensive card
        else if (PlayerCardReveal.childCount > 0 && CurrentState == CombatState.AI_ATK)
            EvaluateCombat();

        // Player played offensive card
        else if (PlayerCardReveal.childCount > 0 && CurrentState == CombatState.Player_ATK)
            AI.Defend(PlayerCardReveal.GetChild(0).GetComponent<Card>().AP);

        // Waiting for AI attack, prompt ai for attack decision
        else if (CurrentState == CombatState.AI_ATK && EnemyCardReveal.childCount == 0)
            AI.Attack();

        // Things that just return
        // - Waiting for player attack
        // - Waiting for player defense
        else 
            return;
    }

    public void PassTurn()
    {
        if (CurrentState == CombatState.Player_ATK)
        {
            EnemyCM.NewTurn();

            CurrentState = CombatState.AI_ATK;
        }
        else if (CurrentState == CombatState.AI_ATK)
        {
            PlayerCM.NewTurn();

            CurrentState = CombatState.Player_ATK;
        }
        else
            Debug.LogError("HOW DID I GET HERE?");
    }

    public void ButtonPress()
    {
        if (CurrentState == CombatState.AI_ATK && EnemyCardReveal.childCount > 0)
            EvaluateCombat();
        else if (CurrentState == CombatState.Player_ATK && PlayerCardReveal.childCount == 0)
            PassTurn();
        else
            Debug.Log("I am refusing to do anything!");
    }

    public void NoDefense()
    {
        EvaluateCombat();
    }

    void SetupUI()
	{
		CombatUI.instance.ChangeAP(Player.MaxAP, true);
		CombatUI.instance.ChangeAP(Enemy.MaxAP, false);
		CombatUI.instance.ChangeHealth(Player.CurrentHealth, true);
		CombatUI.instance.ChangeHealth(Enemy.MaxHealth, false);
        
	}

	// TEMPORARY: Generate generic deck
	void CreateDecks()
	{
		PlayerCM.CreateDeck(CardPrefab);
		EnemyCM.CreateDeck(CardPrefab);
	}

    // Array to store relevant cards in combat
    Card AttackingCard;
    Card DefendingCard;
    CharacterData Defender;

    void EvaluateCombat()
	{
        // Compare cards in reveal areas and compute damage/other effects
        int DMG;

        if (CurrentState == CombatState.Player_ATK)
        {
            AttackingCard = PlayerCardReveal.GetChild(0).GetComponent<Card>();

            if (EnemyCardReveal.childCount > 0)
                DefendingCard = EnemyCardReveal.GetChild(0).GetComponent<Card>();

            Defender = Enemy;
        }
        else if (CurrentState == CombatState.AI_ATK)
        {
            AttackingCard = EnemyCardReveal.GetChild(0).GetComponent<Card>();

            if (PlayerCardReveal.childCount > 0)
                DefendingCard = PlayerCardReveal.GetChild(0).GetComponent<Card>();

            Defender = Player;
        }

        if (DefendingCard == null)
            DMG = AttackingCard.ATK;
        else
            DMG = AttackingCard.ATK - DefendingCard.DEF;

        if (DMG > 0)
        {
            Defender.TakeDamage(DMG);
            if (AttackingCard.hasAttackEffect == true)
                AttackingCard.AttackEffect();
        }
        else if (DMG <= 0 && DefendingCard.hasDefenseEffect == true)
            DefendingCard.DefenseEffect();
        
		DiscardCombatCards();


	}

    void DiscardCombatCards()
	{
		// Move cards to discard pile
        if (AttackingCard != null)
        {
            if (AttackingCard.isPlayer == true)
            {
                if (ShouldTrash(AttackingCard) == true)
                    PlayerCM.TrashCard(AttackingCard);
                else
                    PlayerCM.DiscardCard(AttackingCard);
            }
            else
            {
                if (ShouldTrash(AttackingCard) == true)
                    EnemyCM.TrashCard(AttackingCard);
                else
                    EnemyCM.DiscardCard(AttackingCard);
            }
            
        }
        if (DefendingCard != null)
        {
            if (DefendingCard.isPlayer == true)
            {
                if (ShouldTrash(DefendingCard) == true)
                    PlayerCM.TrashCard(DefendingCard);
                else
                    PlayerCM.DiscardCard(DefendingCard);
            }
            else
            {
                if (ShouldTrash(DefendingCard) == true)
                    EnemyCM.TrashCard(DefendingCard);
                else
                    EnemyCM.DiscardCard(DefendingCard);
            }

        }

        AttackingCard = null;
        DefendingCard = null;

        CombatUI.instance.CheckDoneMoving();
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
				if (CurrentState == CombatState.Player_ATK)
					toReturn =  true;
				else if (CurrentState == CombatState.AI_ATK)
				    toReturn = false;
				break;

			// Case: magic defense
			case CardType.DEF_Mag:
				if (CurrentState == CombatState.AI_ATK)
					toReturn = true;
				else if (CurrentState == CombatState.Player_ATK)
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
        //Debug.Log("Gaining AP");
        if (player == true)
        {
            PlayerCM.GetBackAP(i);
        }
        else
        {
            EnemyCM.GetBackAP(i);
        }
    }

    public void TakeAP(int i, bool player)
    {
        //Debug.Log("Taking AP");
        if (player == true)
        {
            EnemyCM.UseAP(i);
        }
        else
        {
            PlayerCM.UseAP(i);
        }
    }

    public void GainCard(int i, bool player)
    {
        //Debug.Log("Gaining Card");
        if (player == true)
        {
            StartCoroutine(PlayerCM.DrawCards(i));
        }
        else
        {
            StartCoroutine(EnemyCM.DrawCards(i));
        }
    }

    public void DiscardCard(int i, bool player)
    {
        //Debug.Log("Discarding Card");
        //Debug.Log(player);
        if (player == true)
        {
            StartCoroutine(EnemyCM.ForceDiscard(i));
        }
        else
        {
            StartCoroutine(PlayerCM.ForceDiscard(i));
        }
    }
}
