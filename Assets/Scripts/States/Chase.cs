using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    Vector3 playerPos;
    public override void EnterState(StateManager npc)
    {
        Debug.Log("Entered chase state");
    }

    public override void UpdateState(StateManager npc)
    {
        playerPos = npc.player.transform.position;
        playerPos.y += 1.5f;

        npc.transform.position = Vector3.MoveTowards(npc.transform.position, playerPos, npc.moveSpeed * Time.deltaTime);
        
        //make minotaur face direction of movement
        npc.transform.LookAt(playerPos);

        if (!npc.canSeePlayer()){
            npc.SwitchState(npc.findPlayer);
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
