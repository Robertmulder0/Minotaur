using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public bool isPlayerVisible;
    public Idle idle;

    public override State RunState()
    {
        if (isPlayerVisible)
        {
            return this;
        } else {
            return idle;
        }
    }
}
