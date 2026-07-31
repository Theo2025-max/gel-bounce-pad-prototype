using UnityEngine;
public class EnemyStateMachine : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chasing,
        Trapped,
        Destroying
    }

    [Header("Debug")]
    [SerializeField]
    private EnemyState currentState = EnemyState.Idle;

    public EnemyState CurrentState => currentState;
    public void SetState(EnemyState newState)
    {
        if (currentState == newState)
            return;

        Debug.Log($"{name}: {currentState} ? {newState}");

        currentState = newState;
    }
}