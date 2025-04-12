using UnityEngine;
using UnityEngine.AI;

public class FSM : MonoBehaviour
{
    public Transform[] waypoints;
    public NavMeshAgent agent;

    public bool playerInRange = false;
    public Transform player;

    private BaseState currentState;

    private void Start()
    {
        currentState = new PathState(this);
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        BaseState newState = currentState.EvaluateConditions();

        if (newState is not null)
            currentState = newState;

        currentState.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }








}
