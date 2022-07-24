using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public override void EnterState(StateManager npc)
    {
        Debug.Log("Entered chase state");
    }

    public override void UpdateState(StateManager npc)
    {
        npc.transform.position = Vector3.MoveTowards(npc.transform.position, npc.player.transform.position, npc.moveSpeed * Time.deltaTime);

    }

    public override void OnCollisionEnter(StateManager npc, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Bullet")){
            npc.SwitchState(npc.stunned);
        }
    }
}
