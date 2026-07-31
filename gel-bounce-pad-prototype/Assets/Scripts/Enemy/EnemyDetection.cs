using UnityEngine;
public class EnemyDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyAI enemyAI;

    private bool hasDetectedPlayer = false;

    private void Reset()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasDetectedPlayer)
            return;

        if (!other.CompareTag("Player"))
            return;

        hasDetectedPlayer = true;

        enemyAI.StartChasing();
    }
}