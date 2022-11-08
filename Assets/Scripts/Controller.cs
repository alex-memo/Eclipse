using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * player Controller script
 */
public class Controller : MonoBehaviour
{
    public static Controller instance;
    private movementScript movement;

    private spriteRendererScript smallRenderer;
    private spriteRendererScript bigRenderer;
    private deathAnimation deathAnimation;
    private bool big => bigRenderer.enabled;
    private bool dead => deathAnimation.enabled;
    /**
     * @memo 2022
     * Awake method, creates an instance of the player controller
     */
    private void Awake()
    {
        movement = GetComponent<movementScript>();
        deathAnimation = GetComponent<deathAnimation>();
        smallRenderer = transform.GetChild(0).GetComponent<spriteRendererScript>();
        bigRenderer = transform.GetChild(1).GetComponent<spriteRendererScript>();
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
    /**
 * @memo 2022
 * on player get hit
 */
    public void getHit()
    {
        if (big)
        {
            shrink();
        }
        else
        {
            die();
        }
    }
    /**
 * @memo 2022
 * if big amrio shrink then do this
 */
    private void shrink()
    {

    }
    /**
 * @memo 2022
 * when die do this
 */
    private void die()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        gameManager.instance.onDie(3f);
    }
}
