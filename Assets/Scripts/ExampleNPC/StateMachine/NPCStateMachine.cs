using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : StateMachine<NPCSuperStateKey, NPCSubStateKey>
{
    [SerializeField] NPCController ctx;

    NPCSuperState _currentSuperState; public NPCSuperState currentSuperState { get { return _currentSuperState; } }
    NPCSubState _currentSubState; public NPCSubState currentSubState { get { return _currentSubState; } }

    void Awake()
    {
        InitializeEvents();
        InitializeStates();
    }

    private void InitializeStates()
    {
        
    }

    private void InitializeEvents()
    {
        
    }

    protected override void Start()
    {
        
    }

    protected override void TransitionToSubState(NPCSubStateKey nextSubStateKey)
    {
        _isSubTransition = true;
        _currentSubState.ExitState();
        _currentSubState = (NPCSubState)_subStates[nextSubStateKey];
        _currentSubState.EnterState();
        _isSubTransition = false;
    }

    protected override void TransitionToSuperState(NPCSuperStateKey nextSuperStateKey)
    {
        _isSuperTransition = true;
        _currentSuperState.ExitState();
        if(_currentSubState != null)
        {
            NPCSubState _npcSubState = currentSubState;
            _npcSubState.ExitState();
            _currentSubState = null;
        }
        _currentSuperState = (NPCSuperState)_superStates[nextSuperStateKey];
        _currentSuperState.EnterState();
        _isSuperTransition = false;
    }

    protected override void Update()
    {
        NPCSuperStateKey nextSuperStateKey = _currentSuperState.GetNextState();
        if(!_isSuperTransition && nextSuperStateKey.Equals(_currentSuperState.stateKey))
        {
            _currentSuperState.UpdateState();
        }
        else
        if(!_isSuperTransition)
        {
            TransitionToSuperState(nextSuperStateKey);
        }

        if(_currentSubState != null)
        {
            NPCSubStateKey nextSubStateKey = _currentSubState.GetNextState();
            if(!_isSubTransition && nextSubStateKey.Equals(_currentSubState.stateKey))
            {
                _currentSubState.UpdateState();
            }
            else
            if(!_isSubTransition)
            {
                TransitionToSubState(nextSubStateKey);
            }
        }
    }

    public void AssignInitialSuperState(NPCSuperStateKey state)
        {
            _currentSuperState = (NPCSuperState)_superStates[state];
            // _currentSuperState.InitializeSubState();
            _currentSuperState.EnterState();
        }

        public void AssignSubState(NPCSubStateKey state)
        {
            _currentSubState = (NPCSubState)_subStates[state];
            _currentSubState.EnterState();
        }
}

[Serializable]
public class NPCSuperStateMain 
{
    public NPCSuperStateKey superState;
    public List<NPCSuperStateNext> _superNextStates = new List<NPCSuperStateNext>();
    public List<NPCSubStateMain> _subStates = new List<NPCSubStateMain>();
}

[Serializable]
public class NPCSuperStateNext
{
    public NPCSuperStateKey nextState;
    public List<NPCConditions> conditions = new List<NPCConditions>();
}

[Serializable]
public class NPCSubStateMain
{
    public NPCSubStateKey subState;
    public NPCSuperState superState;
}

[Serializable]
public class NPCSubStateNext
{
    public NPCSubStateKey nextState;
    public List<NPCConditions> conditions = new List<NPCConditions>();
}