using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ButtonGotIt : MonoBehaviour
{
    public PlayerMovement pm;
    public GameObject player;
    public MouseMovement mm;
    public GelShooter shooter;
    public GameObject tutorial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OpenTutorial();
    }

    public void OpenTutorial()
    {
        pm.enabled = false;
        mm.enabled = false;
        shooter.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
        pm.enabled = true;
        mm.enabled = true;
        shooter.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
