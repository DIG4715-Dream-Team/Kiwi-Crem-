using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float crouchSpeed = 2f;
    public float normalSpeed = 5f;
    public float boostSpeed = 15f;

    public bool isCrouching;

    private bool boosted = false;

    Rigidbody rb;

    public bool Died { get; private set; }
    public bool CompletedObjectives { get; private set; }
    public int Health { get; private set; }
    public bool GameOver { get; private set; }

    public bool hasHellPearl { get; private set; }
    public int EnPearls { get; private set; }
    public int ExPearls { get; private set; }

    private InputAction crouchAction;

    public bool isInvincible = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Health = 5;
        crouchAction = new InputAction("Crouch", InputActionType.Button, "<Keyboard>/leftShift");
        crouchAction.Enable();
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

        if (Input.GetKey(KeyCode.LeftShift) || Gamepad.current?.buttonWest.isPressed == true)
        {
            rb.velocity = (transform.right * xMove + transform.forward * zMove) * crouchSpeed;
        }
        else
        {
            rb.velocity = (transform.right * xMove + transform.forward * zMove) * speed;
        }
    }

    private void Crouching()
    {
        isCrouching = Input.GetKey(KeyCode.LeftShift) || Gamepad.current?.buttonWest.isPressed == true;
        if (isCrouching)
        {
            speed = crouchSpeed;
        }
        else
        {
            speed = normalSpeed;
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
        if (!isInvincible)
        {
            Health += amount;
            if (Health <= 0)
            {
                Died = true;
                GameOver = true;
            }
        }
    }

    public void SetInvincibility(bool isInvincible)
    {
        this.isInvincible = isInvincible;
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
