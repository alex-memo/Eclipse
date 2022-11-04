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
        if (instance == null)
        {
            instance = this;
        }
        movement = GetComponent<movementScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
