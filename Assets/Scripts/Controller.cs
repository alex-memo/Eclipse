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
    private spriteRendererScript fireRenderer;

    private deathAnimation deathAnimation;
    private bool big => bigRenderer.enabled;
    private bool fire => fireRenderer.enabled;
    private bool isDead => deathAnimation.enabled;

    private spriteRendererScript activeRenderer;
    private string size = "small";
    private CapsuleCollider2D capsuleColl;
    private bool isStar;

    private AudioSource source;
    private soundManager soundManager; 

    private bool isFireFlower=false;
    [SerializeField]
    private GameObject fireballPrefab;    
    private Transform attackPoint;
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
        fireRenderer = transform.GetChild(2).GetComponent<spriteRendererScript>();
        attackPoint = transform.GetChild(3);
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
        if (fire)
        {
            toBig();
        }
        else if (big)
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
        activeRenderer = smallRenderer;
        size = "small";
        capsuleColl.size = new Vector2(.75f, 1);
        capsuleColl.offset = new Vector2(0, 0);
        StartCoroutine(scaleAnim(bigRenderer, smallRenderer));
        Play(soundManager.shrink);
    }
    /**
     * @memo 2022
     * if player gets set to big as hit on fire
     */
    private void toBig()
    {
        isFireFlower = false;
        activeRenderer = bigRenderer;
        size = "big";
        capsuleColl.size = new Vector2(.75f, 2);
        capsuleColl.offset = new Vector2(0, .5f);
        StartCoroutine(scaleAnim(fireRenderer, bigRenderer));
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
        if(isFireFlower||size.Equals("fire"))
        {
            gameManager.instance.addCoin();
            return;
        }
        activeRenderer = bigRenderer;
        size = "big";
        capsuleColl.size = new Vector2(.75f, 2);
        capsuleColl.offset = new Vector2(0, .5f);
        StartCoroutine(scaleAnim(smallRenderer, bigRenderer));
        Play(soundManager.grow);
    }
    /**
* @memo 2022
* player grow
*/
    private void setFireFlower()
    {
        activeRenderer = fireRenderer;
        size = "fire";
        capsuleColl.size = new Vector2(.75f, 2);
        capsuleColl.offset = new Vector2(0, .5f);
        StartCoroutine(scaleAnim(bigRenderer,fireRenderer));
        Play(soundManager.grow);
    }
    /**
* @memo 2022
* animation for when growing player, shifts between a and b sprites
*/
    private IEnumerator scaleAnim(spriteRendererScript A, spriteRendererScript B)
    {
        float timer = 0;
        float duration = .5f;
        while (timer < duration)
        {
            if (Time.frameCount % 4 == 0)
            {
                A.enabled = !A.enabled;
                B.enabled = !A.enabled;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        A.enabled = false;
        B.enabled = false;
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
* activate star item
*/
    public void fireFlower()
    {
        StartCoroutine(firePlayer());
    }
    /**
* @memo 2022
* star animation
*/
    private IEnumerator starAnim(float duration)
    {
        gameManager.instance.Play(soundManager.star);
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
        gameManager.instance.Play(soundManager.overworld);
    }
    /**
* @memo 2022
* fire player, while player has fire power then do this
*/
    private IEnumerator firePlayer()
    {   
        setFireFlower();
        isFireFlower = true;
        float timer = 0;
        float cooldown = 1;
        bool canShoot = true;
        while (isFireFlower)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)&&canShoot)
            {
                shootFire();
                canShoot = false;
            }
            if (!canShoot)
            {
                timer += Time.deltaTime;
                if(timer>=cooldown)
                {
                    canShoot = true;
                    timer = 0;
                }
            }
            yield return null;
        }
        activeRenderer.getSpriteRenderer().color = Color.white;
        isStar = false;
    }
    /**
     * @memo 2022
     * shoots fireball
     */
    private void shootFire()
    {
        GameObject tempShot = Instantiate(fireballPrefab, attackPoint);
        tempShot.transform.parent = null;
        Play(soundManager.fireball);
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
