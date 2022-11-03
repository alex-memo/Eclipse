using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = Controller.instance.transform;
    }
    private void LateUpdate()//late update to guarentee player position
    {
        Vector3 camPos = transform.position;
        camPos.x = Mathf.Max(camPos.x,player.position.x);
        transform.position = camPos;
    }
}
