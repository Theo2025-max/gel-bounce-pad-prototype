using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStateMachine))]
public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    private NavMeshAgent agent;
    private EnemyStateMachine stateMachine;

    private Vector3 spawnPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();

        spawnPosition = transform.position;
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("EnemyAI: No GameObject with the 'Player' tag was found.");
                return;
            }
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        if (stateMachine.CurrentState == EnemyStateMachine.EnemyState.Chasing)
        {
            agent.SetDestination(player.position);
        }
    }

    public void StartChasing()
    {
        stateMachine.SetState(EnemyStateMachine.EnemyState.Chasing);
    }

    public void StopMoving()
    {
        agent.ResetPath();
    }

    public void ReturnToSpawn()
    {
        if (!agent.enabled)
            return;

        stateMachine.SetState(EnemyStateMachine.EnemyState.Idle);

        agent.isStopped = false;
        agent.SetDestination(spawnPosition);

        EnemyDetection detection = GetComponentInChildren<EnemyDetection>();

        if (detection != null)
        {
            detection.ResetDetection();
        }

        Debug.Log($"{name} returning to spawn.");
    }
}