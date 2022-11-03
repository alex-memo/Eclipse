using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    private Rigidbody2D body;
    private float moveSpeed=5f;
    private float inputAxis;
    private Vector2 velocity;
    private Camera cam;

    private void Awake() 
    {
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    void Update() 
    {
        move(); 
    }
    private void move()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x,inputAxis*moveSpeed,Time.deltaTime*moveSpeed);
    }
    private void FixedUpdate()
    {
        Vector2 pos = body.position;
        pos += velocity * Time.fixedDeltaTime;

        Vector2 lEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        pos.x = Mathf.Clamp(pos.x, lEdge.x+.5f, rEdge.x-.5f);//ensures player stays inside cam
        body.MovePosition(pos);
    }
}
