using System.Collections;
using UnityEngine;
/**
* @memo 2022
* class to handle block items
*/

public class blockItem : MonoBehaviour
{
    /**
* @memo 2022
* start method, calls the animation
*/
    void Start()
    {
        StartCoroutine(anim());
    }
    /**
* @memo 2022
* animate the item coming out from block
*/
    private IEnumerator anim()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        CircleCollider2D coll = GetComponent<CircleCollider2D>();
        BoxCollider2D trigger = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        itemEnemyMovement movement = GetComponent<itemEnemyMovement>();
        rb.isKinematic = true;
        coll.enabled = false;
        trigger.enabled = false;
        spriteRenderer.enabled = false;
        movement.enabled=false;
        yield return new WaitForSeconds(.25f);
        spriteRenderer.enabled = true;
        float timer = 0;
        float duration = .5f;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = transform.localPosition+Vector3.up;
        while (timer < duration)
        {
            float temp = timer / duration;
            transform.position = Vector3.Lerp(startPos, endPos, temp);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPos;
        rb.isKinematic = false;
        coll.enabled = true;
        trigger.enabled = true;
        movement.enabled = true;
    }
}
