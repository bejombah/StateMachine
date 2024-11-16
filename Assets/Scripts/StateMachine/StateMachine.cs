using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StateMachine<SuperStateKey, SubStateKey> : MonoBehaviour
where SuperStateKey : BaseStateKey
where SubStateKey : BaseStateKey
{
    protected Dictionary<SuperStateKey, BaseState<SuperStateKey>> _superStates = new Dictionary<SuperStateKey, BaseState<SuperStateKey>>();
    protected Dictionary<SubStateKey, BaseState<SubStateKey>> _subStates = new Dictionary<SubStateKey, BaseState<SubStateKey>>();

    protected bool _isSuperTransition = false;
    protected bool _isSubTransition = false;

    protected abstract void Start();
    protected abstract void Update();

    protected abstract void TransitionToSuperState(SuperStateKey superStateKey);
    protected abstract void TransitionToSubState(SubStateKey subStateKey);
}
