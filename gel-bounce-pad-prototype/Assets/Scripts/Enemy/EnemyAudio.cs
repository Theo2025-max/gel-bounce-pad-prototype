using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    [Header("Voice Lines")]
    [SerializeField] private AudioClip targetConfirmedClip;
    [SerializeField] private AudioClip trapGruntClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayTargetConfirmed()
    {
        PlayVoiceLine(targetConfirmedClip);
    }

    public void PlayTrapGrunt()
    {
        PlayVoiceLine(trapGruntClip);
    }

    private void PlayVoiceLine(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        audioSource.PlayOneShot(clip);
    }
}