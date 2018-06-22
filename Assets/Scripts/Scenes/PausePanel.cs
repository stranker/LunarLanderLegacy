using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

    public GameObject inputPanel;

    private void Start()
    {
        OnInputClose();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnContinueClicked();
        }
    }

    public void OnContinueClicked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OnInputClicked()
    {
        inputPanel.SetActive(true);
    }

    public void OnMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void OnInputClose()
    {
        inputPanel.SetActive(false);
    }

}
