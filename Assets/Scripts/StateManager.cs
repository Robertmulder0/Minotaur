using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    public Idle idle = new Idle();
    public Chase chase = new Chase();
    public Stunned stunned = new Stunned();
    public Patrol patrol = new Patrol();
    public FindPlayer findPlayer = new FindPlayer();

    public float moveSpeed = 5.0f;
    public bool isStunned;

    public GameObject player;
    public GameObject gridManager;
    public GameObject patrolPoint;
    public GameObject[] patrolPointNodes = new GameObject[10];

    public Material[] material;
    Renderer rend;
    
    void Start()
    {
        currentState = idle;

        currentState.EnterState(this);

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];

    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(State state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void SwitchMat() //switch material when stunned
    {
        if (rend.sharedMaterial == material[1]){
            rend.sharedMaterial = material[0];
        } else {
            rend.sharedMaterial = material[1];
        }
    }

     public bool canSeePlayer()
    {
        Vector3 playerDir = (player.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerDir, out hit, Mathf.Infinity)) {
            if (hit.transform.gameObject == player) {
                return true;
            } else {
                return false;
            }
        }
        return false;
    }

}
