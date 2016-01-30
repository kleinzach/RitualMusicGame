using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class MainVisualizer : MonoBehaviour {

    public static MainVisualizer singleton;

    public ParticleSystem circlePs;
    public ParticleSystem squarePs;

    // Use this for initialization
    void Start () {
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void Hit(float accuracy)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[singleton.circlePs.particleCount];
        singleton.circlePs.GetParticles(particles);
        singleton.circlePs.SetParticles(particles, singleton.circlePs.particleCount);
        singleton.circlePs.Emit((int)( 25 * accuracy));
    }

    public static void Beat(float amount)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[singleton.squarePs.particleCount];
        singleton.squarePs.GetParticles(particles);
        singleton.squarePs.SetParticles(particles, singleton.squarePs.particleCount);
        singleton.squarePs.Emit((int)(ScoreManager.singleton.score * 5 * amount));
    }
}
