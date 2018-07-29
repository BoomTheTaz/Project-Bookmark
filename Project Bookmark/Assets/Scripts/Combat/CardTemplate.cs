using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTemplate : MonoBehaviour {

	public static CardStats GetTemplate(int i)
	{
		switch (i)
		{

			case 0:
				return new CardStats(CardType.ATK_Phys, 1, 1, 1, "Thrust");
			case 1:
				return new CardStats(CardType.ATK_Phys, 2, 1, 2, "Slash");
            case 2:
				return new CardStats(CardType.DEF_Mag, 1, 2, 2, "Shield");
            case 3:
				return new CardStats(CardType.ATK_Mag, 3, 1, 3, "Fireball");
            case 4:
				return new CardStats(CardType.DEF_Phys, 1, 2, 2, "Deflect");
            case 5:
				return new CardStats(CardType.DEF_Phys, 1, 1, 1, "Brace");


			default:
				Debug.LogError("Could not find CardTemplate with identifier " + i.ToString());
				return null;
		}
	}
}
