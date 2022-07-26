using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{   
    public GameObject [] patrolPoints;
    public Node nextNode;

    public override void EnterState(StateManager npc)
    {
        
    }

     public override void UpdateState(StateManager npc)
    {   
        nextNode = npc.gridManager.GetComponent<Pathfinding>().nextNode;
        npc.transform.position = Vector3.MoveTowards(npc.transform.position, nextNode.position, npc.moveSpeed * Time.deltaTime);
    }

    public override void OnCollisionEnter(StateManager npc, Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Bullet")){
            npc.SwitchState(npc.stunned);
        }
    }
}
