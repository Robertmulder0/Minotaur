using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{   
    public Node nextNode;
    private GameObject[] patrolPointNodes;

    public override void EnterState(StateManager npc)
    {
        Debug.Log("Entered Patrol State");
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
        
        //find random position for patrol point
        patrolPointNodes = npc.patrolPointNodes;
        if (npc.transform.position == npc.patrolPoint.transform.position){
            int randomNode = Random.Range(0, patrolPointNodes.Length);
            npc.patrolPoint.transform.position = patrolPointNodes[Random.Range(0, randomNode)].transform.position;
            //if next patrol point position is not within 75 units of player, reroll
            while (Vector3.Distance(npc.patrolPoint.transform.position, npc.player.transform.position) > 75){
                randomNode = Random.Range(0, patrolPointNodes.Length);
                npc.patrolPoint.transform.position = patrolPointNodes[Random.Range(0, randomNode)].transform.position;
            }
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
