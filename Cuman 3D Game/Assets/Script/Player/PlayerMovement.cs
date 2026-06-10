using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f; // Separate rotation speed for smoother control
    [SerializeField] private bool shouldFaceMoveDirection = true;

    [Header("Physics & Gravity")]
    [SerializeField] private float gravity = -15f;       // Kekuatan gravitasi (bisa kamu sesuaikan)
    [SerializeField] private float jumpHeight = 2f;


    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector3 moveDir;


    private Vector3 velocity;                            // Menyimpan kecepatan vertikal (Y) untuk gravitasi
    private bool isGrounded;

    [Header("Animation")]
    [SerializeField] private Animator animator;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetMoveDirection(Vector2 input)
    {
        moveInput = input;
    }

    // Camera will call this from LateUpdate to pass the correct camera relative vectors
    public void UpdateMoveDirection(Vector3 cameraForward, Vector3 cameraRight)
    {
        // Flatten the vectors so looking up/down doesn't affect ground movement
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate final movement vector (moveInput.y is Forward/Backward, moveInput.x is Left/Right)
        moveDir = (cameraForward * moveInput.y) + (cameraRight * moveInput.x);
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            // Reset velocity Y jadi sedikit minus agar player tetap menempel di lantai/turunan dengan stabil
            velocity.y = -2f;
        }






        // Move the character
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);



        // Rumus Fisika: Kecepatan bertambah seiring waktu dikali gravitasi (v = g * t)
        velocity.y += gravity * Time.deltaTime;

        // Gerakkan player secara Vertikal (Ke bawah/Jatuh)
        characterController.Move(velocity * Time.deltaTime);



        float currentSpeed = moveInput.magnitude;

        // Kirim nilai speed ke animator. 
        // Di Animator, kamu tinggal bikin kondisi transisi: 
        // Jika Speed > 0.1 maka Walk/Run, Jika Speed < 0.1 maka kembali ke Idle.
        animator.SetFloat("velocityX", currentSpeed);

        // Rotate the character to face movement direction
        if (shouldFaceMoveDirection && moveDir.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}