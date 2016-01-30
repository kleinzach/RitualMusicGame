using UnityEngine;
using System.Collections;

public class NormalBead : Bead {

    public override void OnBeat()
    {

    }

    public override void OnHit(float accuracy)
    {
        rend.color = hitColor;
        useTimer = .25f;
        ScoreManager.ScoreBead(this, accuracy);
        asource.PlayOneShot(clip,accuracy);
        MainVisualizer.Hit(accuracy);
    }

    public override void OnMiss()
    {

    }
}
