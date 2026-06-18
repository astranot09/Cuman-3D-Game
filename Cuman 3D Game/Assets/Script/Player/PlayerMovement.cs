using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private bool shouldFaceMoveDirection = true;

    private CharacterController characterController;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 2f; // Tinggi lompatan dalam meter
    private ForceReceiver forceReceiver; // Referensi ke skrip ForceReceiver

    private Vector2 moveInput;
    private Vector3 moveDir;

    [Header("Animation")]
    [SerializeField] private PlayerAnimator animator;


    [Header("Movement Bool")]
    [SerializeField] private bool onMovement = true;

    // BERLANGGANAN EVENT (Saat skrip aktif)
    private void OnEnable()
    {
        PlayerInputHandler.JumpEvent += HandleJump; // Daftarkan fungsi HandleJump ke Event
    }

    // MENCABUT EVENT (Saat skrip mati/hancur)
    private void OnDisable()
    {
        PlayerInputHandler.JumpEvent -= HandleJump;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        forceReceiver = GetComponent<ForceReceiver>(); // Ambil komponen ForceReceiver
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetMoveDirection(Vector2 input)
    {
        moveInput = input;
    }

    public void UpdateMoveDirection(Vector3 cameraForward, Vector3 cameraRight)
    {
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        moveDir = (cameraForward * moveInput.y) + (cameraRight * moveInput.x);
    }

    private void Update()
    {
        if (!onMovement)
        {
            moveDir = Vector3.zero;
        }  

        // 1. Hitung total pergerakan (Jalan + Gravitasi dari ForceReceiver)
        Vector3 motion = (moveDir * moveSpeed) + forceReceiver.Movement;

        // 2. Cukup panggil fungsi Move SEKALI saja agar performa lebih stabil
        characterController.Move(motion * Time.deltaTime);

        // Animasi
        float currentSpeed = moveInput.magnitude;
        animator.SetVelocityXAnimation(currentSpeed);

        // Rotasi karakter
        if (shouldFaceMoveDirection && moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        forceReceiver.Jump(jumpHeight);
    }


    public void SetMovement(bool x)
    {
        onMovement = x;
    }


}