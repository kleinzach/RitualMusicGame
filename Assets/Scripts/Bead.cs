﻿using UnityEngine;
using System.Collections;

public abstract class Bead : MonoBehaviour {

    public AudioClip hitClip;

    //Pointer to the ring this bead is on.
    public BeatRing ring;

    public KeyCode key;

    protected Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public abstract void OnHit(float accuracy);
    public abstract void OnMiss();
    public abstract void OnCenter();
}
