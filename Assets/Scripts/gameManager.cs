using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    private int world;
    private int stage;
    private int lives;
    private int coins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
       
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    void Start()
    {
        NewGame();
    }
    private void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLevel(1, 1);
    }
    private void LoadLevel(int w, int s)
    {
        world = w;
        stage = s;
        SceneManager.LoadScene($"{world}-{stage}");
    }
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
    private void GameOver()
    {
        //for now just restart game after 3sec
        Invoke(nameof(NewGame), 3f);
    }
    public void onDie(float delay)
    {
        Invoke(nameof(onDie), delay);
    }
    public void NextLevel()
    {
        LoadLevel(world, stage + 1);//if full game remaking then should have logic as follows
        /**
         * if(world==1&&stage==10)
         * {
         * LoadLevel(world+1,1);
         * }
         * etc...
         */
    }
}
