using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleInvincibility : MonoBehaviour
{
    public PlayerController player;

    public void ToggleInvincible(bool isInvincible)
    {
        player.SetInvincibility(isInvincible);
    }
}
