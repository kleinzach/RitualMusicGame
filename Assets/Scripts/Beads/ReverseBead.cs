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
            health--;
        }
    }
}
