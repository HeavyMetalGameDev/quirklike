using UnityEngine;

public class IdleState : State
{
    private readonly Wanderer wanderer;
    
    public IdleState(Wanderer w){
        this.wanderer = w;
    }

    public void Tick()
    { 
        // Debug.Log("birthtick");
    }

    public void OnEnter(){
        // Debug.Log("I am birthed");
    }

    public void OnExit(){
        // Debug.Log("birth Exit");
    }
}
