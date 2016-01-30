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

    /// <summary>
    /// Initialization by:
    ///     Setting bead starting positions
    /// </summary>
	void Start () {
        RecalculateBeadPositions();
	}
	
	/// <summary>
    /// Once per frame:
    ///     Rotate to the correct angle.
    /// </summary>
	void Update () {
        transform.rotation = Quaternion.Euler(0, 0, (360/beadList.Count) * speed * (Time.time));
        currentBead = (int)(Time.time * speed) % beadList.Count;

	}

    void RecalculateBeadPositions()
    {
        for (int i = 0; i < beadList.Count; i++)
        {
            Bead b = beadList[i];
            float lamda = i * 2 * Mathf.PI / beadList.Count;
            if (b)
            {
                b.transform.parent = null;
                b.transform.localScale = Vector3.one;
                b.transform.localRotation = Quaternion.Euler(Vector3.zero);
                b.transform.parent = this.transform;
                b.transform.localPosition = new Vector3(Mathf.Cos(lamda), Mathf.Sin(lamda), 0) / 2f;
            }
        }
    }
}
