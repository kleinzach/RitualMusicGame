using UnityEngine;
using System.Collections;

public class PieceController : MonoBehaviour
{
	public GameObject Prefab;

	private RitualMasterModeController ritualMasterModeController;
	private Vector3 offset;

	private Collider2D coll;
	private Vector3 startPos;

	// Use this for initialization
	void Start()
	{
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

		for(int beatRingIndex = 0; beatRingIndex < ritualMasterModeController.BeatRings.Count; beatRingIndex++)
		{
			for(int beatIndex = 0; beatIndex < ritualMasterModeController.BeatRings[beatRingIndex].beadList.Count; beatIndex++)
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
			this.transform.position = startPos;
		}
	}

}
