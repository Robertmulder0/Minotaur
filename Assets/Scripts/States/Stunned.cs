using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : State
{
    public override void EnterState(StateManager npc)
    {
        Debug.Log("Entered Stunned State");
    }

    public override void UpdateState(StateManager npc)
    {

    }

    public override void OnCollisionEnter(StateManager npc, Collision collision)
    {

    }
}
