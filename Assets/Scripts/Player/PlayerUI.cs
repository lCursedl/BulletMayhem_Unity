using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Script to set the text for the interaction UI
public class PlayerUI : MonoBehaviour
{
    //Refrence to the UI text for prompts
    [SerializeField]
    private TextMeshProUGUI m_promptText;

    //Updates the prompt text in the UI with the designated one
    public void UpdateText(string promptMessage) {
        m_promptText.text = promptMessage;
    }
}
