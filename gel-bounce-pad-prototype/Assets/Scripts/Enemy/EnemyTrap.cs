using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyTrap : MonoBehaviour, IGelTarget
{
    [Header("Jelly")]
    [SerializeField] private Transform jellySpawnPoint;
    [SerializeField] private GameObject jellyPrefab;

    private EnemyStateMachine stateMachine;
    private EnemyAI enemyAI;
    private NavMeshAgent agent;

    private bool isTrapped = false;

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyAI = GetComponent<EnemyAI>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void Trap()
    {
        if (isTrapped)
            return;

        isTrapped = true;

        // Change the gameplay state.
        stateMachine.SetState(EnemyStateMachine.EnemyState.Trapped);

        // Stop all navigation.
        agent.isStopped = true;
        agent.ResetPath();
        agent.enabled = false;

        // Disable the AI script.
        enemyAI.enabled = false;

        // Spawn the jelly.
        if (jellyPrefab != null && jellySpawnPoint != null)
        {
            Instantiate(jellyPrefab,jellySpawnPoint.position,jellySpawnPoint.rotation);
        }

        Debug.Log($"{name} has been trapped.");
    }
}