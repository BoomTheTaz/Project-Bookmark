using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTemplate : MonoBehaviour {

	public static CardStats GetTemplate(int i)
	{
		CardStats result = null;
		switch (i)
		{

			case 0:
				result = new CardStats(CardType.ATK_Phys, 1, 1, 1, "Thrust");
				break;
			case 1:
				result = new CardStats(CardType.ATK_Phys, 2, 1, 2, "Slash");
                break;
            case 2:
				result = new CardStats(CardType.DEF_Mag, 1, 2, 2, "Shield");
                break;
            case 3:
				result = new CardStats(CardType.ATK_Mag, 3, 1, 3, "Fireball");
                break;
            case 4:
				result = new CardStats(CardType.DEF_Phys, 1, 2, 2, "Deflect");
                break;
            case 5:
				result = new CardStats(CardType.DEF_Phys, 1, 1, 1, "Brace");
                break;


			default:
				Debug.LogError("Could not find CardTemplate with identifier " + i.ToString());
				return null;            
		}

		result.SetTemplateID(i);

		return result;
	}
}
