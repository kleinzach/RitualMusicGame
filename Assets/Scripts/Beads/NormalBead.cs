using UnityEngine;
using System.Collections;

public class NormalBead : Bead {

    public override void OnBeat()
    {
        //targetScale = Vector3.one * .5f;
    }

    public override void OnHit(float accuracy)
    {
        if(rend)
            rend.color = Color.black;
        useTimer = .25f;
        ScoreManager.ScoreBead(this, accuracy);
        asource.PlayOneShot(clip,accuracy);
        MainVisualizer.Hit(accuracy);
        rend.color = hitColor;
        health--;
    }

    public override void OnMiss()
    {

    }
}
