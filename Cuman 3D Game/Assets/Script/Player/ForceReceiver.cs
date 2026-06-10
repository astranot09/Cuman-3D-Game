using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private float drag = 0.3f;


    [SerializeField] private PlayerAnimator playerAnimator;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        // Menggunakan GroundChecker custom kamu agar sinkron dengan fungsi Jump
        bool isGrounded = groundChecker.IsGrounded();

        if (verticalVelocity < 0f && isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;

            // Saat di tanah, set velocityY ke 0 agar animasi kembali ke Idle/Walk
            playerAnimator.SetVelocityYAnimation(0);
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;

            // Saat di udara (melompat atau jatuh), kirim nilai aslinya ke Animator
            playerAnimator.SetVelocityYAnimation(verticalVelocity);
        }


        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

    }

    public void Jump(float jumpHeight)
    {
        if (groundChecker.IsGrounded())
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);

            // Langsung update animator begitu melompat agar transisinya instan
            playerAnimator.SetVelocityYAnimation(verticalVelocity);
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

}