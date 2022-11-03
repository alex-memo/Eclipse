using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public item currentItem;
    private float moveSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem.isMove) {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0));
        }

    }
}
