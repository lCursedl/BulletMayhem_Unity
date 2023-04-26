using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Class to execute interactions by the player
public class PlayerInteract : MonoBehaviour
{
    //Reference to player camera
    private Camera m_playerCamera;
    //Variable to set the valid distance for an interaction
    //prompt to appear on screen
    [SerializeField]
    private float InteractDistance;
    //Variable to set the layer the raycast and interactables will be
    [SerializeField]
    private LayerMask m_layerMask;
    //Variable to hold a reference of the player's UI script
    private PlayerUI m_playerUI;
    //Variable to hold a reference of the Input Manager
    private InputManager m_inputManager;

    void Start() {
        //Obtain all necesary components for the functionality of the script
        m_playerCamera = GetComponent<PlayerLook>().m_playerCamera;
        m_playerUI = GetComponent<PlayerUI>();
        m_inputManager = GetComponent<InputManager>();
    }

    void Update() {
        //Set the text to blank in case the player is not facing an interactable
        m_playerUI.UpdateText(string.Empty);
        //By raycast detect if player is facing an interactable and if so, obtain its prompt message
        //If the player presses the interact button, call the interact method from the game object
        Ray detetionRay = new(m_playerCamera.transform.position, m_playerCamera.transform.forward);
        if (Physics.Raycast(detetionRay, out RaycastHit hitInfo, InteractDistance)) {
            if (hitInfo.collider.GetComponent<Interactable>() != null) {
                Interactable tempInteract = hitInfo.collider.GetComponent<Interactable>();
                m_playerUI.UpdateText(tempInteract.PromptMessage);
                if (m_inputManager.m_defaultActions.Interact.triggered) {
                    tempInteract.BaseInteract();
                }
            }
        }
    }
}
