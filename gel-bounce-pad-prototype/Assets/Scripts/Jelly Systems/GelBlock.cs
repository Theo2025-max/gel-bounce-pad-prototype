using UnityEngine;

public class GelBlock : MonoBehaviour
{
    public float growthAmount = 0.5f;   // how much bigger each hit makes it
<<<<<<< Updated upstream
    public float maxScale = 1.5f;         // cap so it doesn't grow forever
=======
<<<<<<< HEAD
    public float maxScale = 2.5f;         // cap so it doesn't grow forever
=======
    public float maxScale = 1.5f;         // cap so it doesn't grow forever
>>>>>>> bcd5196e0fb0a3229a0e169ac04c39d75df0dfa1
>>>>>>> Stashed changes

    public void Grow()
    {
        Vector3 newScale = transform.localScale + Vector3.one * growthAmount;

        // Clamp so it doesn't scale infinitely
        newScale = Vector3.Min(newScale, Vector3.one * maxScale);

        transform.localScale = newScale;
    }
}
