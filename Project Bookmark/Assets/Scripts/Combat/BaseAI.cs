using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI  {

	
    public virtual Card[] DecideAttack(int ap, Card[] hand)
    {
        return null;
    }

    public virtual Card DecideDefense(Card c, Card[] hand)
    {
        return null;
    }
}
