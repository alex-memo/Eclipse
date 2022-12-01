using UnityEngine;
using UnityEngine.SceneManagement;
/**
 * @memo 2022
 * Game manager script
 */
public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    private int world;
    private int stage;
    private int lives;
    private int coins;

    public item fireFlower;

    private soundManager soundManager;
    private AudioSource source;
    /**
 * @memo 2022
 * On awake, create instance of game manager if there is alr on then destroy
 */
    private void Awake()
    {
        if (instance == null)
        {
            Application.targetFrameRate = 60;//sets the fps to 60
            instance = this;
            soundManager = GetComponent<soundManager>();
            source = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    /**
 * @memo 2022
 * on destroy if this was the instance the instance is now null
 */
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    /**
 * @memo 2022
 * on start creates a new game
 */
    void Start()
    {        
        NewGame();
    }
    /**
 * @memo 2022
 * on new game resets variebles and sets world to 1-1
 */
    private void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLevel(1, 1);
    }
    /**
 * @memo 2022
 * loads the received world
 */
    private void LoadLevel(int w, int s)
    {
        world = w;
        stage = s;
        SceneManager.LoadScene($"{world}-{stage}");
        if (!$"{world}-{stage}".Equals("1-2"))
        {
            Play(soundManager.overworld);
        }
        else
        {
            Play(soundManager.underworld);
        }
    }
    /**
 * @memo 2022
 * on die decrease lives by 1 and reset current level if game no lives then game over
 */
    public void onDie()
    {
        lives--;
        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }
    /**
 * @memo 2022
 * game over, resets the game
 */
    private void GameOver()
    {
        //for now just restart game after 3sec
        Play(soundManager.lose);
        Invoke(nameof(NewGame), 7f);
    }
    /**
 * @memo 2022
 * on die but receives a delay to reset the game
 */
    public void onDie(float delay)//resets the level basically
    {
        if (lives > 1)
        {
            Invoke(nameof(onDie), delay);
        }
        else
        {
            Invoke(nameof(onDie),0);
        }        
    }
    /**
     * @memo 2022
     * gets the next level and loads it
     */
    public void NextLevel()
    {
        LoadLevel(1, 1);
        //LoadLevel(world, stage + 1);//if full game remaking then should have logic as follows
        /**
         * if(world==1&&stage==10)
         * {
         * LoadLevel(world+1,1);
         * }
         * etc...
         */
    }
    /**
* @memo 2022
* adds a coin to player
*/
    public void addCoin()
    {
        Controller.instance.Play(soundManager.coin);
        coins++;
        if (coins == 100)
        {
            coins = 0;
            addLife();
        }
    }
    /**
* @memo 2022
* adds life to player
*/
    public void addLife()
    {
        Controller.instance.Play(soundManager.oneUp);
        lives++;
    }
    /**
     * @memo 2022
     * getter for soundmanager
     */
    public soundManager getSoundManager()
    {
        return soundManager;
    }
    /**
     * @memo 2022
     * plays the set clip
     */
    public void Play(AudioClip clip)
    {
        if (clip != null)
        {
            source.clip = clip;
            source.Play();
        }        
    }
    /**
 * @memo 2022
 * plays the win clip
 */
    public void PlayWin()
    {
        if (soundManager.win != null)
        {
            source.clip = null;
            //source.Play();
            source.PlayOneShot(soundManager.win);
        }
    }
}
