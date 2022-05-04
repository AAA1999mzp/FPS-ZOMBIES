using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour
{
    public CharacterController playercontroller;
    private float movespeed = 10f;
    public float runspeed = 10f;
    public float walkspeed = 5f;
    public float g;
    public Vector3 velocity;
    public LayerMask ground;
    public bool onGround;
    public Transform checkSpherepos;
    public float checkRadius;
    public float jumpHeight;
    public bool iswalking;
    public bool isidle;
    public AudioSource jumpAudio;
    public AudioSource walkingAudio;
    public AudioSource runningAudio;
    CharacterController characterCollider;
    public bool jumped;
    public bool Crouched;

    [HideInInspector]
    public Vector2 RunAxis;
    [HideInInspector]
    public bool SprintAxis;
    [HideInInspector]
    public bool JumpAxis;
    [HideInInspector]
    public bool CrouchAxis;

    //public FixedJoystick joystick;
    public VariableJoystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        characterCollider = gameObject.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Sprint") || SprintAxis)
        {
            if (runningAudio.isPlaying == false)
                runningAudio.Play();
            movespeed = runspeed;
            iswalking = false;
            isidle = false;
        }
        else
        {
            movespeed = walkspeed;
            iswalking = true;
            isidle = false;
        }

        if (CrossPlatformInputManager.GetButtonDown("Crouch") || CrouchAxis)
        {
            jumped = false;
            Crouched = !Crouched;
            if (Crouched)
            {
                characterCollider.height = 1.0f;
            }
            if (!Crouched)
            {
                characterCollider.height = 2.0f;
            }
        }

        onGround = Physics.CheckSphere(checkSpherepos.position, checkRadius, ground);

        if (onGround == true && velocity.magnitude > 2f && walkingAudio.isPlaying == false)
        {
            walkingAudio.volume = Random.Range(0.8f, 1.0f);
            walkingAudio.pitch = Random.Range(0.8f, 1.1f);
            walkingAudio.Play();
            velocity.y = -1f;
        }
        else
        {
            velocity.y -= g * Time.deltaTime;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump") || JumpAxis && onGround)
        {
            jumped = true;
            Crouched = false;
            characterCollider.height = 2.0f;
            velocity.y = jumpHeight;
            jumpAudio.Play();
        }

        playercontroller.Move(velocity);

        //float x = Input.GetAxis("Horizontal") * movespeed * Time.deltaTime;
        //float y = Input.GetAxis("Vertical") * movespeed * Time.deltaTime;

        //playercontroller.Move(transform.forward * y);
        //playercontroller.Move(transform.right * x);

        //float x = joystick.Horizontal * movespeed * Time.deltaTime;
        //float y = joystick.Vertical * movespeed * Time.deltaTime;

        float x = RunAxis.x * movespeed * Time.deltaTime;
        float y = RunAxis.y * movespeed * Time.deltaTime;

        playercontroller.Move(transform.forward * y);
        playercontroller.Move(transform.right * x);

        if (x == 0f && y == 0f)
        {
            isidle = true;
            runningAudio.Stop();
            walkingAudio.Stop();
        }
    }
    public void JumpButton()
    {
        JumpAxis = true;
    }
    public void CrouchButton()
    {
        CrouchAxis = true;
    }
    public void SprintButton()
    {
        SprintAxis = true;
    }
}