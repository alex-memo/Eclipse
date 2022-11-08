using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * Script that handles the movement of the camera
 */
public class cameraScript : MonoBehaviour
{
    private Transform player;
    /**
     * @memo 2022
     * Start method, sets the player var to the player 
     */
    private void Start()
    {
        player = Controller.instance.transform;
    }
    /**
     * @memo 2022
     * Late update, handles the camera movement, will not let the camera go back
     */
    private void LateUpdate()//late update to guarentee player position
    {
        Vector3 camPos = transform.position;
        camPos.x = Mathf.Max(camPos.x,player.position.x);//prevents the cam from going back
        transform.position = camPos;
    }
}
