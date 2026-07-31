using UnityEngine;
public class EnemyDialogueDirector : MonoBehaviour
{
    public static EnemyDialogueDirector Instance { get; private set; }

    [Header("Dialogue Settings")]
    [SerializeField] private float dialogueCooldown = 4f;

    private float nextAllowedDialogueTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool CanPlayDialogue()
    {
        if (Time.time < nextAllowedDialogueTime)
            return false;

        nextAllowedDialogueTime = Time.time + dialogueCooldown;

        return true;
    }
}