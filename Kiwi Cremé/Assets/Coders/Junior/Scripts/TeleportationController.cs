using UnityEngine;

public class TeleportationController : MonoBehaviour
{
    private GameObject player;

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
                    player = other.gameObject;
                    GameObject Exit = GameObject.FindGameObjectWithTag("Exit");
                    Vector3 tloc = Exit.transform.position;
                    Vector3 velocity = other.GetComponent<Rigidbody>().velocity;
                    player.transform.position = tloc;
                    player.GetComponent<Rigidbody>().velocity = velocity;
                    Quaternion portalRotation = Exit.transform.localRotation;
                    player.transform.localRotation = Quaternion.LookRotation(portalRotation * Vector3.forward, portalRotation * Vector3.up);
                    player.GetComponent<PlayerController>().hasTeleported = true;
                }
            }
        }
        else if (other != null && gameObject.CompareTag("Exit"))
        {
        }
    }
}
