using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {

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




	private void Awake()
	{
		CharacterStats[(int)Stats.Power] = Roll(1, 3);
		CharacterStats[(int)Stats.Technique] = Roll(1, 3);
        CharacterStats[(int)Stats.Constitution] = Roll(1, 3);
		CharacterStats[(int)Stats.Magic] = Roll(1, 3);
        CharacterStats[(int)Stats.Wisdom] = Roll(1, 3);
        CharacterStats[(int)Stats.Charisma] = Roll(1, 3);

        // ======= TEMP HARD CODE ========
        MaxAP = 3;
        MaxHealth = 100;
        CurrentHealth = 100;
        MaxCards = 10;
        CardsInHand = 5;
	}


	//public CharacterData(int s, int d, int c, int i, int w, int ch)
 //   {
 //       CharacterStats[(int)Stats.Strength] = s;
 //       CharacterStats[(int)Stats.Dexterity] = d;
 //       CharacterStats[(int)Stats.Constitution] = c;
 //       CharacterStats[(int)Stats.Intelligence] = i;
 //       CharacterStats[(int)Stats.Wisdom] = w;
 //       CharacterStats[(int)Stats.Strength] = ch;
  
 //   }

	//public CharacterData()
    //{
        
    //}

    // roll d Ds
    public int Roll(int d, int s)
    {
        int sum = 0;
        for (int i = 0; i < d; i++)
        {
            sum += Random.Range(1, s+1);
        }
        return sum;
    }

    public int GetStat(Stats s)
    {
        return CharacterStats[(int)s];
    }

	public void TakeDamage(int d, bool isPlayer)
	{
		if (d > 0)
			CurrentHealth -= d;

		CombatUI.instance.ChangeHealth(CurrentHealth, isPlayer);

		if (CurrentHealth <= 0)
			CombatManager.instance.EndGame(isPlayer);
	}

	public virtual bool StatCheck(Stats s, int n)
	{
		return false;
	}
    
}
