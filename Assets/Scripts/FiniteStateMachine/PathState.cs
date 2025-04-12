public class PathState : BaseState
{
    internal PathState(FSM _fsm) : base(_fsm)
    {

    }

    public override void Update()
    {
        fsm.agent.SetDestination(fsm.waypoints[0].position);
    }

    public override BaseState EvaluateConditions()
    {
        if (fsm.playerInRange)
            return new AttackState(fsm);
        return null;
    }
}
