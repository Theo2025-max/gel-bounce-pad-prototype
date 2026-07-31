using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

        if (playerDeath == null)
            return;

        if (playerDeath.IsInvincible)
            return;

        playerDeath.Die();
    }
}