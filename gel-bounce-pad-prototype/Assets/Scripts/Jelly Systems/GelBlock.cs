using UnityEngine;

public class GelBlock : MonoBehaviour
{
    public float growthAmount = 0.5f;   // how much bigger each hit makes it
    public float maxScale = 1.5f;         // cap so it doesn't grow forever
    public string size2Layer = "GelPad2";
    public string size3Layer = "GelPad3";

    private float baseScale;
    private int growthStage = 0;

    void Awake()
    {
        baseScale = transform.localScale.x; // assumes uniform scale (x=y=z)
    }

    public void Grow()
    {
        Vector3 newScale = transform.localScale + Vector3.one * growthAmount;
        // Clamp so it doesn't scale infinitely
        newScale = Vector3.Min(newScale, Vector3.one * maxScale);
        transform.localScale = newScale;
        growthStage++;
        UpdateLayer();
    }

    void UpdateLayer()
    {
        switch (growthStage)
        {
            case 1:
                gameObject.layer = LayerMask.NameToLayer(size2Layer);
                break;
            case 2:
                gameObject.layer = LayerMask.NameToLayer(size3Layer);
                break;
                default:
                gameObject.layer = LayerMask.NameToLayer(size3Layer);
                break;
        }
    }
}
