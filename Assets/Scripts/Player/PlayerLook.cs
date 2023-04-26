using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to handle the player's camera rotation
public class PlayerLook : MonoBehaviour
{
    //Player camera reference
    public Camera m_playerCamera;
    //Variable for storing the rotation in the X axis
    private float m_rotationX = 0f;

    //Variable for the sensitivity on X
    [SerializeField]
    private float SensitivityX = 25f;
    //Variable for the sensitivity on Y
    [SerializeField]
    private float SensitivityY = 25f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Obtain mouse/stick input and rotate the camera
    public void ProcessLook(Vector2 input) {
        m_rotationX -= (input.y * Time.deltaTime) * SensitivityY;
        m_rotationX = Mathf.Clamp(m_rotationX, -80f, 80f);
        m_playerCamera.transform.localRotation = Quaternion.Euler(m_rotationX, 0, 0);
        transform.Rotate(Vector3.up * (input.x * Time.deltaTime) * SensitivityX);
    }
}
