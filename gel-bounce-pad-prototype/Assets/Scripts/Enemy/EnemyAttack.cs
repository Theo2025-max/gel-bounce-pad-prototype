using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyTrap trap;

    private void Awake()
    {
        trap = GetComponentInParent<EnemyTrap>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || trap.isTrapped)
            return;

        PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

        if (playerDeath == null)
            return;

        if (playerDeath.IsInvincible)
            return;

        playerDeath.Die();
    }
}