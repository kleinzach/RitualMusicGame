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

    public Color hitColor = Color.black;

	// Use this for initialization
	void Start () {
        rend = GetComponentInChildren<SpriteRenderer>();
        asource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        useTimer -= Time.deltaTime;
        rend.color = Color.Lerp(rend.color, Color.gray, 1f * Time.deltaTime);
	}

    public abstract void OnHit(float accuracy);
    public abstract void OnMiss();
    public abstract void OnBeat();
}
