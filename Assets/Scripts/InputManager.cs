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
        List<KeyCode> keyPressedLeftList = new List<KeyCode>();
        foreach (KeyCode key in keyList)
        {
            if (key != KeyCode.None && Input.GetKeyDown(key))
            {
                keyPressedList.Add(key);
            }

        }
        keyPressedLeftList = new List<KeyCode>( keyPressedList);

        //For each of the rings in the scene...
        foreach (BeatRing ring in ringList)
        {
            //Check to see if a key pressed cooresponds to the ring's current bead.
            if (keyPressedList.Contains(ring.getCurrentKey()))
            {
                if (keyPressedLeftList.Contains(ring.getCurrentKey()))
                {
                    keyPressedLeftList.Remove(ring.getCurrentKey());
                }
                Debug.Log("SCore");
                ring.Score();
                // score points
            }
        }
        //Send a fail signal for each key that was pressed and wasnt an input.
        foreach (KeyCode key in keyPressedLeftList)
        {
            ScoreManager.Miss();
            Debug.Log("Fail! " + Time.time);
        }
	}
}