using UnityEngine;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour {

    public List<BeatRing> rings;

    float nextBead = 10f;
    public float beadInterval = 10f;

    public static bool readyForAnother = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (readyForAnother)
        {
            nextBead = beadInterval;
            readyForAnother = false;
        }
        nextBead -= Time.deltaTime;
        if (nextBead < 0f)
        {
            rings[Random.Range(0,rings.Count-1)].addBead();
            nextBead = 1000f;
        }
	}
}
