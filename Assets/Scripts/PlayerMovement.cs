using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;

    public Transform Orientation;

    float HorizontalInput;
    float VerticalInput;

    Vector3 Direction;

    Rigidbody RB;

    public float GroundDrag;

    public float JumpForce;
    public float JumpCoolDown;
    public float AirMultiplier;

    bool ReadyToJump;

    public AudioSource WalkingSound;

    [Header("Ground Check")]
    public float Height;
    public LayerMask WhatIsGround;
    bool Grounded;

    [Header("Keybinds")]
    public KeyCode JumpKey = KeyCode.Space;

    public GameObject DamageDisplay;

    private int Health = 100;
    // Start is called before the first frame update

    public TMPro.TMP_Text Text;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
        ReadyToJump = true;
        WalkingSound = GetComponent<AudioSource>();
        Text.SetText("HEALTH: " + Health.ToString());
    }

    public void TakeDamage()
    {
        Health -= 5;
        Text.SetText("HEALTH: " + Health.ToString());
        DisplayDamageMask();
        
    }
    
    private void DisplayDamageMask()
    {
        DamageDisplay.SetActive(true);
        Invoke("RemoveDamageMask", 1.0f);
    }

    private void RemoveDamageMask()
    {
        DamageDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Grounded = Physics.Raycast(transform.position, Vector3.down, Height * 0.5f + 0.5f, WhatIsGround);
        MyInput();
        SpeedControl();

        RB.drag = Grounded ? GroundDrag : 0;        

        if (Health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    private void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(JumpKey) && ReadyToJump && Grounded)
        {
            Jump();
            ReadyToJump = false;

            Invoke(nameof(ResetJump), JumpCoolDown);
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        Direction = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;

        Vector2 Movement;
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        var Moving = Movement != Vector2.zero;

        if (Grounded && Moving && !WalkingSound.isPlaying)
        {
            WalkingSound.Play(0);
        }
        else if ((!Moving || !Grounded) && WalkingSound.isPlaying)
        {
            WalkingSound.Stop();
        }

        if (Grounded)
            RB.AddForce(Direction.normalized * Speed * 10f, ForceMode.Force);
        else
            RB.AddForce(Direction.normalized * Speed * 10f * AirMultiplier, ForceMode.Force);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -17, 17), transform.position.y, Mathf.Clamp(transform.position.z, -17, 17));
    }

    private void SpeedControl()
    {
        var flatVelocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);
        if (flatVelocity.magnitude > Speed)
        {
            var limitedVelocity = flatVelocity.normalized * Speed;
            RB.velocity = new Vector3(limitedVelocity.x, RB.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);
        RB.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        ReadyToJump = true;
    }
}
