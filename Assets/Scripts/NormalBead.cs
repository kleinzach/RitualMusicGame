using UnityEngine;
using System.Collections;

public class NormalBead : Bead {

    public override void OnCenter()
    {

    }

    public override void OnHit(float accuracy)
    {
        if(rend)
            rend.material.color = Random.ColorHSV();
    }

    public override void OnMiss()
    {

    }
}
