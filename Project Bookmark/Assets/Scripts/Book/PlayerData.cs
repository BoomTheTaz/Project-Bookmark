using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats { Power, Technique, Constitution, Magic, Wisdom, Charisma, NUM_ELEMENTS };

public class PlayerData : CharacterData {
    
    void Awake()
	{
		CharacterStats[(int)Stats.Power] = Random.Range(3,6);
		CharacterStats[(int)Stats.Technique] = Random.Range(3, 6);
		CharacterStats[(int)Stats.Constitution] = Random.Range(3, 6);
		CharacterStats[(int)Stats.Magic] = Random.Range(3, 6);
		CharacterStats[(int)Stats.Wisdom] = Random.Range(3, 6);
		CharacterStats[(int)Stats.Charisma] = Random.Range(3, 6);

        // ======= TEMP HARD CODE ========
        MaxAP = 3;
        MaxHealth = 100;
        CurrentHealth = 100;
        MaxCards = 10;
        CardsInHand = 5;

		WeaponPhys = 2;
		WeaponMag = 2;
		ArmorMag = 1;
		ArmorPhys = 1;
        

		//Debug.Log("Power: " + CharacterStats[(int)Stats.Power].ToString());
		//Debug.Log("Technique: " + CharacterStats[(int)Stats.Technique].ToString());
		//Debug.Log("Magic: " + CharacterStats[(int)Stats.Magic].ToString());

	}

	public bool StatCheck(Stats s, int n)
	{
		if (CharacterStats[(int)s] >= n)
			return true;
		return false;
	}

    
}
