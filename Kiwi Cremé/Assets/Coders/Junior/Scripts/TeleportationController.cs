using UnityEngine;

public class TeleportationController : MonoBehaviour
{
    private GameObject Exit;
    private Vector3 tloc;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && gameObject.CompareTag("Entry"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (GameObject.FindGameObjectWithTag("Exit") == false)
                {
                    return;
                }
                else
                {
                    Exit = GameObject.FindGameObjectWithTag("Exit");
                    tloc = Exit.transform.position;
                    other.transform.position = tloc;
                }
            }
        }
        else if (other != null && gameObject.CompareTag("Exit"))
        {

        }
    }
}
