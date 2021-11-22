using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private GameController gameController;

    // Player audio sources
    public AudioSource runSound1;
    public AudioSource runSound2;
    public AudioSource jumpSound;

    private Animator animator;
    private CharacterController controller;
    public Camera cam;
    public float speed = 7.0f;
    public float sprintSpeed = 12.0f;
    public float doubleJumpMultiplier = 0.5f;
 
    bool isSprinting = false;

    float mDesiredRotation = 0;
    public float RotationSpeed = 15;

    float mDesiredAnimationSpeed = 0f;

    // How smoothly animation exits from running to walking to idle.
    public float AnimationBlendSpeed = 6f;

    float mSpeedY = 0;
    float mGravity = -9.81f;

    bool isJumping = false;
    public float jumpSpeed = 15;
    bool canDoubleJump = false;

    // Fetch components from player
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        controller = GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
        AudioSource[] audio = GetComponents<AudioSource>();
        runSound1 = audio[0];
        runSound2 = audio[1];
        jumpSound = audio[2];
    }

    // Executes PlayerMovement
    void Update()
    {
        PlayerMovement();
    }

    // Player movements and sound effects
    private void PlayerMovement()
    {
        // Input variables
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        isSprinting = Input.GetKey(KeyCode.LeftShift);


        // Check if player is moving and walking/running sounds should be stopped
        if (controller.velocity.x == 0)
        {
            runSound1.Stop();
            runSound2.Stop();
        }

        // Check if player is jumping / double jumping
        // If jumping, sound effect and animations should be played. 
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            canDoubleJump = true;
            animator.SetTrigger("Jump");
            mSpeedY += jumpSpeed;
            jumpSound.Play();
        }
        else if (isJumping && canDoubleJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                mSpeedY += jumpSpeed * doubleJumpMultiplier;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        // How fast player falls in air
        if (!controller.isGrounded)
        {
            mSpeedY += mGravity * Time.deltaTime;
        }
        else if (mSpeedY < 0)
        {
            mSpeedY = 0;
        }

        // Set animator float to determine what part of the animations should be played.
        animator.SetFloat("SpeedY", mSpeedY / jumpSpeed);

        // When player hits defined surface, trigger Landing animation.
        if (isJumping && mSpeedY < 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f, LayerMask.GetMask("Default","Ground")))
            {
                isJumping = false;
                animator.SetTrigger("Land");
            }
        }

        
        // Movement related code
        // Movement animation code
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 rotatedMovement = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * movement;
        Vector3 verticalMovement = Vector3.up * mSpeedY;
        controller.Move((verticalMovement + (rotatedMovement * (isSprinting ? sprintSpeed : speed))) * Time.deltaTime);
        if (rotatedMovement.magnitude > 0)
        {
            mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            mDesiredAnimationSpeed = isSprinting ? 1 : .5f;
        }
        else
        {
            mDesiredAnimationSpeed = 0;
        }

        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), mDesiredAnimationSpeed, AnimationBlendSpeed * Time.deltaTime));
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Part")
        {
            print("Testit testailtu");
            gameController.saveData.levelsCompleted += 1;
            print(gameController.saveData.levelsCompleted);
        } 
    }

}