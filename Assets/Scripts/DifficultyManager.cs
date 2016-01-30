using UnityEngine;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour {

    public List<BeatRing> rings;

    float nextBead = 10f;

    public static bool readyForAnother = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (readyForAnother)
        {
            nextBead = 30f;
            readyForAnother = false;
        }
        nextBead -= Time.deltaTime;
        if (nextBead < 0f)
        {
            rings[0].addBead();
            nextBead = 1000f;
        }
	}
}
