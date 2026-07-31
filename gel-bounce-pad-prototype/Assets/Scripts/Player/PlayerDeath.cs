using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(MouseMovement))]
[RequireComponent(typeof(CharacterController))]
public class PlayerDeath : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 2f;

    [Header("Invincibility")]
    [SerializeField] private float invincibilityDuration = 2f;

    private PlayerMovement playerMovement;
    private MouseMovement mouseMovement;
    private CharacterController characterController;

    private bool isDead;
    private bool isInvincible;

    public bool IsDead => isDead;
    public bool IsInvincible => isInvincible;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        mouseMovement = GetComponent<MouseMovement>();
        characterController = GetComponent<CharacterController>();
    }

    public void Die()
    {
        // Prevent multiple deaths or dying while invincible.
        if (isDead || isInvincible)
            return;

        isDead = true;

        Debug.Log("Player Died.");

        playerMovement.enabled = false;
        mouseMovement.enabled = false;
        characterController.enabled = false;

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        EnemyAI[] enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);

        foreach (EnemyAI enemy in enemies)
        {
            enemy.ReturnToSpawn();
        }

        // Respawn at the latest checkpoint.
        if (CheckpointManager.Instance != null &&
            CheckpointManager.Instance.CurrentCheckpoint != null)
        {
            transform.position = CheckpointManager.Instance.CurrentCheckpoint.SpawnPosition;

            Debug.Log("Player Respawned.");
        }
        else
        {
            Debug.LogWarning("No checkpoint found. Respawn cancelled.");
        }

        // Re-enable the CharacterController after teleporting.
        characterController.enabled = true;

        // Re-enable player controls.
        playerMovement.enabled = true;
        mouseMovement.enabled = true;

        isDead = false;

        // Give the player a short grace period.
        StartCoroutine(InvincibilityRoutine());
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        Debug.Log("Player is temporarily invincible.");

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;

        Debug.Log("Player is vulnerable again.");
    }
}