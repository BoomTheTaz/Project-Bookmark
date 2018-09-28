using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI  {

	
    public virtual Card[] DecideAttack(int ap, List<Card> hand)
    {
        return null;
    }

    public virtual Card DecideDefense(Card c, List<Card> hand)
    {
        return null;
    }
}
