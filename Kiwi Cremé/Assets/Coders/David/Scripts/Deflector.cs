using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
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
            Instantiate(Sheild);
            lastTime = Time.time;
        }

    }

    public IEnumerator Wait(float delayInSecs)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            yield return new WaitForSeconds(2);
            Destroy(Sheild);
        }
    }
}
