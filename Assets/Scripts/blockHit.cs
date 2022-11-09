using UnityEngine;
/**
* @memo 2022
* block hit class, handles player hitting blocks
*/
public class blockHit : MonoBehaviour
{
    public int maxHits = -1;
    public Sprite brokenBlock;
    private bool isAnimating;
    public item item;
    /**
* @memo 2022
* oncollision enter, if player hits this then
*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAnimating&&maxHits!=0&&collision.gameObject.CompareTag("Player"))
        {//if not currently animating the block & there is still something in box then
            if(collision.transform.dotProduct(transform, Vector2.up))
            {//checks if player hits from bottom to top
                hit();
            }
        }
    }
    /**
* @memo 2022
* handles hit from player
*/
    private void hit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        maxHits--;
        if (maxHits == 0)
        {
            sprite.sprite = brokenBlock;
        }
        if (item != null)
        {
            GameObject itemObj=Instantiate(item.itemObject, transform.position, Quaternion.identity);
            itemObj.GetComponent<itemScript>().setItem(item);
        }
        anim();
        
    }
    /**
* @memo 2022
* animates the hit
*/
    private void anim()
    {
        isAnimating = true;
        StartCoroutine(transform.anim(.5f));
        isAnimating = false;
    }
}
