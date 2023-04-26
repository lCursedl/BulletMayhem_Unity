using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle the shoot input and communicate with equipped weapon
public class PlayerShoot : MonoBehaviour
{
    //Actions to communicate with weapons
    public static Action m_shootAction;
    public static Action m_reloadAction;
    //Flag to determine if player is pressing the shoot input
    private bool m_shooting = false;

    //Constantly check if player is trying to shoot and invoke the weapons method for shooting
    private void Update() {
        if (m_shooting)
        {
            m_shootAction?.Invoke();
        }
    }

    //Method to change the shooting flag
    public void ShootWeapon(bool value) {
        m_shooting = value;
    }
    //Method to changee the reloading flag
    public void ReloadWeapon()
    {
        m_reloadAction?.Invoke();
    }
}
