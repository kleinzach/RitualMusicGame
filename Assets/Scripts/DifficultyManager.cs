using UnityEngine;
using System.Collections.Generic;

public class DifficultyManager : MonoBehaviour {

    public static DifficultyManager singleton;

    public List<BeatRing> rings;

    float nextBead = 10f;
    public float beadInterval = 10f;
    public float longBeadInterval = 100f;

    public static bool readyForAnother = false;

    public float startSpeed;
    public float speedPerSecond;

	// Use this for initialization
	void Start () {
        singleton = this;
        nextBead = beadInterval;
        MusicManager.singleton.Speed = startSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (MusicManager.singleton.IsBeat)
		{
			MusicManager.singleton.Speed = startSpeed + Time.time * speedPerSecond;
		}
        if (readyForAnother)
        {
            nextBead = Mathf.Min(nextBead,beadInterval);
            readyForAnother = false;
        }
        nextBead -= Time.deltaTime;
        if (nextBead < 0f)
        {
            rings[Random.Range(0,rings.Count)].addBead();
            nextBead = longBeadInterval;
        }
	}


    [System.Serializable]
    public struct BeadFrequency
    {
        public Bead bead;
        public float frequency;
    }

    public BeadFrequency[] beadPossibilities;

    /// <summary>
    /// Generate a random bead based on the frequencies.
    /// </summary>
    /// <returns></returns>
    public static Bead generateRandomBead()
    {
        Bead b = singleton.beadPossibilities[0].bead;

        float f = Random.value;

        foreach (BeadFrequency bf in singleton.beadPossibilities)
        {
            f -= bf.frequency;
            if (f <= 0)
            {
                b = bf.bead;
                break;
            }
        }

        return b;
    }
}
