using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;

public class StateMachine
{
   private State CurrentState;
   
   private Dictionary<Type, List<Transition>> D_Transitions = new Dictionary<Type,List<Transition>>();
   private List<Transition> L_CurrentTransitions = new List<Transition>();
   private List<Transition> L_AnyTransitions = new List<Transition>();
   
   private static List<Transition> L_EmptyTransitions = new List<Transition>(0);

   public void Tick()
   {
      var transition = GetTransition();
      if (transition != null)
         SetState(transition.To);
      
      CurrentState?.Tick();
   }

   public void SetState(State state)
   {
      if (state == CurrentState)
         return;
      
      CurrentState?.OnExit();
      CurrentState = state;
      
      D_Transitions.TryGetValue(CurrentState.GetType(), out L_CurrentTransitions);
      if (L_CurrentTransitions == null)
         L_CurrentTransitions = L_EmptyTransitions;
      
      CurrentState.OnEnter();
   }

   public void AddTransition(State from, State to, Func<bool> predicate)
   {
      if (D_Transitions.TryGetValue(from.GetType(), out var transitions) == false)
      {
         transitions = new List<Transition>();
         D_Transitions[from.GetType()] = transitions;
      }
      
      transitions.Add(new Transition(to, predicate));
   }

   public void AddAnyTransition(State state, Func<bool> predicate)
   {
      L_AnyTransitions.Add(new Transition(state, predicate));
   }

   private class Transition
   {
      public Func<bool> Condition {get; }
      public State To { get; }

      public Transition(State to, Func<bool> condition)
      {
         To = to;
         Condition = condition;
      }
   }

   private Transition GetTransition()
   {
      foreach(var transition in L_AnyTransitions)
         if (transition.Condition())
            return transition;
      
      foreach (var transition in L_CurrentTransitions)
         if (transition.Condition())
            return transition;

      return null;
   }
}