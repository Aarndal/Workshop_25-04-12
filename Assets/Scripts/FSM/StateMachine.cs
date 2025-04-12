using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class StateMachine
{
    private readonly ReaderWriterLockSlim Lock = new(LockRecursionPolicy.SupportsRecursion);
    private readonly int MinHistorySize = 5;

    private bool _isTransitioning = false;

    private State _currentState;
    private State _targetState;

    public State CurrentState
    {
        get => _currentState;
        protected set
        {
            if (States.Contains(value) && _currentState != value)
                _currentState = value;
        }
    }

    public HashSet<State> States { get; protected set; }
    public List<Transition> AnyTransitions { get; protected set; }
    public Stack<State> History { get; protected set; }


    public StateMachine()
    {
        States = new();
        AnyTransitions = new();
        History = new(MinHistorySize);
    }

    #region Unity Build-In Callbacks
    public async void OnStart()
    {
        await CurrentState.OnEnter();
    }

    public void OnFixedUpdate()
    {
        CurrentState.OnFixedUpdate();
    }

    public void OnUpdate()
    {
        SwitchState();

        CurrentState.OnUpdate();
    }

    public void OnLateUpdate()
    {
        CurrentState.OnLateUpdate();
    }
    #endregion

    #region Private Methods
    private async void SwitchState()
    {
        _targetState = GetTargetState();

        if (_targetState != null && _targetState != CurrentState && !_isTransitioning)
            await TransitionTo(_targetState);
    }

    private State GetTargetState()
    {
        foreach (Transition transition in AnyTransitions)
        {
            if (transition.Condition() == true)
                return transition.TargetState;
        }

        foreach (Transition transition in CurrentState.Transitions)
        {
            if (transition.Condition() == true)
                return transition.TargetState;
        }

        return null;
    }

    private async Task TransitionTo(State targetState)
    {
        _isTransitioning = true;

        AddState(targetState);

        if (History.Count >= States.Count && History.Count >= MinHistorySize) // check to regulate the size of the History
        {
            History.Clear();
            History.Push(CurrentState);
            //TO-DO: also add state before current state!!!
        }

        History.Push(targetState);

        Debug.LogWarning($"Transitioning from {CurrentState} to {targetState}");

        if (CurrentState != null)
            await CurrentState.OnExit();

        await targetState.OnEnter();
        CurrentState = targetState;

        _isTransitioning = false;
    }
    #endregion

    #region Public Methods
    public bool AddState(State state)
    {
        //---------------------------
        //https://josipmisko.com/posts/c-sharp-hashset

        try
        {
            Lock.EnterWriteLock();
            return States.Add(state); // No need of using the Contains method to check if the element is already contained within the HashSet
        }
        finally
        {
            if (Lock.IsWriteLockHeld) Lock.ExitWriteLock();
        }

        //---------------------------
    }


    public void AddAnyTransition(Transition transition)
    {
        if (!AnyTransitions.Contains(transition))
            AnyTransitions.Add(transition);
    }

    public void SetInitialState(State state)
    {
        AddState(state);
        History.Push(state);
        CurrentState = state;
    }
    #endregion
}