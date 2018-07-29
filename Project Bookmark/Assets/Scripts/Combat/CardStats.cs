using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStats {

	public CardType Type { get; protected set; }
	public int ATK { get; protected set; }
	public int DEF { get; protected set; }
	public int AP { get; protected set; }
	public string Name { get; protected set; }


	public CardStats(CardType c, int a, int d, int ap, string name)
	{
		Type = c;
		ATK = a;
		DEF = d;
		AP = ap;
		Name = name;
	}
}
