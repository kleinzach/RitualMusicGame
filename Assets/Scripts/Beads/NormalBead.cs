using UnityEngine;
using System.Collections;

public class NormalBead : Bead {

    public override void OnBeat()
    {

    }

    public override void OnHit(float accuracy)
    {
        if(rend)
            rend.material.color = Color.black;
        useTimer = .1f;
        ScoreManager.ScoreBead(this, accuracy);
    }

    public override void OnMiss()
    {

    }
}
