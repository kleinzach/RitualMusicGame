using UnityEngine;
using System.Collections;

public class Bead : MonoBehaviour {

    public AudioClip hitClip;

    //Pointer to the ring this bead is on.
    public BeatRing ring;

    public KeyCode key;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnHit(float accuracy)
    {

    }
    public void OnMiss()
    {

    }

}
