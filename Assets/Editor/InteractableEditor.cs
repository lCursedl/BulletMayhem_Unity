using UnityEditor;
//Editor class to assign a game object interactions by events
[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    //Method to assign or revert the interactions by events on the inspector for the gameobject
    public override void OnInspectorGUI() {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventOnlyInteractable)) {
            interactable.PromptMessage = EditorGUILayout.TextField("Prompt Message", interactable.PromptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can only use UnityEvents", MessageType.Info);
            if (interactable.GetComponent<InteractionEvent>() == null) {
                interactable.UseEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else {
            base.OnInspectorGUI();
            if (interactable.UseEvents) {
                if (interactable.GetComponent<InteractionEvent>() == null) {
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else {
                if (interactable.GetComponent<InteractionEvent>() != null) {
                    Destroy(interactable.GetComponent<InteractionEvent>());
                }
            }
        }
    }
}
