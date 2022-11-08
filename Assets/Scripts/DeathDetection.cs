using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetection : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Sprite smallDeathSprite;
    [SerializeField] Sprite bigDeathSprite; //I can't find the big mario death sprite but doesnt matter cause we change anyway
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            if (transform.dotProduct(collision.transform, Vector2.left) || transform.dotProduct(collision.transform, Vector2.right)) {
                Destroy(GetComponent<movementScript>());
                Destroy(GetComponent<CapsuleCollider2D>());
                Destroy(GetComponentInChildren<animationScript>()); //Might have to change this because it only removes small mario's animationscript
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = smallDeathSprite;

                rb.gravityScale = 1;
                rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                print("bruh");
            }
        }
    }
}
