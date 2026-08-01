using UnityEngine;

public class GelBlock : MonoBehaviour
{
    public float growthAmount = 0.5f;   // how much bigger each hit makes it
    public float maxScale = 1.5f;         // cap so it doesn't grow forever

    public void Grow()
    {
        Vector3 newScale = transform.localScale + Vector3.one * growthAmount;

        // Clamp so it doesn't scale infinitely
        newScale = Vector3.Min(newScale, Vector3.one * maxScale);

        transform.localScale = newScale;
    }
}
