using UnityEngine;
using System.Collections;
using System;

public class ReverseBead : Bead {
    
    
    public override void OnHit(float accuracy)
    {
        useTimer = Time.time;
    }

    public override void OnMiss()
    {

    }

    float lastCenter = 0f;

    public override void OnBeat()
    {
		if ((Time.time - useTimer) > .1f)
		{
			ring.NeedsReversing = true;
			useTimer = Time.time;
		}		
    }

	public void DecreaseHealth()
	{
		//I know this is ugly and contrary to the design, but it seems to work and fix the reverse bug
		health--;
	}
}
