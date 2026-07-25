using UnityEngine;
using StarterAssets;

public class JellyBouncePad : MonoBehaviour
{
    [Header("Bounce Settings")]
    [SerializeField]
    private float bounceHeight = 8f;

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();

        if (player == null)
            return;

        player.Launch(bounceHeight);

        Debug.Log("Player bounced!");
    }
}