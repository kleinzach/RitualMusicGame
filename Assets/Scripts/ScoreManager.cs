using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager singleton;

    float _score;
    public float Score
    {
        get { return _score; }
        set {
            _score = value;
            if(ScoreText)
                ScoreText.text = Score + "";
        }
    }

    float _combo;
    public float Combo
    {
        get { return _combo; }
        set
        {
            _combo = value;
            if (ComboText)
                ComboText.text = Combo + "";
        }
    }

    public float accuracyCutoff;

    public Text ComboText;
    public Text ScoreText;
    public AudioMixer mixer;
    public AudioMixerSnapshot snap1;
    public int scoreBreak1 = 10;
    public AudioMixerSnapshot snap2;
    public int scoreBreak2 = 30;
    public AudioMixerSnapshot snap3;
    public int scoreBreak3 = 50;
    public AudioMixerSnapshot snap4;

    // Use this for initialization
    void Start () {
        Score = 0;
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
        float l1 = 1, l2 = 0, l3 = 0, l4 = 0;
        if(0 < Combo && Combo < scoreBreak1)
        {
            l1 = Mathf.Lerp(1,0, Mathf.InverseLerp(0, scoreBreak1, Combo));
            l2 = Mathf.Lerp(0,1, Mathf.InverseLerp(0, scoreBreak1, Combo));
            l3 = 0;
            l4 = 0;
        }
        if (scoreBreak1 <= Combo && Combo < scoreBreak2)
        {
            l1 = 0;
            l2 = Mathf.Lerp(1, 0, Mathf.InverseLerp(scoreBreak1, scoreBreak2, Combo));
            l3 = Mathf.Lerp(0, 1, Mathf.InverseLerp(scoreBreak1, scoreBreak2, Combo));
            l4 = 0;
        }

        if (scoreBreak2 <= Combo && Combo < scoreBreak3)
        {
            l1 = 0;
            l2 = 0;
            l3 = Mathf.Lerp(1, 0, Mathf.InverseLerp(scoreBreak2, scoreBreak3, Combo));
            l4 = Mathf.Lerp(0, 1, Mathf.InverseLerp(scoreBreak2, scoreBreak3, Combo));

        }
        if(Combo >= scoreBreak3)
        {
            l1 = 0; l2 = 0; l3 = 0; l4 = 1;
        }

        mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { snap1, snap2, snap3, snap4 }, new float[] { l1,l2,l3,l4 }, .1f);
	}

    public static void ScoreBead(Bead b, float accuracy)
    {
        if(accuracy > singleton.accuracyCutoff)
        {
            singleton.Combo++;
            singleton.Score += singleton.Combo;
        }
        else
        {
            singleton.Combo = 0f;
        }
    }
    public static void Miss()
    {
        singleton.Combo = 0f;
    }
}
