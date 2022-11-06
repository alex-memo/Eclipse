using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public item currentItem;
    private Rigidbody2D rb; 
    private float moveSpeed = 2;
    private float gravity = -10;
    private Vector2 velocity;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        CheckMovement();
        ApplyGravity();
    }

    void CheckMovement() {
        if (currentItem.isMove) {
            velocity.x = Mathf.MoveTowards(velocity.x, -moveSpeed, Time.deltaTime * moveSpeed);
        }
    }
    void ApplyGravity() {
        velocity.y += gravity * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, rb.Raycast(Vector2.down)?0:gravity); //If item is grounded, y velocity is 0. otherwise make sure it doesn't fall too fast

        //Debug to check if raycast is working on mushroom (y velocity should be 0 when grounded) (ITS NOT WORKING HELP ALEX)
        print(velocity.y);
    }

    private void FixedUpdate() {
        rb.position += velocity * Time.fixedDeltaTime;
    }
}
