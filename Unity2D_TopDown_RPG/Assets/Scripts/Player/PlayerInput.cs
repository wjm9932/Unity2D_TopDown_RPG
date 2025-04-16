using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private string horizontal = "Horizontal";
    private string vertical = "Vertical";
    private readonly float bufferTime = 0.2f;
    public Vector2 moveInput { get; private set; }
    public float dodgeBufferTime { get; private set; }
    public float autoAttackBufferTime { get; private set; }
    public float dashAttackBufferTime { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dodgeBufferTime -= Time.deltaTime;
        autoAttackBufferTime -= Time.deltaTime;
        dashAttackBufferTime -= Time.deltaTime;

        moveInput = new Vector2(Input.GetAxisRaw(horizontal), Input.GetAxisRaw(vertical)).normalized;

        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            dodgeBufferTime = bufferTime;
        }
        if(Input.GetKeyDown(KeyCode.Q) == true)
        {
            autoAttackBufferTime = bufferTime;
        }
        if(Input.GetKeyDown(KeyCode.W) == true)
        {
            dashAttackBufferTime = bufferTime;
        }
    }
}
