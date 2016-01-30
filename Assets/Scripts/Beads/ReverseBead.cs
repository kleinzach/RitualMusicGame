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
        if(Time.time > lastCenter + 1f)
        {
            ring.speed = -ring.speed;
            lastCenter = Time.time;
            //ring.time += Mathf.Sign(ring.speed) * 2 * (Time.time % 1);
        }
    }
}
