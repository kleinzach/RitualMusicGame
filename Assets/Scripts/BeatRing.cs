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
	Yellow,
	Reverse
}

/// <summary>
/// One of the concentric rings.
/// Spins at a fixed rate, with beads on it which demand input.
/// Takes a list of a certain size which determines the movement speed.
/// </summary>
public class BeatRing : MonoBehaviour
{

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
	public GameObject ReverseBeadPrefab;

	//Reverse on next beat
	public bool NeedsReversing;

	//A list of all beads around this ring, blank spaces representing no input required.
	private List<Bead> beadList;

	//The scene's MusicManager.  Will provides its own deltaTime and IsBeat
	private MusicManager musicManager;

	private Vector3 targetPos;

	/// <summary>
	/// Initialization by:
	///     Setting bead starting positions
	/// </summary>
	void Start()
	{
		targetPos = new Vector3(this.transform.localScale.y / 2.0f, 0.0f, this.transform.position.z);

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
				case BeadEnum.Reverse:
					GameObject reverseGo = GameObject.Instantiate(ReverseBeadPrefab);
					beadList.Add(reverseGo.GetComponent<Bead>());
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
		//GameObject redGo = GameObject.Instantiate(RedBeadPrefab);
		//beadList.Add(redGo.GetComponent<Bead>());
	}

	bool beadAlreadyHit;
	bool pastCenter;
	int lastBead;

	/// <summary>
	/// Once per frame:
	///     Rotate to the correct angle.
	/// </summary>
	void Update()
	{
		if (musicManager.IsBeat)
		{
			if (NeedsReversing)
			{
				speed = -speed;
				NeedsReversing = false;
			}
		}

		//Increment the local time by the current speed
		time += speed * musicManager.DeltaTime;

		transform.rotation = Quaternion.Euler(0, 0, (360 / beadList.Count) * (time));

		//Calculate how close to the beat this frame is.
		frameAccuracy = musicManager.Accuracy;// Mathf.Abs(time % 1);


		//Set Next Bead////////////////////////////////////////////////////////////////////////////////////////
		Bead nextBead;
		int nextBeadIndex = 0;

		if (!musicManager.IsBeat)
		{
			if (speed > 0)
				nextBeadIndex = (int)(time + 1) % beadList.Count;
			else
				nextBeadIndex = (int)(time) % beadList.Count;
		}

		foreach (Bead b in beadList)
		{
			if (b)
				b.IsNext = false;
		}

		nextBead = beadList[nextBeadIndex];
		if (nextBead)
			nextBead.IsNext = true;

		nextBead = beadList[nextBeadIndex];

		if (nextBead)
		{
			if (nextBead.GetType() == typeof(ReverseBead))
			{
				Vector3 targetPosNoZ = new Vector3(targetPos.x, targetPos.y, nextBead.transform.position.z);
				if (Mathf.Abs(Vector3.Distance(nextBead.transform.position, targetPosNoZ)) < 0.3f)
				{
					this.NeedsReversing = true;
				}
			}
		}

		//End Set Next Bead////////////////////////////////////////////////////////////////////////////////////////////

		//Set Current Bead/////////////////////////////////////////////////////////////////////////////////////////////
		float closestDistance = 99999.9f;
		for (int i = 0; i < beadList.Count; i++)
		{
			if (beadList[i])
			{
				if (Mathf.Abs(Vector3.Distance(beadList[i].transform.position, targetPos)) < closestDistance)
				{
					closestDistance = Mathf.Abs(Vector3.Distance(beadList[i].transform.position, targetPos));
					currentBead = beadList[i];
					currentBeadIndex = i;
				}
			}
		}
		//End Set Current Bead///////////////////////////////////////////////////////////////////////////////////////////

		frameAccuracy = 2 * Mathf.Abs(.5f - frameAccuracy);

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
