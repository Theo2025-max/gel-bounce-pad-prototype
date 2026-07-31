using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;

    public Vector3 SpawnPosition => transform.position;

    private void OnTriggerEnter(Collider other)
    {
        if (isActivated)
            return;

        if (!other.CompareTag("Player"))
            return;

        isActivated = true;

        CheckpointManager.Instance?.SetCheckpoint(this);

        Debug.Log("Checkpoint Activated.");
    }
}