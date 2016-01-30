using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum BeadEnum
{
	None,
	Red,
	Green,
	Blue,
	Yellow
}

/// <summary>
/// One of the concentric rings.
/// Spins at a fixed rate, with beads on it which demand input.
/// Takes a list of a certain size which determines the movement speed.
/// </summary>
public class BeatRing : MonoBehaviour {

    Renderer rend;

    public float time = 1000;

    //A speed scalar for the spin rate of this ring.
    public float speed;

	//Enums defining Beads in ring
	public List<BeadEnum> beadEnumList;

    //The closest bead on this ring.
    public int currentBeadIndex = 0;
    public Bead currentBead;

    //How close to accurate a button press would be on this frame.
    public float frameAccuracy = 0;

	//Prefabs
	public GameObject RedBeadPrefab;
	public GameObject BlueBeadPrefab;
	public GameObject YellowBeadPrefab;
	public GameObject GreenBeadPrefab;

    //A list of all beads around this ring, blank spaces representing no input required.
    private List<Bead> beadList;

	//The scene's MusicManager.  Will provides its own deltaTime and IsBeat
	private MusicManager musicManager;

	/// <summary>
	/// Initialization by:
	///     Setting bead starting positions
	/// </summary>
	void Start () {

		//Build beadList
		beadList = new List<Bead>();
		foreach (var beadEnum in beadEnumList)
		{
			switch (beadEnum)
			{
				case BeadEnum.None:
					beadList.Add(null);
					break;
				case BeadEnum.Red:
					GameObject redGo = GameObject.Instantiate(RedBeadPrefab);
					beadList.Add(redGo.GetComponent<Bead>());
					break;
				case BeadEnum.Green:
					GameObject greenGo = GameObject.Instantiate(GreenBeadPrefab);
					beadList.Add(greenGo.GetComponent<Bead>());
					break;
				case BeadEnum.Blue:
					GameObject blueGo = GameObject.Instantiate(BlueBeadPrefab);
					beadList.Add(blueGo.GetComponent<Bead>());
					break;
				case BeadEnum.Yellow:
					GameObject yellowGo = GameObject.Instantiate(YellowBeadPrefab);
					beadList.Add(yellowGo.GetComponent<Bead>());
					break;
				default:
					break;
			}
		}

		rend = GetComponent<Renderer>();
        RecalculateBeadPositions();
		musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
	}

    internal void addBead()
    {
        GameObject redGo = GameObject.Instantiate(RedBeadPrefab);
        beadList.Add(redGo.GetComponent<Bead>());
    }

    bool beadAlreadyHit;
    bool pastCenter;
    int lastBead;
	
	/// <summary>
    /// Once per frame:
    ///     Rotate to the correct angle.
    /// </summary>
	void Update () {
        //Increment the local time by the current speed
        time += speed * musicManager.DeltaTime;

        transform.rotation = Quaternion.Euler(0, 0, (360/beadList.Count) * (time));
        
        //Calculate how close to the beat this frame is.
        frameAccuracy = Mathf.Abs(time % 1);
        
        if(frameAccuracy < .5f)
        {
            pastCenter = false;
            currentBeadIndex = (int)(time) % beadList.Count;
        }
        else
        {
            if (!pastCenter && currentBead && .5f - frameAccuracy < .1f && lastBead == currentBeadIndex)
            {
                currentBead.OnBeat();
                rend.material.color = Color.black;
            }
            pastCenter = true;
            currentBeadIndex = (int)(time+1) % beadList.Count;
        }

        currentBead = beadList[currentBeadIndex];
        
        frameAccuracy = 2*Mathf.Abs(.5f - frameAccuracy);

        //Reposition the beads.
        RecalculateBeadPositions();

        //Debug code to adjust color based on the beat
        rend.material.color = Color.Lerp(rend.material.color, Color.white, Time.deltaTime * 10f);
        rend.material.color = frameAccuracy > .9f ? Color.black : rend.material.color;
        lastBead = currentBeadIndex;
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
                b.ring = this;

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
        if (currentBead && currentBead.useTimer < 0f)
        {
            return currentBead.key;
        }
        else
        {
            return KeyCode.None;
        }
    }

    public void Score()
    {
        currentBead.OnHit(frameAccuracy);
        Debug.Log(frameAccuracy);
    }
}
