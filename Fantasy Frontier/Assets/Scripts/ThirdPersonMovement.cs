using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;//reference to the CharacterController on our player
    public Transform cam;//use main camera and not the Cinamachine camera
    public float speed = 6f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;//normalized help it from not going fast when going diagonal

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //this rotates our player
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);//this makes a smooth rotation of our player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;//this makes it so we move forward in the direction of the camera
            controller.Move(moveDir.normalized * speed * Time.deltaTime);//this is what actully moves our player (i think)
        }

    }
}
