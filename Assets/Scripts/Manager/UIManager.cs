using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject pausePanel;
    public Text score, time, fuel, altitude, horiontalSpeed, verticalSpeed;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPauseButton();
        }
    }

    public void OnPauseButton()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
