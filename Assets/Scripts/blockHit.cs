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
    public BlockType blockType;
    /**
     * @memo 2022
     * enumerator for block types
     */
    public enum BlockType { Brick, Mystery_Block, Solid}
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
        if (!(blockType == BlockType.Brick&&Controller.instance.getSize().Equals("small")&&item==null))
        {
            maxHits--;

            if (item != null)
            {
                GameObject itemObj;
                if (item.effectType == item.EffectType.Big_Mushroom && Controller.instance.getSize().Equals("big"))
                {                    
                    itemObj = Instantiate(gameManager.instance.fireFlower.itemObject, transform.position, Quaternion.identity);
                    itemObj.GetComponent<itemScript>().setItem(gameManager.instance.fireFlower);
                }
                else
                {
                    itemObj = Instantiate(item.itemObject, transform.position, Quaternion.identity);
                    itemObj.GetComponent<itemScript>().setItem(item);
                }
                
            }
        }
        
        if (maxHits == 0)
        {
            sprite.sprite = brokenBlock;
            if (brokenBlock == null)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
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
        enemyOnTop();
        isAnimating = false;
    }
    /**
     * @memo 2020
     * Debugs a sphere in a set location
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector3(transform.localPosition.x,transform.localPosition.y+.5f), .375f);
        
    }
    /**
     * @memo 2021 -Updated 2022
     * Creates a sphere on a set location
     * Checks if there are repeated entities
     * Only does what it should once for every collider
     * inside of the created sphere
     */
    private void enemyOnTop()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(new Vector2(transform.localPosition.x, transform.localPosition.y + .5f), .375f,(LayerMask.GetMask("Enemy","Shell")));
        int[] hittedEnemies = new int[hitEnemies.Length];
        int i = 0;
        foreach (Collider2D enemy in hitEnemies)
        {
            int id = enemy.gameObject.GetInstanceID();
            int j = 0; bool isHit = true;
            while (j < hittedEnemies.Length)
            {
                if (hittedEnemies[j] == id)
                {
                    isHit = false;
                }

                j++;
            }
            hittedEnemies[i] = enemy.gameObject.GetInstanceID();
            i++;
            if (isHit == true)//if there is a unique enemy collider
            {
                enemy.GetComponent<enemyScript>().blockHit();
                
            }
        }
    }
}
