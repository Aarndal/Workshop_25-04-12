using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public sealed class NavMeshMovement : MonoBehaviour, IAmAutonomousMovable
{
    private readonly int DefaultUpdateSpeed = 100;

    private bool _reachedTarget;

    public NavMeshAgent NavMeshAgent { get; private set; }
    public bool ReachedTarget
    {
        get => _reachedTarget;
        set
        {
            if (_reachedTarget != value)
                _reachedTarget = value;
        }
    }
    public int UpdateSpeed { get; private set; }
    public float DistanceToTarget => NavMeshAgent.remainingDistance;
    public float MinDistanceToTarget { get => NavMeshAgent.stoppingDistance; set => NavMeshAgent.stoppingDistance = value; }
    public Vector3 InitialPosition { get; private set; }
    public Vector3 CurrentPosition => this.transform.position;

    private void Awake()
    {
        NavMeshAgent = NavMeshAgent != null ? NavMeshAgent : GetComponent<NavMeshAgent>();
        UpdateSpeed = DefaultUpdateSpeed;
    }

    private void OnEnable()
        => InitialPosition = this.transform.position;

    private void Start()
        => NavMeshAgent.enabled = true;

    private void Update()
        => _reachedTarget = HasReachedTargetCheck();

    private bool HasReachedTargetCheck()
    {
        if (!NavMeshAgent.pathPending && NavMeshAgent.hasPath)
            if (DistanceToTarget <= MinDistanceToTarget || (NavMeshAgent.transform.position - NavMeshAgent.pathEndPosition).sqrMagnitude <= 0.1f)
                return true;
        return false;
    }

    public async void MoveTo(TargetProvider targetProvider)
    {
        if (!targetProvider.HasTarget)
        {
            NavMeshAgent.isStopped = true;
            Debug.Log($"{gameObject.name} has no target");
        }

        if (targetProvider.HasTarget)
        {
            NavMeshAgent.isStopped = false;
            NavMeshAgent.SetDestination(targetProvider.Target.position);
            await Task.Delay(UpdateSpeed);
        }
    }
}