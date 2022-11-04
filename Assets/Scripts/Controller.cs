using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private movementScript movement;
    /**
     * @memo 2022
     * Awake method, creates an instance of the player controller
     */
    private void Awake()
    {
        movement = GetComponent<movementScript>();
        if (instance == null)
        {
            instance = this;
        }       
    }
    /**
     * @memo 2022
     * getter for get movement
     */
    public movementScript getMovement()
    {
        return movement;
    }
}
