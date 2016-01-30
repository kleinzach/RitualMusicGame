using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class MainVisualizer : MonoBehaviour {

    public static MainVisualizer singleton;

    ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        singleton = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void Hit(float accuracy)
    {
        singleton.ps.SetParticles(null, 0);
        singleton.ps.Emit((int)(100 * accuracy));
    }
}
