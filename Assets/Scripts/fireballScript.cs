using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * script for fireballs
 */
public class fireballScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed=10;

    /**
 * @memo 2022
 * sets fireball to die after 10 sec if nothing hit
 */
    void Start()
    {
        Destroy(gameObject,5f);
        rb=GetComponent<Rigidbody2D>();
        rb.velocity=transform.right*speed;
        
    }
    /**
 * @memo 2022
 * if the fireball exceeds a set speed then cap it
 */
    private void FixedUpdate()
    {
        if (rb.velocity.y > 5)
        {
            rb.velocity= new Vector2(rb.velocity.x,-4);
        }
    }
    /**
 * @memo 2022
 * if hits an enemy will enemy
 */
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {            
            coll.GetComponent<enemyScript>().blockHit();
        }
    }
    /**
 * @memo 2022
 * if out of screen then destroy this object
 */
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
