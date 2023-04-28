using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleInvincibility : MonoBehaviour
{
    public PlayerController player;
    public Toggle toggle;

    private void Start()
    {
        toggle.isOn = player.isInvincible;
        toggle.onValueChanged.AddListener(SetInvincible);
    }

    public void SetInvincible(bool isInvincible)
    {
        player.SetInvincibility(isInvincible);
    }
}
