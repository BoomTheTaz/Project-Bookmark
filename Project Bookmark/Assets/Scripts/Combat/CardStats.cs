using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStats {

	public CardType Type { get; protected set; }
	public int ATK { get; protected set; }
	public int DEF { get; protected set; }
	public int AP { get; protected set; }
	public string Name { get; protected set; }
	public int TemplateID { get; protected set; }

    public CardEffect[] AttackEffects { get; protected set; }
    public CardEffect[] DefenseEffects { get; protected set; }


    public CardStats(CardType c, int a, int d, int ap, string name, CardEffect[] attackFX = null, CardEffect[] defenseFX = null)
	{
		Type = c;
		ATK = a;
		DEF = d;
		AP = ap;
		Name = name;

        AttackEffects = attackFX;
        DefenseEffects = defenseFX;
	}

    public void SetTemplateID(int id)
	{
		TemplateID = id;
	}

    public CardEffect[] GetAttackEffects()
    {
        return AttackEffects;
    }

    public CardEffect[] GetDefenseEffects()
    {
        return DefenseEffects;
    }
}
