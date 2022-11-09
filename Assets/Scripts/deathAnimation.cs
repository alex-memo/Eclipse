using System;
using System.Collections;
using UnityEngine;
/**
 * @memo 2022
 * script to play death animations
 */
public class deathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;
    /**
 * @memo 2022
 * when this component is assigned to the inspector then set the sprite renderer to the default one 
 */
    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /**
 * @memo 2022
 * when this is enabled meaning the entity has died
 */
    private void OnEnable()
    {
        updateSprite();
        noPhysics();
        StartCoroutine(animate());
    }
    /**
 * @memo 2022
 * sets the sprite to the death one if existing
 */
    private void updateSprite()
    {
        spriteRenderer.enabled = true;
        //spriteRenderer.sortingOrder = 10;
        if (deadSprite != null)//just to prevent nullpointers
        {
            spriteRenderer.sprite = deadSprite;
        }
        
    }
    /**
 * @memo 2022
 * disables physics from this entity
 */
    private void noPhysics()
    {
        Collider2D[] coll = GetComponents<Collider2D>();
        foreach(Collider2D c in coll)
        {
            c.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        try
        {
            GetComponent<movementScript>().enabled = false;
        }
        catch (Exception)
        {
            GetComponent<itemEnemyMovement>().enabled = false;
        }
        
        
    }
    /**
 * @memo 2022
 * animates the death 
 */
    private IEnumerator animate()
    {
        float timer = 0;
        float duration = 3;
        float jumpVel = 10f;
        float gravity = -36f;
        Vector3 vel = Vector3.up * jumpVel;
        while (timer < duration)
        {
            transform.position += vel * Time.deltaTime;
            vel.y += gravity * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
