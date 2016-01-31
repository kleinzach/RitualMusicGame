using UnityEngine;
using System.Collections;
using System;

public class SpeedAdjustBead : Bead {

    public float speedAdjust = 2f;

    public GameObject speedUpSprite;
    public GameObject speedDownSprite;

    public override void OnBeat()
    {
        if ((Time.timeSinceLevelLoad - useTimer) > .5f)
        {
            //MusicManager.singleton.Speed += speedAdjust;
            ring.speed *= speedAdjust;
            speedAdjust = 1 / speedAdjust;
            useTimer = Time.timeSinceLevelLoad;
            health--;
            speedUpSprite.SetActive(speedAdjust > 1);
            speedDownSprite.SetActive(speedAdjust <= 1);
        }
    }

    public override void OnHit(float accuracy)
    {

    }

    public override void OnMiss()
    {

    }
}
