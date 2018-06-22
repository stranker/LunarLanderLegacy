using UnityEngine;
using UnityEngine.SceneManagement;

public class InputScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
