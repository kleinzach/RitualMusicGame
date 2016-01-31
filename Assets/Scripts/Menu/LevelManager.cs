using UnityEngine;

using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
     
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadLatestScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Last Scene"));
    }
}
