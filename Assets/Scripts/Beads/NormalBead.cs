using UnityEngine;
using System.Collections;

public class NormalBead : Bead {

    public override void OnBeat()
    {

    }

    public override void OnHit(float accuracy)
    {
        if(rend)
            rend.color = Color.black;
        useTimer = .25f;
        ScoreManager.ScoreBead(this, accuracy);
        asource.PlayOneShot(clip,accuracy);
        if (particles)
        {
            particles.SetParticles(null, 0);
            particles.Emit((int)(100 * accuracy));
        }
    }

    public override void OnMiss()
    {

    }
}
