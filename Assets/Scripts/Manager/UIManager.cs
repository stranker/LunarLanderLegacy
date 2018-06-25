using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject landingPanel;
    public Text scoreText;
    public Text timeText;
    public Text fuelText;
    public Text altitudeText;
    public Text horizontalSpeedText;
    public Text verticalSpeedText;
    public Text resultText;
    public Text quantityText;
    public int score, fuel, horizontalSpeed, verticalSpeed;
    public int altitude;
    public float time;

    private void Start()
    {
        pausePanel.SetActive(false);
        landingPanel.SetActive(false);
        scoreText.text = "SCORE 0000";
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPauseButton();
        }
        SetTexts();
        ShowOptionsOnPlayerCollision();
    }

    private void ShowOptionsOnPlayerCollision()
    {
        Spaceship player = GameManager.Get().player.GetComponent<Spaceship>();
        if (!player.GetAlive() && !landingPanel.activeInHierarchy)
        {
            landingPanel.SetActive(true);
            resultText.text = "FUEL TANK DESTROYED!";
            quantityText.text = "YOU LOST " + player.GetFuelDecreaseOnDeath() + " UNITS OF FUEL";
        }
    }

    public void OnPauseButton()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void SetTexts()
    {
        GameManager gm = GameManager.Get();
        Spaceship player = gm.player.GetComponent<Spaceship>();
        if (score != gm.score)
        {
            score = gm.score;
            scoreText.text = "SCORE " + score.ToString("0000");
        }
        if (fuel != player.fuel)
        {
            fuel = player.fuel;
            fuelText.text = "FUEL " + fuel.ToString("0000");
        }
        if (verticalSpeed != (int)player.velocity.y)
        {
            verticalSpeed = (int)player.velocity.y;
            verticalSpeedText.text = "VERTICAL SPEED " + verticalSpeed.ToString();
        }
        if (horizontalSpeed != (int)player.velocity.x)
        {
            horizontalSpeed = (int)player.velocity.x;
            horizontalSpeedText.text = "HORIZONTAL SPEED " + horizontalSpeed.ToString();
        }
        if (altitude != (int)player.altitude)
        {
            altitude = (int)player.altitude;
            altitudeText.text = "ALTITUDE " + altitude.ToString();
        }
        if (player.GetAlive())
        {
            time = gm.gameTime - Time.time;
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("00");
            timeText.text = minutes + ":" + seconds;
        }
    }

    public void OnContinueButton()
    {
        GameManager.Get().NextLevel();
    }

}
