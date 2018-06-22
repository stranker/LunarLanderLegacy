using UnityEngine;
using UnityEngine.SceneManagement;

public class InputScene : MonoBehaviour {

    public void OnContinueButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
