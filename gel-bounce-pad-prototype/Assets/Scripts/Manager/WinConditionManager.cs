using System.Collections.Generic;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    public static WinConditionManager Instance { get; private set; }

    [SerializeField] private List<GameObject> activeEnemies = new List<GameObject>();
    [SerializeField] private GameObject winScreen;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Automatically populate the list instead of dragging 8 references manually
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("LastEnemy");
        activeEnemies.AddRange(enemies);
    }

    public void RegisterEnemyDeath(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (activeEnemies.Count == 0)
        {
            winScreen.SetActive(true);
        }
    }
}
