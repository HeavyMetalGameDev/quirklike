using Unity.VisualScripting;
using UnityEngine;

public class BirthTest : State
{
    private readonly Wanderer wanderer;
    
    public BirthTest(Wanderer w){
        Debug.Log("instanciate");
        this.wanderer = w;
    }

    public void Tick()
    {
        Debug.Log("birthtick");
    }

    public void OnEnter(){
        Debug.Log("I am birthed");
    }

    public void OnExit(){
        Debug.Log("birth Exit");
    }
}