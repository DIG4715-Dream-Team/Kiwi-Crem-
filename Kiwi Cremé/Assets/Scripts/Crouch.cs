using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public CharacterController PlayerHeight;
    public float normalHeight, crouchHeight;

        
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerHeight.height = crouchHeight;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            PlayerHeight.height = normalHeight;
        }
    }
}
