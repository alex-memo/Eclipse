using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * death barrier code
 */
public class deathBarrier : MonoBehaviour
{
    /**
 * @memo 2022
 * on enterind death barrier then die, if player with delay if enemy instantly
 */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.gameObject.SetActive(false);
            Controller.instance.die();          
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
