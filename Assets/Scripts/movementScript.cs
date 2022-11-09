using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * Script that handles the movement of the player
 */
public class movementScript : MonoBehaviour
{
    private Rigidbody2D body;
    private new Collider2D collider;
    private float moveSpeed = 8f;
    private float inputAxis;
    private Vector2 velocity;
    private Camera cam;
    private float jumpHeightMax = 5f;
    private float jumpTime = 1f;//idk was going to use rigidbody force but as depends on how long u press button the higher u jump that would be pain
    private float jumpForce => (2f * jumpHeightMax) / (jumpTime / 2f);//defines jump force dynamically
    private float gravity => (-2f * jumpHeightMax) / Mathf.Pow((jumpTime / 2f), 2);//defines gravity dynamically
    private bool isGrounded;
    private bool isJumping;
    private bool isRunning => Mathf.Abs(velocity.x) > .2f || Mathf.Abs(inputAxis) > .2;//gets absolute value of player velocity and if moving then running true else false
                                                                                     //if(input then also running
    private bool isTurning => (inputAxis > 0 && velocity.x < 0) || (inputAxis < 0 && velocity.x > 0);//if changiong direction then turning

    private bool isFalling => velocity.y < 0 || !Input.GetButton("Jump");//if y vel is <0 or is jumping then isfalling

    /**
     * @memo 2022
     * sets the variables from this script as needed
     */
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    /**
     * @memo 2022
     * Update method for player movement
     */
    void Update()
    {
        move();
        isGrounded = body.Raycast(Vector2.down, GetComponent<Collider2D>().offset.y);//calls the extension file 
        if (isGrounded)
        {
            groundedMovement();
        }
        gravityApply();
    }
    /**
     * @memo 2022
     * this is the jumpHandler for now
     */
    private void groundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);//prevents the y velocity to bvuild up while grounded
        isJumping = velocity.y > 0;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            isJumping = true;
        }
    }
    /**
     * @memo 2022
     * applies the gravity to the player
     */
    private void gravityApply()
    {

        float gravityMultiplayer = isFalling ? 2f : 1f;//makes the game feel like mario, if the button is pressed then it will kinda increase force of jump, and if not it will make mario fall faster
        velocity.y += gravity * Time.deltaTime * gravityMultiplayer;
        velocity.y = Mathf.Max(velocity.y, gravity / 2);//prevents y to going a bit too fast, not even necessary but just safety measure
    }
    /**
     * @memo 2022
     * handles the movement of the player
     */
    private void move()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, Time.deltaTime * moveSpeed);
        if (body.Raycast(Vector2.right * velocity.x, GetComponent<Collider2D>().offset.x) && inputAxis == 1)//if player running into wall then (right)
        {
            velocity.x = 0;//set accel to that place to 0
        }
        if (velocity.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0)//flips sprite
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }
    /**
     * @memo 2022
     * adds the velocity from move to player and prevents player from going outside screen
     */
    private void FixedUpdate()
    {
        Vector2 pos = body.position;
        pos += velocity * Time.fixedDeltaTime;

        Vector2 lEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        pos.x = Mathf.Clamp(pos.x, lEdge.x + .5f, rEdge.x - .5f);//ensures player stays inside cam
        body.MovePosition(pos);
    }
    /**
     * @memo 2022
     * checks on collision on head, if hit head and is not object then stops accel to up
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer== LayerMask.NameToLayer("Enemy"))
        {
            if (transform.dotProduct(collision.transform, Vector2.down))
            {
                
                if (Input.GetButtonDown("Jump"))
                {
                    velocity.y = jumpForce;
                }
                else
                {
                    velocity.y = jumpForce / 2;
                }
                isJumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("Items"))
        {

            if (transform.dotProduct(collision.transform, Vector2.up))
            {
                velocity.y = 0;//if hit head then reset velocity so that it doesnt stay floating
            }
        }
    }
    /**
     * @memo 2022
     * getter for inJumping
     */
    public bool getIsJumping()
    {
        return isJumping;
    }
    /**
    * @memo 2022
    * getter for isTurning
     */
    public bool getIsTurning()
    {
        return isTurning;
    }
    /**
    * @memo 2022
    * getter for isRunning
    */
    public bool getIsRunning()
    {
        return isRunning;
    }
}
