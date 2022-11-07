using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goombaScript : MonoBehaviour
{
    public Sprite flatSprite;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //chack dir of player is going down, meaning they stomped him
            if(collision.transform.dotProduct(transform, Vector2.down))
            {
                flattenGoomba();
            }
        }
    }
    private void flattenGoomba()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<itemEnemyMovement>().enabled = false;
        GetComponent<animationScript>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, .5f);
    }
}
