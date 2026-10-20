using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private PortalScript targetLocation;
    bool canTeleport = true;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Nabrak Sesuatu");
        if (other.CompareTag("Player") && canTeleport)
        {
            Debug.Log("Nabrak Player");
            CharacterController cc = other.GetComponent<CharacterController>();
            cc.enabled = false;
            canTeleport = false;
            targetLocation.canTeleport = false;
            other.transform.position = targetLocation.transform.position;
            cc.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }
}
