using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @memo 2022
 * Script that simulates animation based on sprites
 */
public class animationScript : MonoBehaviour
{
    public Sprite[] sprites;
    public float fps = 1f / 6f;//6 fps
    private SpriteRenderer spriteRenderer;
    private int frame;
    /**
 * @memo 2022
 * Awake method, sets the sprite renderer
 */
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /**
 * @memo 2022
 * on enable invoke animation method
 */
    private void OnEnable()
    {
        InvokeRepeating(nameof(anim), fps, fps);
    }
    /**
 * @memo 2022
 * on disable stop animation
 */
    private void OnDisable()
    {
        CancelInvoke();
    }
    /**
 * @memo 2022
 * cycles through sprites every defined intergval by fps so it looks like animation
 */
    private void anim()
    {
        frame++;
        if (frame >= sprites.Length)
        {
            frame = 0;
        }
        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }       
    }
}
