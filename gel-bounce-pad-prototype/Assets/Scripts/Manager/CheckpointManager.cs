using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    public Checkpoint CurrentCheckpoint { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetCheckpoint(Checkpoint checkpoint)
    {
        CurrentCheckpoint = checkpoint;

        Debug.Log($"Checkpoint Saved: {checkpoint.SpawnPosition}");
    }
}