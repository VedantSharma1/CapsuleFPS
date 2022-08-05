using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 8f;

    public bool isGrounded;
    private bool sprinting;
    private bool doubleJump;
    public float gravity = -30f;

    public float jumpHeight = 1.8f;

    private GunController gunController;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gunController = GameObject.Find("ak47").GetComponent<GunController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            isGrounded = controller.isGrounded;
            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                gunController.WeaponDown();
                speed = 13f;
            }
            else
            {
                speed = 8f;
            }
        }
        

    }

    private void FixedUpdate()
    {
        
    }

    //recieve the inputs from our input manager script and apply them to our character controller
    public void ProcessMove(Vector2 input)
    {   
        //left and right movement
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y; // getting the y component and applying to z axis
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime; // applies downward force but it keeps on accumulating every frame even when we are grounded
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // having a small -ve value , which i think will be used for jumping
        }
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            doubleJump = true;
        }

        if (!isGrounded && doubleJump)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            doubleJump = false;
        }
    }

   

}
