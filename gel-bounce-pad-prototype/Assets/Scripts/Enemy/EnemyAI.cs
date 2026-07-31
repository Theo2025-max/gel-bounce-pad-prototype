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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyStateMachine>();
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

        Debug.Log($"Is On NavMesh: {agent.isOnNavMesh}");
    }

    private void Update()
    {
        if (player == null)
            return;

        if (stateMachine.CurrentState == EnemyStateMachine.EnemyState.Chasing)
        {
            Debug.Log("Setting Destination");
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
}