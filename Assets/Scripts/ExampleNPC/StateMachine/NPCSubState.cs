using System.Collections;
using System.Collections.Generic;

public class NPCSubState : BaseState<NPCSubStateKey>
{
    protected NPCController ctx;
    protected NPCStateMachine stateMachine;
    protected List<NPCSubStateNext> nextState = new List<NPCSubStateNext>();
    public NPCSubState(NPCSubStateKey key, NPCStateMachine stateMachine, NPCController ctx, List<NPCSubStateNext> nextState)
    : base(key)
    {
        this.ctx = ctx;
        this.stateMachine = stateMachine;
        this.nextState = nextState;
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }

    public override NPCSubStateKey GetNextState()
    {
        if(nextState != null)
        {
            foreach (var item in nextState)
            {
                bool isAllMet = true;
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