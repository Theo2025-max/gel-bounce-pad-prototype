using System.Collections;
using UnityEngine;

public class JellyWobble : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float wobbleDuration = 2f;

    [Header("Rotation")]
    [SerializeField] private float wobbleSpeed = 16f;
    [SerializeField] private float wobbleAngle = 8f;

    [Header("Squash & Stretch")]
    [SerializeField] private float squashAmount = 0.08f;
    [SerializeField] private float stretchAmount = 0.12f;

    private Quaternion startRotation;
    private Vector3 startScale;

    private void Awake()
    {
        startRotation = transform.localRotation;
        startScale = transform.localScale;
    }

    public void StartWobble()
    {
        StartCoroutine(WobbleRoutine());
    }

    private IEnumerator WobbleRoutine()
    {
        float timer = 0f;

        while (timer < wobbleDuration)
        {
            timer += Time.deltaTime;

            float wave = Mathf.Sin(timer * wobbleSpeed);

            // Rotation
            transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, wave * wobbleAngle);

            // Squash & Stretch
            float xScale = 1f + (wave * stretchAmount);
            float yScale = 1f - (wave * squashAmount);
            float zScale = 1f + (wave * stretchAmount);

            transform.localScale = Vector3.Scale(startScale, new Vector3(xScale, yScale, zScale));

            yield return null;
        }

        transform.localRotation = startRotation;
        transform.localScale = startScale;
    }
}