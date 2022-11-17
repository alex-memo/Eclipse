using System.Collections;
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
    private bool isDead => deathAnimation.enabled;

    private spriteRendererScript activeRenderer;
    private string size = "small";
    private CapsuleCollider2D capsuleColl;
    private bool isStar;

    private AudioSource source;
    private soundManager soundManager; 
    /**
     * @memo 2022
     * start method, sets the sound manager
     */
    private void Start()
    {
        soundManager = gameManager.instance.getSoundManager();
    }
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
        capsuleColl = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer;
        size = "small";
        source = GetComponent<AudioSource>();
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
        if (isStar || isDead)
        {
            return;
        }
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
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;
        size = "small";
        capsuleColl.size = new Vector2(.75f, 1);
        capsuleColl.offset = new Vector2(0, 0);
        StartCoroutine(scaleAnim());
        Play(soundManager.shrink);
    }
    /**
 * @memo 2022
 * when die do this
 */
    public void die()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        deathAnimation.enabled = true;
        Play(soundManager.die);
        gameManager.instance.onDie(3f);
    }
    /**
* @memo 2022
* player grow
*/
    public void grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;
        size = "big";
        capsuleColl.size = new Vector2(.75f, 2);
        capsuleColl.offset = new Vector2(0, .5f);
        StartCoroutine(scaleAnim());
        Play(soundManager.grow);
    }
    /**
* @memo 2022
* animation for when growing player
*/
    private IEnumerator scaleAnim()
    {
        float timer = 0;
        float duration = .5f;
        while (timer < duration)
        {
            if (Time.frameCount % 4 == 0)
            {
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !smallRenderer.enabled;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }
    /**
* @memo 2022
* activate star item
*/
    public void star(float duration = 10)
    {
        StartCoroutine(starAnim(duration));
    }
    /**
* @memo 2022
* star animation
*/
    private IEnumerator starAnim(float duration)
    {
        isStar = true;
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                activeRenderer.getSpriteRenderer().color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
            }
            yield return null;
        }
        activeRenderer.getSpriteRenderer().color = Color.white;
        isStar = false;
    }
    /**
* @memo 2022
* getter for is star
*/
    public bool getIsStar()
    {
        return isStar;
    }
    /**
     * @memo 2022
     * getter for size
     */
    public string getSize()
    {
        return size;
    }
    /**
     * @memo 2022
     * plays a break sound
     */
    public void playBreakSound()
    {
        if (soundManager.breakSounds.Length > 0)
        {
            int rand = Random.Range(0, soundManager.breakSounds.Length-1);
            source.PlayOneShot(soundManager.breakSounds[rand]);
        }       
    }
/**
 * @memo 2022
 * plays a set audio clip
 */
    public void Play(AudioClip clip)
    {
        if (clip != null)
        {
            source.PlayOneShot(clip);
        }
    }
}
