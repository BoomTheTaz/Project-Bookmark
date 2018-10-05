using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData {

	// ===== STATS =====
	protected int[] CharacterStats = new int[(int)Stats.NUM_ELEMENTS];


    public int CurrentHealth { get; set; }
	public int MaxHealth { get; protected set; }
	public int MaxAP { get; protected set; }
	public int MaxCards { get; protected set; }
	public int CardsInHand { get; protected set; }
	public int WeaponPhys { get; protected set; }
	public int WeaponMag { get; protected set; }
	public int ArmorPhys { get; protected set; }
	public int ArmorMag { get; protected set; }
    public int TurnAP { get; protected set; }

    public bool isPlayer = false;
    
    public CardStats[] Cards { get; protected set; }

    // Constants
    const int BaseMaxAP = 6;
    const int TechPerAPLevel = 3;

    // Default Constructor should run DefaulSetup
    public CharacterData()
    {
        DefaultSetup();
    }

    // Default setup, used in child classes to set default enemy types
    protected virtual void DefaultSetup()
    {
        return;
    }

    public CharacterData(int power, int tech, int magic, int health, CardStats[] cards)
    {
        StoreData(power, tech, magic, health, cards);
    }

    protected void StoreData(int power, int tech, int magic, int health, CardStats[] cards)
    {
        // ===== Store inputted values =====
        CharacterStats[(int)Stats.Power] = power;
        CharacterStats[(int)Stats.Technique] = tech;
        CharacterStats[(int)Stats.Magic] = magic;
        MaxHealth = health;
        CurrentHealth = health;
        Cards = cards;

        // ===== Calculate other values based on inputs =====

        // Gain 2 AP per every "TechPerAPLevel" tech levels
        MaxAP = BaseMaxAP + Mathf.FloorToInt(CharacterStats[(int)Stats.Technique] / TechPerAPLevel) * 2;

        // TurnAP is half of max
        TurnAP = MaxAP / 2;

        // Cards in hand will equal turnAP for now
        CardsInHand = TurnAP;
    }
    
    public int GetStat(Stats s)
    {
        return CharacterStats[(int)s];
    }

	public void TakeDamage(int d)
	{
		if (d > 0)
			CurrentHealth -= d;
        Debug.Log(isPlayer);
		CombatUI.instance.ChangeHealth(CurrentHealth, isPlayer);

		if (CurrentHealth <= 0)
			CombatManager.instance.EndGame(isPlayer);
	}

	public bool StatCheck(Stats s, int n)
	{
        if (CharacterStats[(int)s] >= n)
            return true;
        return false;
    }
    
}
