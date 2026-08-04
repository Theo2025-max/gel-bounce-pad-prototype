using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStateMachine))]
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAudio))]
public class EnemyTrap : MonoBehaviour, IGelTarget
{
    [Header("Jelly")]
    [SerializeField] private Transform jellySpawnPoint;
    [SerializeField] private GameObject jellyPrefab;

    [Header("Lifetime")]
    [SerializeField] private float trapDuration = 8f;
    [SerializeField] private float wobbleDuration = 2f;

    [Tooltip("Delay after the warning voice line before the wobble begins.")]
    [SerializeField] private float warningDelay = 1.25f;

    [Header("Effects")]
    [SerializeField] private GameObject explosionPrefab;

    private EnemyStateMachine stateMachine;
    private EnemyAI enemyAI;
    private NavMeshAgent agent;
    private EnemyAudio enemyAudio;

    public bool isTrapped = false;
    private GameObject spawnedJelly;

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyAI = GetComponent<EnemyAI>();
        agent = GetComponent<NavMeshAgent>();
        enemyAudio = GetComponent<EnemyAudio>();
    }

    public void Trap()
    {
        if (isTrapped)
            return;

        isTrapped = true;

        stateMachine.SetState(EnemyStateMachine.EnemyState.Trapped);

        agent.isStopped = true;
        agent.ResetPath();
        agent.enabled = false;

        enemyAI.enabled = false;

        if (jellyPrefab != null && jellySpawnPoint != null)
        {
            spawnedJelly = Instantiate(jellyPrefab,jellySpawnPoint.position,jellySpawnPoint.rotation);
        }

        StartCoroutine(TrapLifetimeRoutine());
    }

    private IEnumerator TrapLifetimeRoutine()
    {
        // Time before the warning is played.
        float stableTime = trapDuration - wobbleDuration - warningDelay;

        // Prevent negative wait times.
        stableTime = Mathf.Max(0f, stableTime);

        yield return new WaitForSeconds(stableTime);

        // Play the warning first.
        enemyAudio?.PlayTrapGrunt();

        // Wait before starting the wobble.
        yield return new WaitForSeconds(warningDelay);

        BeginWobble();

        yield return new WaitForSeconds(wobbleDuration);

        DestroyEnemy();
    }

    public void BeginWobble()
    {

        if (spawnedJelly == null)
            return;

        JellyWobble wobble = spawnedJelly.GetComponent<JellyWobble>();

        if (wobble != null)
        {
            wobble.StartWobble(2f);
        }
    }

    private void DestroyEnemy()
    {

        if (spawnedJelly != null)
        {
            Destroy(spawnedJelly);
        }

        if (explosionPrefab != null && jellySpawnPoint != null)
        {
            Instantiate(explosionPrefab,jellySpawnPoint.position,Quaternion.identity);
        }
        else
        {
            Debug.LogError("Explosion Prefab or Jelly Spawn Point is missing!");
        }

        Destroy(gameObject);
    }
}