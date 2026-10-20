using UnityEngine;

public class NPCInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactName;
    public string nameInteract => interactName;

    [SerializeField] private DialogueSO dialogueSO;

    public void InInteractRange()
    {
        Debug.Log("Player masuk area: " + nameInteract);
    }

    public void OnInteract()
    {
        Debug.Log("Interaksi dengan: " + nameInteract);
        DialogueManager.instance.PlayDialogue(dialogueSO);
    }
}
