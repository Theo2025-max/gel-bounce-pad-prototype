using UnityEngine;

public class GelShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject jellyPrefab;
    [SerializeField] private ParticleSystem jellyMuzzleFlash;

    [Header("Raycast Settings")]
    [SerializeField] private float shootDistance = Mathf.Infinity;
    [SerializeField] private LayerMask shootLayerMask;

    [Header("Animation References")]
    [SerializeField] private Animator animator;

    private GameObject jelly1, jelly2, jelly3, jelly4;

    private PlayerControls controls;

    private const string SHOOT_STRING = "Shoot";

    private void Awake()
    {
        controls = new PlayerControls();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        controls.Player.Shoot.performed += ctx => Shoot();
    }

    private void Shoot()
    {
        jellyMuzzleFlash.Play();
        animator.Play(SHOOT_STRING, 0, 0f);

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, shootDistance, shootLayerMask))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            if (hit.collider.TryGetComponent<IGelTarget>(out IGelTarget gelTarget))
            {
                gelTarget.Trap();

                Debug.DrawLine(ray.origin, hit.point, Color.cyan, 2f);

                return;
            }
            
            GelBlock existingGel = hit.collider.GetComponent<GelBlock>();

<<<<<<< Updated upstream
            GelBlock existingGel = hit.collider.GetComponent<GelBlock>();

            if (existingGel != null)
            {
                // Already a gel block grow it instead of spawning a new one
                existingGel.Grow();
            }
            else
            {
                if (jelly4 != null)
                {
                    Destroy(jelly4);
                }
                if (jelly3 != null) jelly4 = jelly3;
                if (jelly2 != null) jelly3 = jelly2;
                if (jelly1 != null) jelly2 = jelly1;
                jelly1 = Instantiate(jellyPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, Vector3.up));
            }
=======
<<<<<<< HEAD
            if (existingGel != null)
            {
                // Already a gel block - grow it instead of spawning a new one
                existingGel.Grow();
            }
            else
            {
                if (jelly4 != null)
                {
                    Destroy(jelly4);
                }

                if (jelly3 != null) jelly4 = jelly3;
                if (jelly2 != null) jelly3 = jelly2;
                if (jelly1 != null) jelly2 = jelly1;
                jelly1 = Instantiate(jellyPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, Vector3.up));
            }
=======
            GelBlock existingGel = hit.collider.GetComponent<GelBlock>();

            if (existingGel != null)
            {
                // Already a gel block grow it instead of spawning a new one
                existingGel.Grow();
            }
            else
            {
                if (jelly4 != null)
                {
                    Destroy(jelly4);
                }
                if (jelly3 != null) jelly4 = jelly3;
                if (jelly2 != null) jelly3 = jelly2;
                if (jelly1 != null) jelly2 = jelly1;
                jelly1 = Instantiate(jellyPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, Vector3.up));
            }
>>>>>>> bcd5196e0fb0a3229a0e169ac04c39d75df0dfa1
>>>>>>> Stashed changes

            Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);
        }
        else
        {
            Debug.Log("Nothing was hit.");

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
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