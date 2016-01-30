using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Manages Music.  Provides a DeltaTime and if the current frame is a beat.
/// Controls strobes as wll.
/// </summary>
public class MusicManager : MonoBehaviour {

    public static MusicManager singleton;

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

	//Music sources
	public AudioSource audioSource;

    public AudioSource[] additiveMusic;

	//Seconds per beat.  Set through BeatsPerMinute
	private float secondsPerBeat;

	//The time the next beat will hit
	private float nextBeat;

	//audioSource.time at the last frame
	private float lastAudioSourceTime;

	//AudioSourceTotalTime in the last frame;
	private float lastAudioSourceTotalTime;

    float _speed = 1f;
    public float speed
    {
        get { return _speed; }
        set { _speed = value;
        audioSource.pitch = value;
            foreach(AudioSource a in additiveMusic)
            {
                a.pitch = value;
            }
        }
    }

	// Use this for initialization
	void Start () {
        singleton = this;
		audioSource = this.GetComponent<AudioSource>();

        audioSource.pitch = 1f;

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
			nextBeat += secondsPerBeat;

            MainVisualizer.Beat(.1f);

			for (int i = 0; i < images.Length; i++)
			{
				images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1.0f);
			}
		}
		else
		{
			IsBeat = false;
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

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            speed += .1f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            speed -= .1f;
        }
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
