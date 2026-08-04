using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hazard : MonoBehaviour
{
    private void Reset()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

        if (playerDeath != null)
        {
            playerDeath.Die();
        }
    }
}