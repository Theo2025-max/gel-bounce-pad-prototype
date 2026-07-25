using UnityEngine;
using StarterAssets;

public class GelShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject jellyPrefab;

    [Header("Raycast Settings")]
    [SerializeField] private float shootDistance = Mathf.Infinity;
    [SerializeField] private LayerMask gelSurfaceLayer;

    private GameObject currentJelly;
    private StarterAssetsInputs inputs;

    private void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (!inputs.shoot)
        {
            return;
        }

        Shoot();

        // Prevent multiple shots from one click.
        inputs.shoot = false;
    }

    private void Shoot()
    {
        Ray ray = new Ray(playerCamera.transform.position,playerCamera.transform.forward);

        if (Physics.Raycast(ray,out RaycastHit hit,shootDistance,gelSurfaceLayer))
        {
            Debug.Log($"Valid Gel Surface Hit: {hit.collider.name}\n" +$"Point: {hit.point}\n" +$"Normal: {hit.normal}");

            if (currentJelly != null)
            {
                Destroy(currentJelly);
            }

            Quaternion rotation = Quaternion.FromToRotation(Vector3.up,hit.normal);

            currentJelly = Instantiate(jellyPrefab,hit.point,rotation);

            Debug.DrawLine(ray.origin,hit.point,Color.green, 2f);
        }
        else
        {
            Debug.Log("No valid Gel Surface was hit.");

            Debug.DrawRay(ray.origin,ray.direction * 100f,Color.red, 2f);
        }
    }
}