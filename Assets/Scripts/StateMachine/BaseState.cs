using UnityEngine;

public abstract class BaseState<StateKey>
where StateKey : ScriptableObject
{
    public BaseState(StateKey key)
    {
        stateKey = key;
    }

    public StateKey stateKey { get; set; }

    public virtual void EnterState() {}
    public virtual void UpdateState() {}
    public virtual void ExitState() {}

    public abstract StateKey GetNextState();
}
