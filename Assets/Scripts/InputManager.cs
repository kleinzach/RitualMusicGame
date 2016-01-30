using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
    public List<Bead> beadList;
    public List<KeyCode> keyList;
    public List<BeatRing> ringList;

    // Use this for initialization
    void Start () {
        foreach (Bead bead in beadList)
        {
            keyList.Add(bead.key);
        }
	}
	
	// Update is called once per frame
	void Update () {
        List<KeyCode> keyPressedList = new List<KeyCode>();
        foreach (KeyCode key in keyList)
        {
            if (Input.GetKeyDown(key))
            {
                keyPressedList.Add(key);
            }
                
        }
        foreach (BeatRing ring in ringList)
        {
            if (keyPressedList.Contains(ring.getCurrentKey()))
            {
                keyPressedList.Remove(ring.getCurrentKey());
                // score points
            }
        }
        foreach (KeyCode key in keyPressedList)
        {
            // lose points
        }
	}
}