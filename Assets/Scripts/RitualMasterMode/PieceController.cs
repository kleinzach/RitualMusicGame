using UnityEngine;
using System.Collections;

public class PieceController : MonoBehaviour
{
	public GameObject Prefab;

	public bool PreventDuplicatesOnRing = false;

	private RitualMasterModeController ritualMasterModeController;
	private Vector3 offset;

	private Collider2D coll;
	private Vector3 startPos;

	private System.Type beadType;

	private bool lerpBack;

	// Use this for initialization
	void Start()
	{
		lerpBack = false;
		beadType = Prefab.GetComponent<Bead>().GetType();

		startPos = this.transform.position;
		GameObject gameControllerObject = GameObject.Find("RitualMasterModeController");
		if (gameControllerObject != null)
		{
			ritualMasterModeController = gameControllerObject.GetComponent<RitualMasterModeController>();
		}
		if (ritualMasterModeController == null)
		{
			Debug.Log("Cannot find 'ritualMasterModeController' script");
		}

		coll = this.GetComponent<Collider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (lerpBack)
		{
			this.transform.position = Vector3.Lerp(this.transform.position, startPos, 0.5f);
			if (Vector2.Distance(this.transform.position, startPos) < 0.1f)
			{
				this.transform.position = startPos;
				lerpBack = false;
			}
		}
	}


	void OnMouseDown()
	{
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp()
	{
		snapToGrid();
	}

	private void snapToGrid()
	{
		Vector2 currentPos = this.transform.position;
		Bead currentBead = null;
		int currentBeadIndex = -1;
		int currentBeadBeatRingIndex = -1;

		float distance = Mathf.Abs(Vector2.Distance(startPos, currentPos));
		int similarTypesOnThisRing = 0;

		for(int beatRingIndex = 0; beatRingIndex < ritualMasterModeController.BeatRings.Count; beatRingIndex++)
		{
			similarTypesOnThisRing = 0;
			if (PreventDuplicatesOnRing)
			{
				for (int beatIndex = 0; beatIndex < ritualMasterModeController.BeatRings[beatRingIndex].beadList.Count; beatIndex++)
				{
					if ((ritualMasterModeController.BeatRings[beatRingIndex]) && (ritualMasterModeController.BeatRings[beatRingIndex].beadList[beatIndex]))
					{
						Bead bead = ritualMasterModeController.BeatRings[beatRingIndex].beadList[beatIndex];
						if (bead.GetType() == this.beadType)
						{
							similarTypesOnThisRing++;
							break;
						}
					}
				}
			}

			if (similarTypesOnThisRing <= 0)
			{
				for (int beatIndex = 0; beatIndex < ritualMasterModeController.BeatRings[beatRingIndex].beadList.Count; beatIndex++)
				{
					if ((ritualMasterModeController.BeatRings[beatRingIndex]) && (ritualMasterModeController.BeatRings[beatRingIndex].beadList[beatIndex]))
					{
						Bead bead = ritualMasterModeController.BeatRings[beatRingIndex].beadList[beatIndex];
						if (Mathf.Abs(Vector2.Distance(bead.transform.position, currentPos)) < distance)
						{
							distance = Mathf.Abs(Vector2.Distance(bead.transform.position, currentPos));
							currentBead = bead;
							currentBeadIndex = beatIndex;
							currentBeadBeatRingIndex = beatRingIndex;
						}
					}
				}
			}
		}

		if (currentBead)
		{
			GameObject go = GameObject.Instantiate(Prefab);
			GameObject.Destroy(ritualMasterModeController.BeatRings[currentBeadBeatRingIndex].beadList[currentBeadIndex].gameObject);
			ritualMasterModeController.BeatRings[currentBeadBeatRingIndex].beadList[currentBeadIndex] = null;
			ritualMasterModeController.BeatRings[currentBeadBeatRingIndex].beadList[currentBeadIndex] = go.GetComponent<Bead>();
			this.transform.position = startPos;
		}
		else
		{
			lerpBack = true;
		}
	}

}
