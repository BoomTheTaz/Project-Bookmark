using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CardEffect {

    public EffectTypes effectType { get; private set; }
    public int effectAmount { get; private set; }


    public CardEffect (EffectTypes t, int i)
    {
        effectType = t;
        effectAmount = i;
    }
	
}
