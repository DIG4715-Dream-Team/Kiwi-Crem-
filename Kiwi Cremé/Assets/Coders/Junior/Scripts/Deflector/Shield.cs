using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Shield : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cam;
    [SerializeField] private float spawnDistance = 3f;
    [SerializeField] private GameObject Deflector;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private bool canspawn = true;

    private InputAction spawnShieldAction;

    void Start()
    {
        spawnShieldAction = new InputAction("Shield", InputActionType.Button, "<Gamepad>/buttonSouth");
        spawnShieldAction.Enable();
        spawnShieldAction.performed += _ => SpawnShield();
    }
    void Update()
    {
        if (canspawn == true && (Gamepad.current != null && Gamepad.current.enabled && spawnShieldAction.triggered ||
        Input.GetKeyDown(KeyCode.E)))
        {
            SpawnShield();
        }
    }

    void Awake()
    {
        canspawn = true;
    }

    void SpawnShield()
    {
        Instantiate(Deflector, player.position + player.forward * spawnDistance, cam.rotation);
        canspawn = false;

        if (Gamepad.current != null && Gamepad.current.enabled)
        {
            spawnShieldAction.Disable();
            StartCoroutine(SpawnCooldownWithAction());
        }
        else
        {
            StartCoroutine(SpawnCooldown());
        }
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canspawn = true;
    }

    IEnumerator SpawnCooldownWithAction()
    {
        yield return new WaitForSeconds(cooldown);
        canspawn = true;
        spawnShieldAction.Enable();
    }
}
