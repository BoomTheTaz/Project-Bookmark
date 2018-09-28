using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : BaseAI {

    // TODO: Decide if this should return an attack plan (i.e. Card[]) or one attack card
    public override Card[] DecideAttack(int ap, List<Card> hand)
    {
        // TODO: Write this code,
        // Should probably just take all the remaining ap and spend it to maximize damage
        // For Later: If there is rollover AP, consider taking that into account

        Card max = null;

        // Simply get max attack
        foreach (var item in hand)
        {
            if (max == null)
                max = item;
            else if (max.ATK < item.ATK)
                max = item;
        }

        return new Card[] { max };


        /*
         * TODO: Improve this AI
         * 
         * ===========
         *    LOGIC
         * ===========
         * 
         * 
         * - Looking for highest combination of cards
         * - Maybe just sort by a goodness value determined by ATK/AP + a mod for special effects
         * - Maybe create the best combination of one value cards, try to replace with two ap cards, etc.
         *      - At this point it may be better to do nested for loops,
         *      - Yeah probably this to create an attack plan, may have to redo if AP changes by enemy card effect
         * 
         */
    }

    public override Card DecideDefense(Card c, List<Card> hand)
    {
        // TODO: Write this code,
        // Should probably not defend unless life threatening for this AI type
        // For Later: If there is rollover AP, consider taking that into account


        // This ai will not defend
        return null;



        /*
         * TODO: Improve this AI
         * 
         * ===========
         *    LOGIC
         * ===========
         * 
         * - Maybe allow for desperation, wherein will protect self from critical blows,
         *   or if health is below a certain threshold
         * 
         * 
         */
    }
}
