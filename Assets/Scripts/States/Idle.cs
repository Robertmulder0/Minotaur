using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public bool ifPlayerVisible;

    public override void EnterState(StateManager npc)
    {
        Debug.Log("Entered Idle State");
    }

    public override void UpdateState(StateManager npc)
    {
        if (npc.canSeePlayer()){
            npc.SwitchState(npc.chase);
        } else {
            npc.SwitchState(npc.patrol);
        }
    }

    public override void OnCollisionEnter(StateManager npc, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Bullet")){
            npc.SwitchState(npc.stunned);
        }
    }

}
