using UnityEngine;
using System.Collections;
using System;

public class SpeedAdjustBead : Bead {

    public float speedAdjust = 2f;

    public override void OnBeat()
    {
        if ((Time.time - useTimer) > .5f)
        {
            //MusicManager.singleton.Speed += speedAdjust;
            ring.speed *= speedAdjust;
            speedAdjust = 1 / speedAdjust;
            useTimer = Time.time;
            Debug.Log("BEAT");
        }
    }

    public override void OnHit(float accuracy)
    {

    }

    public override void OnMiss()
    {

    }
}
