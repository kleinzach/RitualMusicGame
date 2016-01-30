using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

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
        score = 0;
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
        float l1 = 1, l2 = 0, l3 = 0, l4 = 0;
        if(0 < score && score < scoreBreak1)
        {
            l1 = Mathf.Lerp(1,0, Mathf.InverseLerp(0, scoreBreak1, score));
            l2 = Mathf.Lerp(0,1, Mathf.InverseLerp(0, scoreBreak1, score));
            l3 = 0;
            l4 = 0;
        }
        if (scoreBreak1 <= score && score < scoreBreak2)
        {
            l1 = 0;
            l2 = Mathf.Lerp(1, 0, Mathf.InverseLerp(scoreBreak1, scoreBreak2, score));
            l3 = Mathf.Lerp(0, 1, Mathf.InverseLerp(scoreBreak1, scoreBreak2, score));
            l4 = 0;
        }

        if (scoreBreak2 <= score && score < scoreBreak3)
        {
            l1 = 0;
            l2 = 0;
            l3 = Mathf.Lerp(1, 0, Mathf.InverseLerp(scoreBreak2, scoreBreak3, score));
            l4 = Mathf.Lerp(0, 1, Mathf.InverseLerp(scoreBreak2, scoreBreak3, score));

        }
        if(score >= scoreBreak3)
        {
            l1 = 0; l2 = 0; l3 = 0; l4 = 1;
        }

        mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { snap1, snap2, snap3, snap4 }, new float[] { l1,l2,l3,l4 }, .1f);
	}

    public static void ScoreBead(Bead b, float accuracy)
    {
        if(accuracy > singleton.accuracyCutoff)
        {
            singleton.score++;
            if (singleton.score % 5 == 0)
            {
                b.ring.addBead();
            }
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
