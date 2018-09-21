using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAI : BaseAI {

    public override Card[] DecideAttack(int ap)
    {
        // TODO: Write this code,
        // Should probably just take all the remaining ap and spend it to maximize damage
        // For Later: If there is rollover AP, consider taking that into account

        return null;
    }

    public override Card[] DecideDefense(int ap)
    {
        // TODO: Write this code,
        // Should probably not defend unless life threatening for this AI type
        // For Later: If there is rollover AP, consider taking that into account

        return null;
    }
}
