using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("Custom Ground Check")]
    [SerializeField] private Transform groundCheckPoint; // Tarik objek kosong di kaki player ke sini
    [SerializeField] private float groundCheckRadius = 0.1f; // Ini ukuran radius yang bisa kamu set bebas di Inspector
    [SerializeField] private LayerMask groundLayer; // Pilih layer tanah (misal: Default atau Ground)


    public bool IsGrounded()
    {
        return (Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer));
    }

}
