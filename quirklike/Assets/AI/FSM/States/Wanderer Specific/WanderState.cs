using UnityEngine;
using UnityEngine.AI;

public class WanderState : State
{
    private readonly Wanderer Wanderer;
    private readonly NavMeshAgent NavAgent;

    public WanderState(Wanderer w, NavMeshAgent a)
    {
        Wanderer = w;
        NavAgent = a;
    }

    public void Tick(){
        
    }

    public void OnEnter(){
        NavAgent.enabled  = true;
    }

    public void OnExit(){
        NavAgent.enabled = false;
    }
}
