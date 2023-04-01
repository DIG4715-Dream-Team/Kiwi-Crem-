using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotate : MonoBehaviour
{
    public GameObject playerParent;

    public float rotationSpeed = 10f;

    private bool shouldRotate = false;

    void Update()
    {
        if (shouldRotate)
        {
            Vector3 targetDirection = transform.parent.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(-targetDirection, Vector3.up);

            playerParent.transform.rotation = Quaternion.Lerp(playerParent.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(playerParent.transform.rotation, targetRotation) < 0.1f)
            {
                shouldRotate = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null && gameObject.CompareTag("Entry"))
        {
            shouldRotate = true;
        }
    }
}
