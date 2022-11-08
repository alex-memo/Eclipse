using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * koopa script, sub from enemyScript
 */
public class koopaScript : enemyScript
{
    private bool isShell;
    private bool isShellMoving;
    private float shellSpeed = 12;
    /**
 * @memo 2022
 * onCollision enter override
 *  if is not shelled then do super  
 */
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isShell)
        {
            base.OnCollisionEnter2D(collision);
        }
    }
    /**
 * @memo 2022
 * onTrigger enter override
 * kill player or move shell
 */
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (isShell && collision.CompareTag("Player"))
        {
            if (!isShellMoving)
            {
                Vector2 dir = new Vector2(transform.position.x - collision.transform.position.x, 0f);
                push(dir);
            }
            else
            {
                Controller.instance.getHit();
            }
        }
        else if (!isShell)
        {
            base.OnTriggerEnter2D(collision);
        }
        
    }
    /**
 * @memo 2022
 * if goes out of screen as shell then destroys the item to match original game
 */
    private void OnBecameInvisible()
    {
        if (isShellMoving)//this is to match original game as if shell goes out of screen in g game the shell just poofs
        {
            Destroy(gameObject);
        }
    }
    /**
     * @memo 2022
     * pushes shell 
     */
    private void push(Vector2 dir)
    {
        isShellMoving = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        itemEnemyMovement movement = GetComponent<itemEnemyMovement>();
        movement.setDir(dir.normalized);
        movement.setSpeed(shellSpeed);
        movement.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Shell");
    }
    /**
 * @memo 2022
 * on koopa die 
 */
    protected override void onDie()
    {
        isShell = true;
        GetComponent<itemEnemyMovement>().enabled = false;
        GetComponent<animationScript>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
    }
}
