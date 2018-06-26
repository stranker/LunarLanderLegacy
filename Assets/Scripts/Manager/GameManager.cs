using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public GameObject terrain;
    public int score = 0;
    public float gameTime = 120;
    public float currentGameTime;
    public int currentLevel = 1;
    public int remainingFuel;
    public int scoreOnLanding = 150;
    public bool playing = false;
    private bool scoreAdded = false;
    public bool gameOver = false;
    public int initSpaceshipPosX = 10;
    private const int offsetSpaceshipY = 2;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start() {
        currentGameTime = gameTime;
        GetRunnableParameters();
    }

    private void Update()
    {
        TimeControl();
        PlayerControl();
    }

    private void PlayerControl()
    {
        if (player)
        {
            Spaceship p = player.GetComponent<Spaceship>();
            if (playing)
            {
                if (p.landed && !scoreAdded)
                {
                    scoreAdded = true;
                    AddScore();
                    playing = false;
                }
                if (p.fuel <= 0 && !p.GetAlive())
                {
                    gameOver = true;
                    playing = false;
                }
                if (!p.GetAlive() || p.IsLanded())
                {
                    remainingFuel = p.fuel;
                    playing = false;
                }
            }
        }
    }

    private void TimeControl()
    {
        if (playing)
        {
            currentGameTime = gameTime - Time.timeSinceLevelLoad;
            if (currentGameTime <= 0)
            {
                currentGameTime = 0;
                if (!gameOver)
                {
                    gameOver = true;
                }
            }
        }
    }


    private void GetRunnableParameters()
    {
        playing = true;
        scoreAdded = false;
        player = GameObject.FindGameObjectWithTag("Player");
        terrain = GameObject.FindGameObjectWithTag("Terrain");
        player.transform.position = new Vector2(initSpaceshipPosX, terrain.GetComponent<TerrainGenerator>().maxHighMountain + offsetSpaceshipY);
        if (currentLevel == 1)
        {
            remainingFuel = player.GetComponent<Spaceship>().fuel;
        }
        else
        {
            player.GetComponent<Spaceship>().fuel = remainingFuel;
        }
    }

    public static GameManager Get()
    {
        return instance;
    }

    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            GetRunnableParameters();
        }
    }

    public void AddScore()
    {
        score += scoreOnLanding;
    }

}
