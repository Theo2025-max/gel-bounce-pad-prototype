using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GelShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject jellyPrefab;
    [SerializeField] private ParticleSystem jellyMuzzleFlash;
    [SerializeField] private WeaponAudio weaponAudio;
    [SerializeField] private GameObject explosionPrefab;

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

        // Automatically find the WeaponAudio component if it wasn't assigned.
        if (weaponAudio == null)
        {
            weaponAudio = GetComponent<WeaponAudio>();
        }

        controls.Player.Shoot.performed += ctx => Shoot();
    }

    private void Shoot()
    {
        // Play the weapon firing sound.
        weaponAudio?.PlayShoot();

        // Play visual effects.
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

            if (existingGel != null)
            {
                // Already a gel block—grow it instead of spawning a new one.
                existingGel.Grow();
            }
            else
            {
                if (jelly4 != null)
                {
                    Destroy(jelly4);
                    if (explosionPrefab != null && hit.point != null)
                    {
                        Instantiate(explosionPrefab, jelly4.transform.position, Quaternion.identity);
                    }
                }
                if (jelly3 != null) jelly4 = jelly3;
                if (jelly2 != null) jelly3 = jelly2;
                if (jelly1 != null) jelly2 = jelly1;

                jelly1 = Instantiate(jellyPrefab,hit.point,Quaternion.FromToRotation(Vector3.up, Vector3.up));
                StartCoroutine(LifetimeRoutine(jelly1, hit.point));
            }

            Debug.DrawLine(ray.origin, hit.point, Color.green, 2f);
        }
        else
        {
            Debug.Log("Nothing was hit.");

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);
        }
    }

    private IEnumerator LifetimeRoutine(GameObject jelly, Vector3 jellySpawnPoint)
    {
        yield return new WaitForSeconds(10f);
        if (jelly != null) BeginWobble(jelly);

        yield return new WaitForSeconds(5f);
        if (jelly != null) Destroy(jelly);
        if (jelly != null && explosionPrefab != null && jellySpawnPoint != null)
        {
            Instantiate(explosionPrefab, jellySpawnPoint, Quaternion.identity);
        }
    }

    public void BeginWobble(GameObject spawnedJelly)
    {

        if (spawnedJelly == null)
            return;

        JellyWobble wobble = spawnedJelly.GetComponent<JellyWobble>();

        if (wobble != null)
        {
            wobble.StartWobble(5f);
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