using UnityEngine;
using System.Collections;

public class NormalBead : Bead {

    public override void OnBeat()
    {
        //targetScale = Vector3.one * .5f;
    }

    public override void OnHit(float accuracy)
    {
        rend.color = hitColor;
        Debug.Log("hit");
        useTimer = .25f;
        ScoreManager.ScoreBead(this, accuracy);
        MainVisualizer.PlaySound(clip,accuracy);
        MainVisualizer.Hit(accuracy);
        rend.color = hitColor;
        health--;
    }

    public override void OnMiss()
    {
        targetScale = Vector3.one * 1.25f;
        Debug.Log("Miss");
        ScoreManager.Miss();
    }
}
