using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatsData : CharacterData {

	public RatsData()
    {
        DefaultSetup();
    }

    protected override void DefaultSetup()
    {
        StoreData(1, 1, 0, 50, new CardStats[] {
            new CardStats(CardType.ATK_Phys, 1, 1, 1, "Bite"),
            new CardStats(CardType.ATK_Phys, 1, 1, 1, "Bite"),
            new CardStats(CardType.ATK_Phys, 1, 1, 2, "Leaping Bite") });
    }
}
