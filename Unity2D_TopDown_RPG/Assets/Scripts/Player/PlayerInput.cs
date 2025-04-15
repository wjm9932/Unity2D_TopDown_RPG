using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string horizontal = "Horizontal";
    private string vertical = "Vertical";

    public Vector2 moveInput { get; private set; }
    public bool isDodge { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical)).normalized;
        isDodge = Input.GetKeyDown(KeyCode.Space);
    }
}
