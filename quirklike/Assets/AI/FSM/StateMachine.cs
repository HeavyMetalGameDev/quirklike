using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;
using System;

public class StateMachine 
{
    private State CurrentState;

    //saves all the transistions in a dict
    private Dictionary<Type, List<Transition>> D_Transitions = new Dictionary<Type, List<Transition>>();
    //switching out whatever our active transisions might be in a given state
    private List<Transition> L_CurrentTrasitions = new List<Transition>();
    private List<Transition> L_AnyTransitions = new List<Transition>();

    private static List<Transition> EmptyTransitions = new List<Transition>();


    public void Tick()
    {
        var Transition = GetTransition();
        if (Transition != null)
        {
            SetState(Transition.To);
        }
        CurrentState?.Tick();
    }

    public void SetState(State state)
    {
        if(state == CurrentState) return;

        CurrentState?.OnExit();
        CurrentState = state;

        D_Transitions.TryGetValue(CurrentState.GetType(), out var CurTransition);
        if(CurTransition != null)
        {
            CurTransition = EmptyTransitions;
        }
        CurrentState.OnEnter();
    }

    public void AddTransition(State From, State To, Func<bool> condition)
    {
        
        if(D_Transitions.TryGetValue(From.GetType(), out var transistion) == false){
            transistion = new List<Transition>();
            D_Transitions[From.GetType()] = transistion;
        }
        
        Debug.Log("added tras");
        transistion.Add(new Transition(To, condition));
    }

    public void AddAnyTransition(State state, Func<bool> condition){
        L_AnyTransitions.Add(new Transition(state, condition));
    }

    private class Transition
    {
        public Func<bool> Condition { get; } 
        public State To { get; }

        public Transition(State to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }
    private Transition GetTransition()
    {
        //these are transitions from ANY state. No from, they always exist
        foreach(var transition in L_AnyTransitions){
            if (transition.Condition())
                return transition;
        }
        
        foreach(var transistion in L_CurrentTrasitions){
            if (transistion.Condition())
                return transistion;  
        }

        return null;
    }
}
