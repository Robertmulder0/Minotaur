using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    public Idle idle = new Idle();
    public Chase chase = new Chase();
    public Stunned stunned = new Stunned();

    public float moveSpeed = 5.0f;
    public bool isStunned;

    public GameObject player;
    
    void Start()
    {
        currentState = idle;

        currentState.EnterState(this);
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
}
