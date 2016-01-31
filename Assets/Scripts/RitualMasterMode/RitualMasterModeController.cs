using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RitualMasterModeController : MonoBehaviour
{
	public List<BeatRing> BeatRings;

	public GameObject speedBeatPlacer;
	public GameObject speedBeatSprite;
	public GameObject speedBeatSpriteWithX;


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
		for (int beatRingIndex = 0; beatRingIndex < this.BeatRings.Count; beatRingIndex++)
		{
			for (int beatIndex = 0; beatIndex < this.BeatRings[beatRingIndex].beadList.Count; beatIndex++)
			{
				if ((this.BeatRings[beatRingIndex]) && (this.BeatRings[beatRingIndex].beadList[beatIndex]))
				{
					Bead bead = this.BeatRings[beatRingIndex].beadList[beatIndex];
					if (bead.GetType() == typeof(SpeedAdjustBead))
					{
						return true;
					}
				}
			}			
		}

		return false;
	}
}
