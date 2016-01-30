using UnityEngine;
using System.Collections;
using System;

public class SpeedAdjustBead : Bead {

    public float speedAdjust = .1f;

    public override void OnBeat()
    {
        MusicManager.singleton.speed += speedAdjust;
    }

    public override void OnHit(float accuracy)
    {

    }

    public override void OnMiss()
    {

    }
}
