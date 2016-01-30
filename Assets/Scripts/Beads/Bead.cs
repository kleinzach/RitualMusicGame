using UnityEngine;
using System.Collections;

public abstract class Bead : MonoBehaviour {

    public AudioClip hitClip;

    //Pointer to the ring this bead is on.
    public BeatRing ring;

    public KeyCode key;

    protected SpriteRenderer rend;

    public float useTimer;

    protected AudioSource asource;
    public AudioClip clip;
    
    public Color hitColor;
	public bool IsNext;

    public Vector3 targetScale = Vector3.one;

	// Use this for initialization
	void Start () {
        rend = GetComponentInChildren<SpriteRenderer>();
        asource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        useTimer -= Time.deltaTime;
		if (rend)
			rend.color = Color.Lerp(rend.material.color, Color.white, 10*Time.deltaTime);
        targetScale = Vector3.Lerp(targetScale, Vector3.one, Time.deltaTime);
	}

    public abstract void OnHit(float accuracy);
    public abstract void OnMiss();
    public abstract void OnBeat();
}
