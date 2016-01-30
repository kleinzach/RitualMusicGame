using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager singleton;

    float _score;
    public float score
    {
        get { return _score; }
        set {
            _score = value;
            if(ComboText)
                ComboText.text = score + "";
        }
    }

    public float accuracyCutoff;

    public Text ComboText;

	// Use this for initialization
	void Start () {
        score = 0;
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void ScoreBead(Bead b, float accuracy)
    {
        if(accuracy > singleton.accuracyCutoff)
        {
            singleton.score++;
        }
        else
        {
            singleton.score = 0f;
        }
    }
    public static void Miss()
    {
        singleton.score = 0f;
    }
}
