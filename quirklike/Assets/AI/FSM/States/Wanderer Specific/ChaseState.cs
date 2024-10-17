using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    private readonly Wanderer wanderer;
    public ChaseState(Wanderer w){
        wanderer = w;
    }

    public void Tick(){

    }
    public void OnEnter(){
        Debug.Log("Current State");
    }
    public void OnExit(){

    }
}
