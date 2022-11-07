using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemEnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    [SerializeField]
    private float speed=1;
    [SerializeField]
    private Vector2 dir = Vector2.left;
    private float gravity = -9.81f;//with this we can customize gravity per entity if needed, just by serializing
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnBecameVisible()
    {
        enabled = true;
    }
    private void OnBecameInvisible()
    {
        enabled = false;
    }
    private void OnEnable()
    {
        rb.WakeUp();
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }
    private void FixedUpdate()
    {
        velocity.x = dir.x * speed;
        velocity.y += gravity*Time.fixedDeltaTime;//could use Physics.gravity.y;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        if (rb.Raycast(dir))
        {
            dir = -dir;
        }
        if (rb.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }
}
