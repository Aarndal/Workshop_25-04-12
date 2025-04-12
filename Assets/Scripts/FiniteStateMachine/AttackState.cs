using UnityEngine;

public class AttackState : BaseState
{
    float shootCounter = 1.0f;
    float secondsAfterShoot = 0;
    internal AttackState(FSM _fsm) : base(_fsm)
    {
       
    }

    public override void Update()
    {
        fsm.agent.SetDestination(fsm.player.position);

        secondsAfterShoot += Time.deltaTime;

        if (secondsAfterShoot > shootCounter)
        {
            secondsAfterShoot = 0;
        }
    }

    public override BaseState EvaluateConditions()
    {
        if (!fsm.playerInRange)
            return new PathState(this.fsm);
        return null;
    }
}
