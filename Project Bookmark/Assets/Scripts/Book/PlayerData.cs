using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats { Power, Technique, Constitution, Magic, Wisdom, Charisma, NUM_ELEMENTS };

public class PlayerData : CharacterData {
    
    public PlayerData()
    {
        isPlayer = true;
    }

    public PlayerData(int power, int tech, int magic, int health, CardStats[] cards)
    {
        isPlayer = true;
        StoreData(power, tech, magic, health, cards);
    }
}
