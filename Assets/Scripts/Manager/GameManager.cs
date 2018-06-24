using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public GameObject terrain;
    public int score;
    public float gameTime;

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
        player = GameObject.FindGameObjectWithTag("Player");
        terrain = GameObject.FindGameObjectWithTag("Terrain");
        player.transform.position = new Vector2(0,terrain.GetComponent<TerrainGenerator>().maxHighMountain + 1);
    }

    public static GameManager Get()
    {
        return instance;
    }
}
