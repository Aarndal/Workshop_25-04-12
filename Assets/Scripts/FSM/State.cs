using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Serializable]
public abstract class State
{
    public StateMachine FSM { get; protected set; }
    public AIEnemy Entity { get; protected set; }
    public List<Transition> Transitions { get; protected set; }

    public State(StateMachine fsm, AIEnemy entity)
    {
        FSM = fsm;
        Entity = entity;

        Transitions = new();
    }

    public async virtual Task OnEnter() { await Task.Yield(); }

    public virtual void OnFixedUpdate() { }

    public virtual void OnUpdate() { }

    public virtual void OnLateUpdate() { }

    public async virtual Task OnExit() { await Task.Yield(); }

    public void AddTransition(Transition transition)
    {
        if (!Transitions.Contains(transition))
            Transitions.Add(transition);
    }
}
