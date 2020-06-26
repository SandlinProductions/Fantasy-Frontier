using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;//reference to the CharacterController on our player
    public Transform cam;//use main camera and not the Cinamachine camera
    public Transform groundCheck;//this is our groundcheck gameobject on the player
    public float speed = 6f;//speed of the Character
    public float gravity = -9.81f;//gravity that brings us back to the ground
    public float groundDistance = 0.4f;//this is the radius of the sphere we use to check
    public LayerMask groundMask;//what objects the groundcheck checks for
    public float turnSmoothTime = .1f;//how smooth the player turns
    float turnSmoothVelocity;//speed of turn
    Vector3 velocity;//this is us falling
    bool isGrounded;//true grounded flase we are not grounded
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);//this creates a sphere on the groundcheck
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");//this moves the player side to side
        float vertical = Input.GetAxisRaw("Vertical");//this moves the player forward and back
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;//normalized help it from not going fast when going diagonal

        if(direction.magnitude >= 0.1f) //this is where the magic happens with moving the player and making it so the player's forward is the way the camera is facing
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //this rotates our player
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);//this makes a smooth rotation of our player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;//this makes it so we move forward in the direction of the camera
            controller.Move(moveDir.normalized * speed * Time.deltaTime);//this is what actully moves our player (i think)
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
