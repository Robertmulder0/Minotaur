using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public bool ifPlayerVisible;

    public override void EnterState(StateManager npc)
    {
        
    }

    public override void UpdateState(StateManager npc)
    {
        if (canSeePlayer()){
            npc.SwitchState(npc.chase);
        }
    }

    public override void OnCollisionEnter(StateManager npc, Collision collision)
    {

    }

    public bool canSeePlayer()
    {
        return true;
    }
}
