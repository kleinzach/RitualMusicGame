using UnityEngine;
using System.Collections;
using System;

public class SpeedAdjustBead : Bead {

    public float speedAdjust = .1f;

    public override void OnBeat()
    {
        MusicManager.singleton.Speed += speedAdjust;
        Debug.Log(MusicManager.singleton.Speed);
    }

    public override void OnHit(float accuracy)
    {

    }

    public override void OnMiss()
    {

    }
}
