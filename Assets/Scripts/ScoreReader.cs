using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreReader : MonoBehaviour {

    public Text textbox;

    public int score;
    public string difficulty;
    public int maxCombo;

	// Use this for initialization
	void Start () {
        score = PlayerPrefs.GetInt("Score", 0);
        difficulty = PlayerPrefs.GetString("Difficulty", "Easy");
        maxCombo = PlayerPrefs.GetInt("Max Combo", 0);
        textbox.text = "Score: " + score + "\nMax Combo: " + maxCombo + "\nDifficulty: " + difficulty;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
