using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI  {

	
    public virtual Card[] DecideAttack(int ap)
    {
        return null;
    }

    public virtual Card[] DecideDefense(int ap)
    {
        return null;
    }
}
