using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * Script that handles the sprites from player
 */
public class spriteRendererScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private movementScript movement;
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    private animationScript run;
    /**
    * @memo 2022
    * awake mathdo, sets the needed components
    */
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        run = GetComponent<animationScript>();       
    }
    /**
    * @memo 2022
    * sets the movement because for some reason it bvreaks on awake as this is being called before movement
    */
    private void Start()
    {
        movement = Controller.instance.getMovement();
    }
    /**
 * @memo 2022
 * on enable enable the sprite renderer
 */
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    /**
 * @memo 2022
 * on disable disable the sprite renderer
 */
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
    /**
 * @memo 2022
 * late update, handles changing the sprite for running so it looks like animaiton
 */
    private void LateUpdate()
    {
        run.enabled = movement.getIsRunning();
        if (movement.getIsJumping())
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.getIsTurning())
        {
            spriteRenderer.sprite = slide;
        }
        else if(!movement.getIsRunning())
        {
            spriteRenderer.sprite = idle;
        }
    }
}
