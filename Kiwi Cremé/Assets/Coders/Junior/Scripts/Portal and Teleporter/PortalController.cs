using UnityEngine;
//using UnityEngine.InputSystem;

public class PortalController : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    [SerializeField] private GameObject Player;
    [SerializeField] private PlayerController PlayerC;

    public GameObject entryPortal;
    public GameObject exitPortal;

    [SerializeField] private Transform entry;
    [SerializeField] private Transform exit;

    private GameObject buttonManager;
    private ButtonManager ButtonManager;

    float timer = 5f;

    private void Start()
    {
        PlayerC = Player.GetComponent<PlayerController>();
        entry = GameObject.FindGameObjectWithTag("EntryPad").transform;
        exit = GameObject.FindGameObjectWithTag("ExitPad").transform;
        buttonManager = GameObject.FindGameObjectWithTag("ButtonManager");
        ButtonManager = buttonManager.GetComponent<ButtonManager>();
    }
    void Update()
    {
        PortalLocation();
    }

    private void PortalLocation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && PlayerC.EnPearls >= 1 && hit.transform.tag == "EntryPad")
            {
                Vector3 point = hit.point;
                Debug.DrawRay(point, Vector3.up, Color.red, 5f);
                Instantiate(entryPortal, point, Player.transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            }
            else if (Physics.Raycast(ray, out hit) && PlayerC.EnPearls == 0 && hit.transform.tag == "EntryPad")
            {
                ButtonManager.Status("Entry Portal");
                if (timer == 5f)
                {
                    timer = timer -= Time.deltaTime;
                }
                if (timer <= 0.1f)
                {
                    ButtonManager.MiddleText.text = "";
                    timer = 5f;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && PlayerC.ExPearls >= 1 && hit.transform.tag == "ExitPad")
            {
                Vector3 point = hit.point;
                Debug.DrawRay(point, Vector3.up, Color.red, 5f);
                Instantiate(exitPortal, point, Player.transform.rotation * Quaternion.Euler(0f, -exit.transform.localRotation.eulerAngles.y, 0f));
            }
            else if (Physics.Raycast(ray, out hit) && PlayerC.ExPearls == 0 && hit.transform.tag == "ExitPad")
            {
                ButtonManager.Status("Exit Portal");
                if (timer == 5f)
                {
                    timer = timer -= Time.deltaTime;
                }
                if (timer <= 0.1f)
                {
                    ButtonManager.MiddleText.text = "";
                    timer = 5f;
                }
            }
        }
    }
}
