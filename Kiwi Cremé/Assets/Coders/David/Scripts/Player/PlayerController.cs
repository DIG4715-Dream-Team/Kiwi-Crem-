using UnityEngine;
//using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float normalSpeed = 5f;
    public float boostSpeed = 15f;

    private bool boosted = false;

    Rigidbody rb;

    public bool Died { get; private set; }
    public bool CompletedObjectives { get; private set; }
    public int Health { get; private set; }
    public bool GameOver { get; private set; }

    public bool hasHellPearl { get; private set; }
    public int EnPearls { get; private set; }
    public int ExPearls { get; private set; }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Health = 5;
    }

    private void Update()
    {
        PlayerMovement();
        Crouching();
        SpeedBoost();
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
            transform.gameObject.tag = "HiddenPlayer";
            speed = 3f;
            Debug.Log("ShiftKey is Down");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            transform.gameObject.tag = "Player";
            speed = 5f;
            Debug.Log("ShiftKey is Up");
        }
    }

    private void SpeedBoost()
    {
        if (boosted)
        {
            speed = boostSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BoostObject"))
        {
            boosted = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("BoostObject"))
        {
            boosted = false;
        }
    }

    public void HealthManagement(int amount)
    {
        Health = Health + amount;

        if (Health <= 0)
        {
            Died = true;
        }
    }

    public void UpdatePearl(string level)
    {
        if (level == "Hell")
        {
            hasHellPearl = true;
        }
    }

    public void UpdateObjective(string level)
    {
        if (level == "Hell")
        {
            CompletedObjectives = true;
        }
    }

    public void UpdatePearlAmount(string type, int change)
    {
        if (type == "Entry")
        {
            EnPearls = EnPearls + change;
        }

        if (type == "Exit")
        {
            ExPearls = ExPearls + change;
        }
    }
}
