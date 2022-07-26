using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunned : State
{
    private float stunTimer;
    
    

    public override void EnterState(StateManager npc)
    {
        Debug.Log("Entered Stunned State");
        npc.SwitchMat();
        stunTimer = 7.0f;
    }

    public override void UpdateState(StateManager npc)
    {
        stunTimer -= Time.deltaTime;
        
        if (stunTimer <= 0.0f){
            npc.SwitchState(npc.idle);
            npc.SwitchMat();
            
        }
    }

    public override void OnCollisionEnter(StateManager npc, Collision collision)
    {

    }
}
