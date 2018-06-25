using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public GameObject terrain;
    public int score = 0;
    public float gameTime;
    public int currentLevel = 1;
    public int remainingFuel;

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
        GetRunnableParameters();
    }

    private void GetRunnableParameters()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        terrain = GameObject.FindGameObjectWithTag("Terrain");
        player.transform.position = new Vector2(0, terrain.GetComponent<TerrainGenerator>().maxHighMountain + 1);
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
}
