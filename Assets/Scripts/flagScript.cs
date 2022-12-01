using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * script for flag and end of game
 */
public class flagScript : MonoBehaviour
{
    [SerializeField]
    private Transform flag;
    [SerializeField]
    private Transform bottom;
    [SerializeField]
    private Transform castle;
    private float speed=3f;
    /**
     * @memo 2022
     * if the player touched flag
     */
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            StartCoroutine(move(flag, bottom.position));
            StartCoroutine(completeLvl(coll.transform));
        }
    }
    /**
 * @memo 2022
 * sequence to complete level
 */
    private IEnumerator completeLvl(Transform player)
    {
        player.GetComponent<movementScript>().enabled= false;
        gameManager.instance.PlayWin();
        yield return move(player, bottom.position);
        yield return move(player, player.position+Vector3.right);
        yield return move(player, player.position+Vector3.right+Vector3.down);
        yield return move(player, castle.position);

        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        gameManager.instance.NextLevel();
    }
    /**
 * @memo 2022
 * moves the player or flag to the next position to animate it
 */
    private IEnumerator move(Transform player, Vector3 pos)
    {
        while(Vector3.Distance(player.position, pos) > .1) 
        {
            player.position=Vector3.MoveTowards(player.position, pos, speed*Time.deltaTime);
            yield return null;
        }
        player.position = pos;
    }
}
