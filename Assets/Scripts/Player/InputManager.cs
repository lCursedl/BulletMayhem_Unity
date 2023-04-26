using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Script to handle the inputs for the player and communicate with various
//systems to handle them
public class InputManager : MonoBehaviour
{
    //Reference to PlayerInput script
    private PlayerInput m_playerInput;
    //Reference to actions the player can do with the input system
    [HideInInspector]
    public PlayerInput.DefaultActions m_defaultActions;
    //Reference to input changes for the player
    private PlayerMovement m_movement;
    private PlayerLook m_look;
    private PlayerShoot m_shoot;

    //Obtain all necesary components and connect script methods with input events
    private void Awake() {
        m_playerInput = new PlayerInput();
        m_defaultActions = m_playerInput.Default;
        m_movement = GetComponent<PlayerMovement>();
        m_defaultActions.Jump.performed += ctx => m_movement.Jump();
        m_look = GetComponent<PlayerLook>();
        m_shoot = GetComponent<PlayerShoot>();
        m_defaultActions.Shoot.canceled += ctx => m_shoot.ShootWeapon(false);
        m_defaultActions.Shoot.performed += ctx => m_shoot.ShootWeapon(true);
        m_defaultActions.Reload.performed += ctx => m_shoot.ReloadWeapon();
    }

    private void LateUpdate() {
        m_look.ProcessLook(m_defaultActions.Look.ReadValue<Vector2>());
    }

    private void FixedUpdate() {
        m_movement.ProcessMovement(m_defaultActions.Movement.ReadValue<Vector2>());
    }

    //Enable inputs
    private void OnEnable() {
        m_defaultActions.Enable();
    }

    //Disable inputs
    private void OnDisable() {
        m_defaultActions.Disable();
    }
}
