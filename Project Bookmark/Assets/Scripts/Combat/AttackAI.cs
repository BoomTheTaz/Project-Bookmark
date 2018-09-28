using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : BaseAI {

    int counter;
    Card[] toReturn;
    Card[] temp;
    Card[] currentHand;
    int AvailableAP;

    // TODO: Decide if this should return an attack plan (i.e. Card[]) or one attack card
    public override Card[] DecideAttack(int ap, Card[] hand)
    {
        // TODO: Write this code,
        // Should probably just take all the remaining ap and spend it to maximize damage
        // For Later: If there is rollover AP, consider taking that into account



        currentHand = hand;
        AvailableAP = ap;

        int iterations = Mathf.Min(ap, hand.Length);
        
        currentMaxATK = 0;
        toReturn = new Card[iterations];
        temp = new Card[iterations];

        RecursiveLoop(0, 0);
       
        return toReturn;





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


    // NEED TO GO FROM PREVIOUS CARD NUMBER ON, CURRENTLY NOT DOING THAT, MAY NEED THIRD VARIABLE

    void RecursiveLoop(int start, int id)
    {
        // Go from counter to handsize
        int loopID = id;

        for (int i = start; i < currentHand.Length; i++)
        {

            // Add next card element only if have enough AP
            if (CheckAP(loopID, i) == true)
                temp[loopID] = currentHand[i];
            //
            else
            {
                temp[loopID] = null;
                CheckIfBetterPlan(loopID);
                continue;
            }
            // ADD CHECK FOR AP LIMIT
            if (loopID < currentHand.Length-1 )
            {
                RecursiveLoop(i+1, loopID + 1);
            }
            else
            {
                CheckIfBetterPlan(loopID);
                
            }
        }
       
    }

    void CheckIfBetterPlan(int id)
    {

        // Clear elements past loop id
        for (int i = id+1; i < temp.Length; i++)
        {
            temp[i] = null;
        }


        int tempATK = SumATK(temp);

        if (currentMaxATK == 0 || currentMaxATK < tempATK)
        {
            // Log new max
            currentMaxATK = tempATK;
            int total = 0;
            // transfer result to toReturn;
            for (int ii = 0; ii < temp.Length; ii++)
            {
              
                toReturn[ii] = temp[ii];
                if (temp[ii] != null)
                    total += temp[ii].AP;
            }

            Debug.Log("AP of new Best: " + total.ToString());

        }
    }

    bool CheckAP(int i, int next)
    {
        int t = 0;
        for (int ii = 0; ii < i; ii++)
        {
            if (temp[ii] != null)
                t += temp[ii].AP;
            else
                Debug.Log("Temp does not have element " + ii.ToString());
        }

        t += currentHand[next].AP;

        if (t <= AvailableAP)
        {
            return true;
        }
        else
            return false;

       
    }

    int currentMaxATK;
    int SumATK(Card[] c)
    {
        int total = 0;
        foreach (Card card in c)
        {
            if (card != null)
                total += card.ATK;
            else
                break;
        }

        return total;
    }

    public override Card DecideDefense(Card c, Card[] hand)
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
