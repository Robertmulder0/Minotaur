using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public bool isPlayerVisible;
    public Chase chase;

    public override State RunState()
    {
        if (isPlayerVisible)
        {
            return chase;
        } else {
            return this;
        }
    }
}
