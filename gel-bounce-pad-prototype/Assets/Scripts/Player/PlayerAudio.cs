using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [Header("Voice Lines")]
    [SerializeField] private AudioClip deathGruntClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathGrunt()
    {
        if (audioSource == null || deathGruntClip == null)
            return;

        audioSource.PlayOneShot(deathGruntClip);
    }
}