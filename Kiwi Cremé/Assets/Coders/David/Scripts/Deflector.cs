using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
    [SerializeField]
    private Camera Cam;
    [SerializeField]
    private GameObject Player;
    
    public GameObject Sheild;
    
    float lastTime;

    void Start()
    {
        lastTime = -5.0f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && (Time.time - lastTime > 5.0f))
        {
            Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 point = hit.point;
                Debug.DrawRay(point, Vector3.up, Color.red, 5f);
                Instantiate(Sheild, point, Player.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
            }
            lastTime = Time.time;
        }
    }
}
