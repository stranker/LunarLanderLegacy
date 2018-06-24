using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject creditsPanel;

    private void Start()
    {
        OnCloseCreditsButton();
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("InputScene");
    }

    public void OnCreditsButton()
    {
        creditsPanel.SetActive(true);
    }

    public void OnCloseCreditsButton()
    {
        creditsPanel.SetActive(false);
    }

    public void OnExitButton()
    {
        EditorApplication.isPaused = true;
        Application.Quit();
    }

}
