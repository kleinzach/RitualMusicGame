using UnityEngine;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour {

    public List<BeatRing> rings;

    float nextBead = 10f;
    public float beadInterval = 10f;

    public static bool readyForAnother = false;

    public float startSpeed;
    public float speedPerSecond;

	// Use this for initialization
	void Start () {
        nextBead = beadInterval;
        MusicManager.singleton.Speed = startSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MusicManager.singleton.Speed = startSpeed + Time.time * speedPerSecond;
        if (readyForAnother)
        {
            nextBead = beadInterval;
            readyForAnother = false;
        }
        nextBead -= Time.deltaTime;
        if (nextBead < 0f)
        {
            rings[Random.Range(0,rings.Count)].addBead();
            nextBead = 1000f;
        }
	}
}
