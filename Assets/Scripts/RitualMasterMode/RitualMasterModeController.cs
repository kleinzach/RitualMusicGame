using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualMasterModeController : MonoBehaviour
{
	public List<BeatRing> BeatRings;

	public GameObject speedBeatPlacer;
	public GameObject speedBeatSprite;
	public GameObject speedBeatSpriteWithX;

	public GameObject MarkerPrefab;

	void Update()
	{
		if (isThereASpeedUpOnScreen())
		{
			speedBeatPlacer.SetActive(false);
			speedBeatSprite.SetActive(false);
			speedBeatSpriteWithX.SetActive(true);
		}
		else
		{
			speedBeatPlacer.SetActive(true);
			speedBeatSprite.SetActive(true);
			speedBeatSpriteWithX.SetActive(false);
		}
	}

	private bool isThereASpeedUpOnScreen()
	{
		bool returnValue = false;

		for (int beatRingIndex = 0; beatRingIndex < this.BeatRings.Count; beatRingIndex++)
		{
			for (int beatIndex = 0; beatIndex < this.BeatRings[beatRingIndex].beadList.Count; beatIndex++)
			{
				if ((this.BeatRings[beatRingIndex]) && (this.BeatRings[beatRingIndex].beadList[beatIndex]))
				{
					Bead bead = this.BeatRings[beatRingIndex].beadList[beatIndex];
					if (bead.GetType() == typeof(SpeedAdjustBead))
					{
						returnValue = true;
					}
				}
				else if ((this.BeatRings[beatRingIndex]) && (!this.BeatRings[beatRingIndex].beadList[beatIndex]))
				{
					Debug.Log("here");
					GameObject go = GameObject.Instantiate(MarkerPrefab);
					this.BeatRings[beatRingIndex].beadList[beatIndex] = null;
					this.BeatRings[beatRingIndex].beadList[beatIndex] = go.GetComponent<Bead>();
				}
			}
		}

		return returnValue;
	}
}
