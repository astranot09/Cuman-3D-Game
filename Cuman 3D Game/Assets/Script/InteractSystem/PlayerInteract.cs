using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField] private float interactRadius = 5f;
    [SerializeField] private LayerMask interactLayer;

    IInteractable interactableObject;

    private void Update()
    {
        interactableObject = FindNearestInteractable();
        if (interactableObject != null)
        {
            interactableObject.InInteractRange();
        }
        else
        {
            interactableObject = null;
        }
    }

    IInteractable FindNearestInteractable()
    {
        Collider[] interactableObjects = Physics.OverlapSphere(transform.position, interactRadius, interactLayer);

        IInteractable interactableNearest = null;
        float mostNearDistance = float.MaxValue;

        foreach(Collider c in interactableObjects)
        {
            float distance = (transform.position - c.transform.position).sqrMagnitude;
            if(distance < mostNearDistance)
            {
                mostNearDistance = distance;
                interactableNearest = c.GetComponent<IInteractable>();
            }
        }
        return interactableNearest;
    }

    public void PlayerIsInteract()
    {
        if(interactableObject != null)
        {
            Debug.Log("Player Interact");
            interactableObject.OnInteract();
        }
        else
        {
            Debug.Log("Player ga bisa interact");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
