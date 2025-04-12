
public class AIEnemyState : State
{
    public AIEnemy AIEnemy { get; protected set; }
    public TargetProvider TargetProvider { get; protected set; }

    public AIEnemyState(StateMachine fsm, AIEnemy entity, TargetProvider targetProvider) : base(fsm, entity)
    {
        AIEnemy = entity;
        TargetProvider = targetProvider;
    }
}