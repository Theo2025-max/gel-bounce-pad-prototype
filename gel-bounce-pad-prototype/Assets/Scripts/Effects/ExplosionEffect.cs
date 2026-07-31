using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
public class ExplosionEffect : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float destroyDelay = 3f;

    private ParticleSystem particleSystemComponent;
    private AudioSource audioSource;

    private void Awake()
    {
        particleSystemComponent = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Play the particle effect.
        particleSystemComponent.Play();

        // Play the explosion sound.
        audioSource.Play();

        // Destroy this GameObject after everything has finished.
        Destroy(gameObject, destroyDelay);
    }
}