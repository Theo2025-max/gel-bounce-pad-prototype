using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(MouseMovement))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAudio))]
public class PlayerDeath : MonoBehaviour
{
    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 2f;

    [Header("Invincibility")]
    [SerializeField] private float invincibilityDuration = 2f;

    [Header("Cameras")]
    [SerializeField] private CinemachineCamera playerFollowCamera;

    private const int PlayerCameraPriority = 10;
    private const int DeathCameraPriority = 20;

    private PlayerMovement playerMovement;
    private MouseMovement mouseMovement;
    private CharacterController characterController;
    private PlayerAudio playerAudio;

    private bool isDead;
    private bool isInvincible;

    public bool IsDead => isDead;
    public bool IsInvincible => isInvincible;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        mouseMovement = GetComponent<MouseMovement>();
        characterController = GetComponent<CharacterController>();
        playerAudio = GetComponent<PlayerAudio>();
    }

    public void Die()
    {
        if (isDead || isInvincible)
            return;

        isDead = true;

        Debug.Log("Player Died.");

        if (playerAudio != null)
        {
            playerAudio.PlayDeathGrunt();
        }

        playerMovement.enabled = false;
        mouseMovement.enabled = false;
        characterController.enabled = false;

        ActivateDeathCamera();

        StartCoroutine(RespawnRoutine());
    }

    private void ActivateDeathCamera()
    {
        if (playerFollowCamera != null)
        {
            playerFollowCamera.Priority = 0;
        }

        if (CheckpointManager.Instance != null && CheckpointManager.Instance.CurrentDeathCamera != null)
        {
            CheckpointManager.Instance.CurrentDeathCamera.Priority = DeathCameraPriority;
        }
    }

    private void RestorePlayerCamera()
    {
        if (CheckpointManager.Instance != null && CheckpointManager.Instance.CurrentDeathCamera != null)
        {
            CheckpointManager.Instance.CurrentDeathCamera.Priority = 0;
        }

        if (playerFollowCamera != null)
        {
            playerFollowCamera.Priority = PlayerCameraPriority;
        }
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);

        EnemyAI[] enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);

        foreach (EnemyAI enemy in enemies)
        {
            enemy.ReturnToSpawn();
        }

        if (CheckpointManager.Instance != null && CheckpointManager.Instance.CurrentCheckpoint != null)
        {
            transform.position = CheckpointManager.Instance.CurrentCheckpoint.SpawnPosition;

            Debug.Log("Player Respawned.");
        }
        else
        {
            Debug.LogWarning("No checkpoint found. Respawn cancelled.");
        }

        RestorePlayerCamera();

        characterController.enabled = true;

        // Clears any stored movement after respawning.
        characterController.Move(Vector3.zero);

        playerMovement.enabled = true;
        mouseMovement.enabled = true;

        isDead = false;

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