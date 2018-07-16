using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats { Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma, NUM_ELEMENTS };

public class PlayerData {

	// ===== STATS =====
	int[] PlayerStats = new int[(int)Stats.NUM_ELEMENTS];


	public static PlayerData instance;

	public PlayerData (int s, int d, int c, int i, int w, int ch)
	{
		PlayerStats[(int)Stats.Strength] = s;
		PlayerStats[(int)Stats.Dexterity] = d;
		PlayerStats[(int)Stats.Constitution] = c;
		PlayerStats[(int)Stats.Intelligence] = i;
		PlayerStats[(int)Stats.Wisdom] = w;
		PlayerStats[(int)Stats.Strength] = ch;

		if (instance == null)
		    instance = this;
	}

    public PlayerData()
	{
		PlayerStats[(int)Stats.Strength] = Roll(3);
		PlayerStats[(int)Stats.Dexterity] = Roll(3);
		PlayerStats[(int)Stats.Constitution] = Roll(3);
		PlayerStats[(int)Stats.Intelligence] = Roll(3);
		PlayerStats[(int)Stats.Wisdom] = Roll(3);
		PlayerStats[(int)Stats.Charisma] = Roll(3);

		if (instance == null)
            instance = this;
	}

    // roll d D6
    int Roll(int d)
	{
		int sum = 0;
		for (int i = 0; i < d; i++)
		{
			sum += Random.Range(1, 7);
		}
		return sum;
	}

	public int GetStat(Stats s)
	{
		return PlayerStats[(int)s];
	}

	public bool StatCheck(Stats s, int n)
	{
		if (PlayerStats[(int)s] >= n)
			return true;
		return false;
	}
}
