using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// One of the concentric rings.
/// Spins at a fixed rate, with beads on it which demand input.
/// Takes a list of a certain size which determines the movement speed.
/// </summary>
public class BeatRing : MonoBehaviour {

    public float speed;

    public List<Bead> beadList;

    public int currentBead = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	/// <summary>
    /// Once per frame:
    ///     Rotate to the correct angle.
    /// </summary>
	void Update () {
        transform.rotation = Quaternion.Euler(0, 0, (360/beadList.Count) * speed * (Time.time));
        currentBead = (int)(Time.time * speed) % beadList.Count;

	}
}
