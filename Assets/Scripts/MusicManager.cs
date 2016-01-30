using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Manages Music.  Provides a DeltaTime and if the current frame is a beat.
/// Controls strobes as wll.
/// </summary>
public class MusicManager : MonoBehaviour {

	//Beats per minute should match the music
	public float BeatsPerMinute = 130.0f;

	//DeltaTime to use to sent to music 
	public float DeltaTime = 0.0f;

	//The total time elapsed of the source
	public float AudioSourceTotalTime;

	//The number of times the music has looped
	public int NumberOfLoops;

	//Whether or not this frame is a beat.
	public bool IsBeat = false;

	//Images to flash on a beat
	public Image[] images;

	//How close to the beat.  1 on beat.  0.1 after beat.  0.9 near beat.
	public float Accuracy;

	//Music
	private AudioSource audioSource;

	//Seconds per beat.  Set through BeatsPerMinute
	private float secondsPerBeat;

	//The time the next beat will hit
	private float nextBeat;

	//audioSource.time at the last frame
	private float lastAudioSourceTime;

	//AudioSourceTotalTime in the last frame;
	private float lastAudioSourceTotalTime;

	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource>();

		//Initialize variables;
		float beatsPerSecond = BeatsPerMinute / 60.0f;
		secondsPerBeat = 1.0f / beatsPerSecond;
		nextBeat = secondsPerBeat;
		lastAudioSourceTime = 0.0f;
		lastAudioSourceTotalTime = 0.0f;
		IsBeat = false;
		NumberOfLoops = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//Set AudioSourceTime and AudioSourceTotalTime
		float audioSourceTime = audioSource.time;
		if (audioSourceTime < lastAudioSourceTime)
		{
			//If we have looped
			NumberOfLoops++;
		}
		AudioSourceTotalTime = NumberOfLoops * audioSource.clip.length + audioSourceTime;

		//Set DeltaTime
		DeltaTime = AudioSourceTotalTime - lastAudioSourceTotalTime;
		
		//Dim Strobes
		for (int i = 0; i < images.Length; i++)
		{
			images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, Mathf.Clamp(images[i].color.a - 0.1f, 0.0f, 1.0f));
		}

		//Play a beat if needed
		if (AudioSourceTotalTime > nextBeat)
		{
			//Play a beat
			IsBeat = true;
			Accuracy = 1.0f;
			nextBeat += secondsPerBeat;

			for (int i = 0; i < images.Length; i++)
			{
				images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1.0f);
			}
		}
		else
		{
			IsBeat = false;
			Accuracy = (secondsPerBeat - (nextBeat - AudioSourceTotalTime)) / secondsPerBeat;
		}

        float[] data = new float[100];
        audioSource.GetOutputData(data, 0);

        float total = 0;
        foreach(float d in data)
        {
            total += d;
        }
        total /= 100f;
        Amplitude = Mathf.Abs(total) * 5;

		lastAudioSourceTime = audioSourceTime;
		lastAudioSourceTotalTime = AudioSourceTotalTime;
	}

    //[HideInInspector]
    public float Amplitude
    {
        set
        {
            Vector3 scale = Vector3.one * value;
            foreach (GameObject g in visualizers)
            {
                g.transform.localScale = scale;
            }
        }
    }

    public GameObject[] visualizers;
}
