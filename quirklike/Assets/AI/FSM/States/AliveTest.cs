using System;
using UnityEngine;

public class AliveTest : State
{
    private readonly Wanderer wanderer;
    public AliveTest(Wanderer w){
        // Debug.Log("Alive Ins");
        wanderer = w;
    }
    public void Tick(){
        // Debug.Log("I am alive");
    }
    public void OnExit(){

    }
    public void OnEnter(){
        // Debug.Log("Breathe in breath out");
    }
}
