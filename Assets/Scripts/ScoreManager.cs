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
    public AudioMixerSnapshot snap2;
    public AudioMixerSnapshot snap3;
    public AudioMixerSnapshot snap4;

    // Use this for initialization
    void Start () {
        score = 0;
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
        mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { snap1, snap2, snap3, snap3 }, new float[] { 1, 100 * score / 10, 100 * (score-10) / 20, 100 * (score-20) / 30 }, .1f);
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
