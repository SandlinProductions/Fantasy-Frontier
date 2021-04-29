using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    public CharacterController controller;//reference to the CharacterController on our player
    //public Animator animator;
    public Transform cam;//use main camera and not the Cinamachine camera
    public Transform groundCheck;//this is our groundcheck gameobject on the player
    public float speed = 6f;//speed of the Character
    [SerializeField]
    private float jumpHeight = 1.0f;
    public float gravity = -9.81f;//gravity that brings us back to the ground
    public float groundDistance = 0.4f;//this is the radius of the sphere we use to check
    public LayerMask groundMask;//what objects the groundcheck checks for
    public float turnSmoothTime = .1f;//how smooth the player turns
    float turnSmoothVelocity;//speed of turn
    Vector3 velocity;//this is us falling
    bool isGrounded;//true grounded flase we are not grounded
    // Update is called once per frame
    
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//this creates a sphere on the groundcheck
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(movement.x, 0f, movement.y).normalized;//normalized help it from not going fast when going diagonal

        if (direction.magnitude >= 0.1f) //this is where the magic happens with moving the player and making it so the player's forward is the way the camera is facing
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //this rotates our player
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);//this makes a smooth rotation of our player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;//this makes it so we move forward in the direction of the camera
            controller.Move(moveDir.normalized * speed * Time.deltaTime);//this is what actully moves our player (i think)
           // var animationSpeedMultiplier = SetCorrectAnimation();
            //velocity *= animationSpeedMultiplier;
        }
        //else
        //{
        //    animator.SetFloat("move", 0);
        //}
        // Changes the height position of the player..
        if (jumpControl.action.triggered && isGrounded)
        {
            Jump();
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Jump()
    {
        velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }
    //private float SetCorrectAnimation()
    //{
    //    //float currentAnimationSpeed = animator.GetFloat("move");
    //    if (turnSmoothVelocity > 10 || turnSmoothVelocity < -10)
    //    {
    //        if (currentAnimationSpeed < 0.2f)
    //        {
    //            currentAnimationSpeed += Time.deltaTime * 2;
    //            currentAnimationSpeed = Mathf.Clamp(currentAnimationSpeed, 0, .2f);
    //        }
    //        animator.SetFloat("move", currentAnimationSpeed);
    //    }
    //    else
    //    {
    //        if (currentAnimationSpeed < 1)
    //        {
    //            currentAnimationSpeed += Time.deltaTime * 2;
    //        }
    //        else
    //        {
    //            currentAnimationSpeed = 1;
    //        }
    //        animator.SetFloat("move", currentAnimationSpeed);
    //    }
    //    return currentAnimationSpeed;
    //}

    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
    }
    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
    }

}
