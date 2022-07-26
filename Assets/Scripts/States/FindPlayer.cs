using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : State
{
    private Vector3 playerPos;
    public Node nextNode;

    public override void EnterState(StateManager npc)
    {
        Debug.Log("Entered Find Player State");

        //set patrol point to player pos so minotaur can pathfind to search for player
        playerPos = npc.player.transform.position;
        playerPos.y += 1.5f; //match minotaur height
        npc.patrolPoint.transform.position = playerPos;
    }

    public override void UpdateState(StateManager npc)
    {   
        if (npc.canSeePlayer()){
            npc.SwitchState(npc.chase);
        }

        //pathfind towards patrol point
        if (nextNode != npc.gridManager.GetComponent<Pathfinding>().endNode) {
            nextNode = npc.gridManager.GetComponent<Pathfinding>().nextNode;
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, nextNode.position, npc.moveSpeed * Time.deltaTime);
            npc.transform.LookAt(nextNode.position);
        } else {
            npc.transform.position = Vector3.MoveTowards(npc.transform.position, npc.patrolPoint.transform.position, npc.moveSpeed * Time.deltaTime);
        }

        if (npc.transform.position == npc.patrolPoint.transform.position){
            npc.SwitchState(npc.idle);
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
