using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float jumpHeight = 2.0f;
    public float sensitivity = 3.0f;
    public float JumpingDirectionDecay = 2.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float moveDirectionBeforJumpX = 0;
    private float moveDirectionBeforJumpZ = 0;
    private float verticalVelocity = 0.0f;

    [SerializeField] private GunManager m_GunManager;

    private bool IsWalking = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            return;

        CheckWeaponChange();

        // Rotate the player based on mouse movement
        float rotX = Input.GetAxis("Mouse X") * sensitivity;
        transform.Rotate(0, rotX, 0);

        // Jumping
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpHeight;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // Movement
        float forward = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (!characterController.isGrounded)
        {
            forward = GetClampedAxisMovementInAir(moveDirectionBeforJumpZ, forward);
            horizontal = GetClampedAxisMovementInAir(moveDirectionBeforJumpX, horizontal);
        }
        else
        {
            moveDirectionBeforJumpX = horizontal;
            moveDirectionBeforJumpZ = forward;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            m_GunManager.Reload();
        }
        else if (Input.GetButton("Fire1"))
        {
            m_GunManager.Shoot();
        }
        else if (forward != 0 || horizontal != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                IsWalking = true;
                m_GunManager.SetState(GunManager.GUNSTATES.GUNSTATE_WALK);
            }
            else
            {
                IsWalking = false;
                m_GunManager.SetState(GunManager.GUNSTATES.GUNSTATE_RUN);
            }
        }
        else
        {
            m_GunManager.SetState(GunManager.GUNSTATES.GUNSTATE_IDLE);
        }

        moveDirection = new Vector3(horizontal, verticalVelocity, forward);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= IsWalking ? speed / 4 : speed;        

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private float GetClampedAxisMovementInAir(float axisValueInAir, float dirrection)
    {
        if (axisValueInAir == 0)
            return dirrection / JumpingDirectionDecay;

        return axisValueInAir > 0 ? Mathf.Clamp(dirrection, 0, dirrection) : Mathf.Clamp(dirrection, dirrection, 0);
    }

    void CheckWeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_GunManager.SetActiveGun(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_GunManager.SetActiveGun(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_GunManager.SetActiveGun(2);
        }
    }
}