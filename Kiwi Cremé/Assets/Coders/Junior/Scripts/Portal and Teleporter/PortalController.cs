using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalController : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    [SerializeField] private GameObject Player;
    [SerializeField] private PlayerController PlayerC;

    public GameObject entryPortal;
    public GameObject exitPortal;

    public bool creatingEntry { get; private set; }
    public bool creatingExit { get; private set; }

    private GameObject gameManager;
    private GameManager GameManager;

    private GameObject[] EntryPad;
    private GameObject[] ExitPad;
    private GameObject EntryPortal;
    private GameObject ExitPortal;

    private int entryIteration = 1;
    private int exitIteration = 1;

    float timer = 5f;

    private InputAction entryAction;
    private InputAction exitAction;

    private void Start()
    {
        PlayerC = Player.GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("ButtonManager");
        GameManager = gameManager.GetComponent<GameManager>();
        EntryPad = GameObject.FindGameObjectsWithTag("EntryPad");
        ExitPad = GameObject.FindGameObjectsWithTag("ExitPad");
    }

    void Update()
    {
        PortalLocation();
    }

    private void Awake()
    {
        entryAction = new InputAction("Entry", InputActionType.Button, "<Gamepad>/leftTrigger");
        exitAction = new InputAction("Exit", InputActionType.Button, "<Gamepad>/rightTrigger");

        entryAction.Enable();
        exitAction.Enable();
    }

    private void PortalLocation()
    {
        if ((Input.GetMouseButtonDown(0) || (Gamepad.current != null && Gamepad.current.leftTrigger.wasPressedThisFrame)) && PlayerC.EnPearls > 0)
        {
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && PlayerC.EnPearls >= 1 && hit.transform.tag == "EntryPad")
            {
                foreach (GameObject entrypad in EntryPad)
                {
                    entrypad.SetActive(true);
                }
                for (int i = 0; i < entryIteration; i++)
                {
                    EntryPortal = GameObject.FindGameObjectWithTag("Entry");
                    Destroy(EntryPortal);
                }
                Vector3 point = hit.point;
                Debug.DrawRay(point, Vector3.up, Color.red, 5f);
                Instantiate(entryPortal, hit.transform.position, Quaternion.Euler(0f, hit.transform.localEulerAngles.y, 0f));
                hit.transform.gameObject.SetActive(false);
                creatingEntry = false;
                PlayerC.UpdatePearlAmount("Entry", -1);
            }
            else if (Physics.Raycast(ray, out hit) && PlayerC.EnPearls == 0 && hit.transform.tag == "EntryPad")
            {
                GameManager.Status("Entry Portal");
            }
        }

        if ((Input.GetMouseButtonDown(1) || (Gamepad.current != null && Gamepad.current.rightTrigger.wasPressedThisFrame)) && PlayerC.ExPearls > 0)
        {
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && PlayerC.ExPearls >= 1 && hit.transform.tag == "ExitPad")
            {
                foreach (GameObject exitpad in ExitPad)
                {
                    exitpad.SetActive(true);
                }
                for (int i = 0; i < exitIteration; i++)
                {
                    ExitPortal = GameObject.FindGameObjectWithTag("Exit");
                    Destroy(ExitPortal);
                }
                Vector3 point = hit.point;
                Debug.DrawRay(point, Vector3.up, Color.red, 5f);
                Instantiate(exitPortal, hit.transform.position, Quaternion.Euler(0f, hit.transform.localEulerAngles.y, 0f));
                hit.transform.gameObject.SetActive(false);
                creatingExit = false;
                PlayerC.UpdatePearlAmount("Exit", -1);
            }
            else if (Physics.Raycast(ray, out hit) && PlayerC.ExPearls == 0 && hit.transform.tag == "ExitPad")
            {
                GameManager.Status("Exit Portal");
            }
        }
    }
}
