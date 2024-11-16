using UnityEngine;

public abstract class BaseCondition<SuperStateKey, SubStateKey> : ScriptableObject
where SuperStateKey : BaseStateKey
where SubStateKey : BaseStateKey
{
    public abstract bool Evaluate(StateMachine<SuperStateKey, SubStateKey> stateMachine, MonoBehaviour context);
} 
