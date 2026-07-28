using UnityEngine;

public class GelShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject jellyPrefab;
    [SerializeField] private ParticleSystem jellyMuzzleFlash;

    [Header("Raycast Settings")]
    [SerializeField] private float shootDistance = Mathf.Infinity;
    [SerializeField] private LayerMask gelSurfaceLayer;

    [Header("Animation References")]
    [SerializeField] private Animator Animator;

    private GameObject currentJelly;
    PlayerControls controls;

    const string SHOOT_STRING = "Shoot";

    private void Awake()
    {
        controls = new PlayerControls();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
        controls.Player.Shoot.performed += ctx =>
        {
            Shoot();
        };
    }

    private void Update()
    {
        
    }

    private void Shoot()
    {
        jellyMuzzleFlash.Play();

        Animator.Play(SHOOT_STRING, 0, 0f);

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

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}