using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    [SerializeField] private Transform spawnPoint;

    [Header("Death Camera")]
    [SerializeField] private CinemachineCamera deathCamera;

    [Header("Activation Feedback")]
    [SerializeField] private Transform vfxSpawnPoint;
    [SerializeField] private GameObject activationEffectPrefab;
    [SerializeField] private AudioClip activationSound;
    [SerializeField] private AudioSource audioSource;

    private bool isActivated;
    public Vector3 SpawnPosition
    {
        get
        {
            if (spawnPoint != null)
                return spawnPoint.position;

            return transform.position;
        }
    }
    public CinemachineCamera DeathCamera => deathCamera;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated)
            return;

        if (!other.CompareTag("Player"))
            return;

        isActivated = true;

        ActivateCheckpoint();

        CheckpointManager.Instance?.SetCheckpoint(this);

        Debug.Log("Checkpoint Activated.");
    }

    private void ActivateCheckpoint()
    {
        // Spawn the checkpoint VFX.
        if (activationEffectPrefab != null)
        {
            Vector3 spawnPosition = transform.position;

            if (vfxSpawnPoint != null)
                spawnPosition = vfxSpawnPoint.position;

            GameObject effect = Instantiate(activationEffectPrefab,spawnPosition,Quaternion.identity);

            Destroy(effect, 5f);
        }

        // Play the checkpoint sound.
        if (audioSource != null && activationSound != null)
        {
            audioSource.PlayOneShot(activationSound);
        }
    }
}