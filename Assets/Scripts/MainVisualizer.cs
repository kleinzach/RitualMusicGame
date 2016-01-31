using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainVisualizer : MonoBehaviour {

    public static MainVisualizer singleton;

    public ParticleSystem circlePs;
    public ParticleSystem squarePs;

    public AudioSource source;
    public AudioClip missSound;

    public Image timer;

    // Use this for initialization
    void Start () {
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
        timer.fillAmount = 1 - ((float)(Time.timeSinceLevelLoad)) / ((float)(DifficultyManager.singleton.timeLimit));
        timer.color = Color.Lerp(Color.red, Color.green, timer.fillAmount);
	}

    public static void Hit(float accuracy)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[singleton.circlePs.particleCount];
        singleton.circlePs.GetParticles(particles);
        singleton.circlePs.SetParticles(particles, singleton.circlePs.particleCount);
        singleton.circlePs.Emit((int)( 10 * accuracy));
    }

    public static void Beat(float amount)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[singleton.squarePs.particleCount];
        singleton.squarePs.GetParticles(particles);
        singleton.squarePs.SetParticles(particles, singleton.squarePs.particleCount);
        singleton.squarePs.Emit((int)((ScoreManager.singleton.Combo+10) * amount));
    }

    public static void Miss()
    {
        Debug.Log("MISS " + ScoreManager.singleton.Combo);
        singleton.source.PlayOneShot(singleton.missSound,ScoreManager.singleton.Combo / 100f);
    }

    public static void PlaySound(AudioClip clip, float volume)
    {
        singleton.source.PlayOneShot(clip,volume);
    }
}
