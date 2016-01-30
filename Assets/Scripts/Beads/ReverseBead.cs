using UnityEngine;
using System.Collections;
using System;

public class ReverseBead : Bead {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public override void OnHit(float accuracy)
    {
        useTimer = 1f;
    }

    public override void OnMiss()
    {

    }

    float lastCenter = 0f;

    public override void OnBeat()
    {

    }
}
