using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private Camera Cam;
    [SerializeField]
    private GameObject Player;

    public GameObject entryPortal;
    public GameObject exitPortal;

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
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "EntryPad")
            {
                Vector3 point = hit.point;
                Debug.DrawRay(point, Vector3.up, Color.red, 5f);
                Instantiate(entryPortal, point, Player.transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "ExitPad")
            {
                Vector3 point = hit.point;
                Debug.DrawRay(point, Vector3.up, Color.red, 5f);
                Instantiate(exitPortal, point, Player.transform.rotation * Quaternion.Euler(0f, 90f, 0f));
            }
        }
    }
}
