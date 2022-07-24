using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    public State currentState;
    // Update is called once per frame
    void Update()
    {
        RunState();
    }

    private void RunState()
    {
        State nextState = null; //initialize null to stop errors
        if (currentState != null)
        {
            nextState = currentState.RunState();
        }

        if (nextState != null)
        {
            currentState = nextState;
        }
    }
}
