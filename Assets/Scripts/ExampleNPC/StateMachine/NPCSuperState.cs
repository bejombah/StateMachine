using System.Collections.Generic;

public class NPCSuperState : BaseState<NPCSuperStateKey>
{
    protected NPCController ctx;
    protected NPCStateMachine stateMachine;
    protected List<NPCSuperStateNext> nextState = new List<NPCSuperStateNext>();
    protected NPCSubStateKey initialSubState;
    public NPCSuperState(
        NPCSuperStateKey key,
        NPCStateMachine stateMachine,
        NPCController ctx,
        List<NPCSuperStateNext> nextState,
        NPCSubStateKey initialSubState
        )
    : base(key)
    {
        this.ctx = ctx;
        this.stateMachine = stateMachine;
        this.nextState = nextState;
        if(initialSubState != null)
        {
            this.initialSubState = initialSubState;
        }
    }

    public override void EnterState()
    {
        base.EnterState();
        InitializeSubState();
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }

    public virtual void InitializeSubState() 
    {
        if(initialSubState != null)
        {
            stateMachine.AssignSubState(initialSubState);
        }
    }

    public override NPCSuperStateKey GetNextState()
    {
        if(nextState != null)
        {
            foreach (var item in nextState)
            {
                bool isAllMet = true;
                // validate each condition on the list, if all true return the next state
                foreach (var condition in item.conditions)
                {
                    if(!condition.Evaluate(stateMachine, ctx))
                    {
                        isAllMet = false;
                        break;
                    }
                }                    

                if(isAllMet)
                {
                    return item.nextState;
                }
            }
        }
        return stateKey;
    }
}