using UnityEngine;

public abstract class State
{
    public abstract void EnterState(StateManager npc);

    public abstract void UpdateState(StateManager npc);

    public abstract void OnCollisionEnter(StateManager npc, Collision collision);
}
