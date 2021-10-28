using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public AudioSource skipidiiYks;
    public AudioSource skipidiiKaks;


    private Animator animator;
    private CharacterController controller;
    public Camera cam;

    public float speed = 7.0f;
    public float sprintSpeed = 12.0f;
    public float doubleJumpMultiplier = 0.5f;

    bool mSprinting = false;
    bool isMoving = false;

    float mDesiredRotation = 0;
    public float RotationSpeed = 15;

    float mDesiredAnimationSpeed = 0f;
    // How smoothly animation exits from running to walking to idle.
    public float AnimationBlendSpeed = 6f;

    float mSpeedY = 0;
    float mGravity = -9.81f;

    bool mJumping = false;
    public float jumpSpeed = 15;
    bool canDoubleJump = false;

    // ?
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();

        AudioSource[] skipi = GetComponents<AudioSource>();
        skipidiiYks = skipi[0];
        skipidiiKaks = skipi[1];
    }

    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        if (controller.velocity.x != 0)
            isMoving = true;
        else
            isMoving = false;
        if (isMoving)
        {
            if (!skipidiiYks.isPlaying)
                skipidiiYks.Play();
        } 
        else
            skipidiiYks.Stop();


        if(Input.GetButtonDown("Jump") && !mJumping)
        {
            mJumping = true;
            canDoubleJump = true;
            animator.SetTrigger("Jump");
            mSpeedY += jumpSpeed;
            skipidiiKaks.Play();
        } else if (mJumping && canDoubleJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                mSpeedY += jumpSpeed * doubleJumpMultiplier;
                canDoubleJump = false;
                skipidiiKaks.Play();
            }
        }

        if (!controller.isGrounded)
        {
            mSpeedY += mGravity * Time.deltaTime;
        } else if (mSpeedY < 0)
        {
            mSpeedY = 0;
        }

        animator.SetFloat("SpeedY", mSpeedY / jumpSpeed);

        if (mJumping && mSpeedY < 0)
        {
            RaycastHit hit;
            if( Physics.Raycast(transform.position, Vector3.down, out hit, .5f, LayerMask.GetMask("Default")))
            {
                mJumping = false;
                animator.SetTrigger("Land");
            }
        }

        mSprinting = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 rotatedMovement = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * movement;
        Vector3 verticalMovement = Vector3.up * mSpeedY;
        controller.Move((verticalMovement + (rotatedMovement * (mSprinting ? sprintSpeed : speed))) * Time.deltaTime);
        if(rotatedMovement.magnitude > 0)
        {
            mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            mDesiredAnimationSpeed = mSprinting ? 1 : .5f;
        } else
        {
            mDesiredAnimationSpeed = 0;
        }

        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), mDesiredAnimationSpeed, AnimationBlendSpeed * Time.deltaTime));
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

}
