using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// One of the concentric rings.
/// Spins at a fixed rate, with beads on it which demand input.
/// Takes a list of a certain size which determines the movement speed.
/// </summary>
public class BeatRing : MonoBehaviour {

    Renderer rend;

    float time;

    //A speed scalar for the spin rate of this ring.
    public float speed;

    //A list of all beads around this ring, blank spaces representing no input required.
    public List<Bead> beadList;

    //The closest bead on this ring.
    public int currentBeadIndex = 0;
    public Bead currentBead;

    //How close to accurate a button press would be on this frame.
    public float frameAccuracy = 0;

    /// <summary>
    /// Initialization by:
    ///     Setting bead starting positions
    /// </summary>
	void Start () {
        rend = GetComponent<Renderer>();
        RecalculateBeadPositions();
	}

    bool beadAlreadyHit;
	
	/// <summary>
    /// Once per frame:
    ///     Rotate to the correct angle.
    /// </summary>
	void Update () {
        //Increment the local time by the current speed
        time += speed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 0, (360/beadList.Count) * time);
        
        //Calculate how close to the beat this frame is.
        frameAccuracy = Mathf.Abs(time % 1);

        currentBeadIndex = frameAccuracy < .5f ? (int)(time) % beadList.Count : (int)(time + 1) % beadList.Count;
        currentBead = beadList[currentBeadIndex];
        
        frameAccuracy = 2*Mathf.Abs(.5f - frameAccuracy);

        //Reposition the beads.
        RecalculateBeadPositions();

        //Debug code to adjust color based on the beat
        rend.material.color = frameAccuracy > .9f ? new Color(1, 1, 1) : new Color(.5f, .5f, .5f);

    }

    /// <summary>
    /// Places the beads around the circle,
    ///     Calculates the angle by dividing pi by the length of the bead list, then multiplies by the bead's position in the list.
    /// </summary>
    void RecalculateBeadPositions()
    {
        //For each bead...
        for (int i = 0; i < beadList.Count; i++)
        {
            //The bead (or null) at this position in the list.
            Bead b = beadList[i];

            if (b)
            {
                //Calculate the angle around the circle,
                float lamda = -i * 2 * Mathf.PI / beadList.Count;

                //Set the global scale and rotation to default values.
                b.transform.parent = null;
                b.transform.localScale = Vector3.one;
                b.transform.localRotation = Quaternion.Euler(Vector3.zero);

                //Reparent and set the position to the edge of the circle at the correct angle.
                b.transform.parent = this.transform;
                b.transform.localPosition = new Vector3(Mathf.Cos(lamda), Mathf.Sin(lamda), 0) / 2f;
            }
        }
    }

    public KeyCode getCurrentKey()
    {
        if (currentBead)
        {
            return currentBead.key;
        }
        else
        {
            return KeyCode.None;
        }
    }
}
