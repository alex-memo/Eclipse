using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * item movement script
 */
public class itemEnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    [SerializeField]
    private float speed=1;
    [SerializeField]
    private Vector2 dir = Vector2.left;
    private float gravity = -9.81f;//with this we can customize gravity per entity if needed, just by serializing
    /**
 * @memo 2022
 * on awake assign rigidbody and disable this component
 */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;
    }
    /**
 * @memo 2022
 * when becomes visible enable this script
 */
    private void OnBecameVisible()
    {
        enabled = true;
    }
    /**
 * @memo 2022
 * when this entity becomes invisible then disable this script
 */
    private void OnBecameInvisible()
    {
        enabled = false;
    }
    /**
 * @memo 2022
 * when enabled wakes the rigidbody
 */
    private void OnEnable()
    {
        rb.WakeUp();
    }
    /**
 * @memo 2022
 * when disabled stops the velocity and puts rigidbody to sleep
 */
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }
    /**
 * @memo 2022
 * fixed update to handle the physics for this entity
 */
    private void FixedUpdate()
    {
        velocity.x = dir.x * speed;
        velocity.y += gravity*Time.fixedDeltaTime;//could use Physics.gravity.y;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        if (rb.Raycast(dir))
        {
            dir = -dir;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (rb.Raycast(Vector2.down))
        {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
    }
    /**
 * @memo 2022
 * setter for dir //direction
 */
    public void setDir(Vector2 direction)
    {
        dir = direction;
    }
    /**
 * @memo 2022
 * setter for speed 
 */
    public void setSpeed(float s)
    {
        speed = s;
    }
}
