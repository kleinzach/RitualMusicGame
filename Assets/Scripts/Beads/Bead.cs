﻿using UnityEngine;
using System.Collections;

public abstract class Bead : MonoBehaviour {

    public AudioClip hitClip;

    //Pointer to the ring this bead is on.
    public BeatRing ring;

    public KeyCode key;

    protected Renderer rend;

    public float useTimer;

	// Use this for initialization
	void Start () {
        rend = GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        useTimer -= Time.deltaTime;
        rend.material.color = Color.Lerp(rend.material.color, Color.white, 10*Time.deltaTime);
	}

    public abstract void OnHit(float accuracy);
    public abstract void OnMiss();
    public abstract void OnBeat();
}