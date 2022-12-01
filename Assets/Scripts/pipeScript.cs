using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * script to handle pipes
 */
public class pipeScript : MonoBehaviour
{
    [SerializeField]
    private KeyCode enterKey=KeyCode.S;
    [SerializeField]
    private Vector3 enterDir = Vector3.down;
    [SerializeField]
    private Vector3 exitDir = Vector3.zero;
    [SerializeField]
    private Transform connect;

    /**
 * @memo 2022
 * if player is on a pipe and enters it
 */
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (Input.GetKeyDown(enterKey)&&connect!=null)
            {
                StartCoroutine(animate(coll.transform));
            }
        }
    }
    /**
 * @memo 2022
 * call animation and change position and scale of player
 */
    private IEnumerator animate(Transform player)
    {
        player.GetComponent<movementScript>().enabled= false;
        player.GetComponent<Controller>().Play(gameManager.instance.getSoundManager().pipe);
        Vector3 enterPos = transform.position + enterDir;
        Vector3 scaling = Vector3.one / 2;
        yield return anim(player, enterPos, scaling);
        yield return new WaitForSeconds(1);
        Camera.main.GetComponent<cameraScript>().underground= connect.position.y<0;
        
        if(exitDir!=Vector3.zero)//if pipe to connect
        {
            player.position=connect.position-exitDir;
            yield return anim(player,connect.position+exitDir, Vector3.one);
        }
        else//connect to fixed point
        {
            player.position=connect.position;
            player.localScale= Vector3.one;
        }
        player.GetComponent<movementScript>().enabled = true;
    }
    /**
 * @memo 2022
 * animate player entering pipe
 */
    private IEnumerator anim(Transform player, Vector3 endPos, Vector3 endScale)
    {
        float timer = 0;
        float duration = 1;
        Vector3 startPos=player.position;
        Vector3 scale=player.localScale;
        while(timer<duration) 
        {
            float temp = timer / duration;
            player.transform.localPosition = Vector3.Lerp(startPos, endPos, temp);
            player.localScale=Vector3.Lerp(scale, endScale, temp);
            timer += Time.deltaTime;
            yield return null;
        }
        player.position = endPos;
        player.localScale = endScale;
    }
}
