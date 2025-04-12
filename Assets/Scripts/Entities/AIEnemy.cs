using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshMovement))]
public class AIEnemy : MonoBehaviour, IAmDamageable
{
    protected Animator _animator;

    public Animator Animator => _animator;

    [SerializeField]
    protected Health _health;

    protected NavMeshMovement _autonomousMover;

    public NavMeshMovement AutonomousMover => _autonomousMover;

    protected virtual void Awake()
    {
        //_animator = Animator != null ? Animator : GetComponentInChildren<Animator>();

        //_autonomousMover = AutonomousMover != null ? AutonomousMover : GetComponent<NavMeshMovement>();
    }

    protected virtual void Start()
    {
        //Animator.enabled = true;
    }

    protected virtual void Update()
    {
        //Animator.SetFloat("Velocity", AutonomousMover.NavMeshAgent.velocity.magnitude);


    }

    public virtual void TakeDamage(int damage)
    {
        _health.DecreaseCurrentHealth(damage);
    }
}
