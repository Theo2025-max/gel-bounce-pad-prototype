using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponAudio : MonoBehaviour
{
    [Header("Weapon Sounds")]
    [SerializeField] private AudioClip shootClip;

    [Header("Pitch Variation")]
    [SerializeField] private float minPitch = 0.97f;
    [SerializeField] private float maxPitch = 1.03f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShoot()
    {
        if (shootClip == null)
            return;

        audioSource.pitch = Random.Range(minPitch, maxPitch);

        audioSource.PlayOneShot(shootClip);
    }
}