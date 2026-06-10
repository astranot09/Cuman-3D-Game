using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private PlayerMovement playerMovement;

    private void Update()
    {
        UpdateMovementVectors();
    }

    private void UpdateMovementVectors()
    {
        if (cameraPivot == null || playerMovement == null) return;

        // Send the raw camera directions straight to the movement script to process
        playerMovement.UpdateMoveDirection(cameraPivot.forward, cameraPivot.right);
    }
}