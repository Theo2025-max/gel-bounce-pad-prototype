using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyAudio enemyAudio;

    private bool hasDetectedPlayer = false;

    private void Reset()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
        enemyAudio = GetComponentInParent<EnemyAudio>();
    }

    private void Awake()
    {
        if (enemyAI == null)
            enemyAI = GetComponentInParent<EnemyAI>();

        if (enemyAudio == null)
            enemyAudio = GetComponentInParent<EnemyAudio>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered detection radius.");

        if (hasDetectedPlayer)
            return;

        if (!other.CompareTag("Player"))
            return;

        hasDetectedPlayer = true;

        enemyAI.StartChasing();

        if (EnemyDialogueDirector.Instance != null &&
            EnemyDialogueDirector.Instance.CanPlayDialogue())
        {
            enemyAudio.PlayTargetConfirmed();
        }
    }

    public void ResetDetection()
    {
        hasDetectedPlayer = false;
    }
}