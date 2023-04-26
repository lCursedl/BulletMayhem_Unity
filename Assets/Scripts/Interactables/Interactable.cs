using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to assig funtionality of interactable objects
public abstract class Interactable : MonoBehaviour
{
    //Flag to determine if an Interaction Event should be added
    //or not to this game object
    public bool UseEvents;
    //String for holding the game object's prompt message
    public string PromptMessage;

    //Public method to call for a game object's interaction
    public void BaseInteract() {
        if (UseEvents) {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interact();
    }

    //Virtual method to implement in every interactable game object
    protected virtual void Interact() {}
}
