﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStats {

	public CardType Type { get; protected set; }
	public int ATK { get; protected set; }
	public int DEF { get; protected set; }
	public int AP { get; protected set; }
	public string Name { get; protected set; }
	public int TemplateID { get; protected set; }
    public int EffectInt { get; protected set; }
    public EffectTypes EffectType { get; protected set; }




    public CardStats(CardType c, int a, int d, int ap, string name, int e = 0, EffectTypes t = EffectTypes.NONE)
	{
		Type = c;
		ATK = a;
		DEF = d;
		AP = ap;
		Name = name;

        EffectInt = e;
        EffectType = t;
	}

    public void SetTemplateID(int id)
	{
		TemplateID = id;
	}
}
