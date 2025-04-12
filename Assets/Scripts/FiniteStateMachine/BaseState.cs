public abstract class BaseState
{
    public FSM fsm;

    internal BaseState(FSM _fsm)
    {
        fsm = _fsm;
    }

    public abstract void Update();
    public abstract BaseState EvaluateConditions();
}
