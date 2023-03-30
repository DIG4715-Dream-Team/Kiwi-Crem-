using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Rigidbody rb;

    public bool Died { get; private set; }
    public bool CompletedObjectives { get; private set; }
    public int Health { get; private set; }
    public bool GameOver { get; private set; }

    public bool HasPearls { get; private set; }
    public int EnPearls { get; private set; }
    public int ExPearls { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerMovement();
        Crouching();
    }

    private void PlayerMovement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        rb.velocity = (transform.right * xMove + transform.forward * zMove) * speed;
    }

    private void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 3f;
            Debug.Log("ShiftKey is Down");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
            Debug.Log("ShiftKey is Up");
        }
    }
}
