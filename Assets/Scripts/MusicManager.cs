using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Manages Music.  Provides a DeltaTime and if the current frame is a beat.
/// Controls strobes as wll.
/// </summary>
public class MusicManager : MonoBehaviour {

	//Beats per minute should match the music
	public float BeatsPerMinute = 130.0f;

	//DeltaTime to use to sent to music 
	public float DeltaTime = 0.0f;

	//Whether or not this frame is a beat.
	public bool IsBeat = false;

	//Images to flash on a beat
	public Image[] images;

	//Music
	private AudioSource audioSource;

	//Seconds per beat.  Set through BeatsPerMinute
	private float secondsPerBeat;

	//The time the next beat will hit
	private float nextBeat;

	//audioSource.time at the last frame
	private float lastAudioSourceTime;

	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource>();

		float beatsPerSecond = BeatsPerMinute / 60.0f;
		secondsPerBeat = 1.0f / beatsPerSecond;
		nextBeat = secondsPerBeat;
		lastAudioSourceTime = 0.0f;
		IsBeat = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Set DeltaTime
		float audioSourceTime = audioSource.time;
		DeltaTime = audioSourceTime - lastAudioSourceTime;

		//Dim Strobes
		for (int i = 0; i < images.Length; i++)
		{
			images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, Mathf.Clamp(images[i].color.a - 0.1f, 0.0f, 1.0f));
		}

		//Play a beat if needed
		if (audioSourceTime > nextBeat)
		{
			//Play a beat
			IsBeat = true;
			nextBeat += secondsPerBeat;
			for (int i = 0; i < images.Length; i++)
			{
				images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1.0f);
			}
		}
		else
		{
			IsBeat = false;
		}

		lastAudioSourceTime = audioSourceTime;
	}
}
