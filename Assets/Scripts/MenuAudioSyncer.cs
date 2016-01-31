using UnityEngine;
using System.Collections;

public class MenuAudioSyncer : MonoBehaviour {
    
    public ParticleSystem ps;

    //Beats per minute should match the music
    public float BeatsPerMinute = 130.0f;

    //Seconds per beat.  Set through BeatsPerMinute
    private float secondsPerBeat;

    //The time the next beat will hit
    private float nextBeat;

    //audioSource.time at the last frame
    private float lastAudioSourceTime;

    //AudioSourceTotalTime in the last frame;
    private float lastAudioSourceTotalTime;

    //The number of times the music has looped
    public int NumberOfLoops;

    //Whether or not this frame is a beat.
    public bool IsBeat = false;
    
    //DeltaTime to use to sent to music 
    public float DeltaTime = 0.0f;

    //The total time elapsed of the source
    public float AudioSourceTotalTime;

    //How close to the beat.  1 on beat.  0.1 after beat.  0.9 near beat.
    public float Accuracy;

    AudioSource audioSource;

    // Use this for initialization
    void Start ()
    {
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
        
        //Play a beat if needed
        if (AudioSourceTotalTime > nextBeat)
        {
            //Play a beat
            IsBeat = true;
            Accuracy = 1.0f;
            nextBeat += secondsPerBeat;

            ps.SetParticles(null, 0);
            ps.Emit(300);
        }
        else
        {
            IsBeat = false;
            Accuracy = (secondsPerBeat - (nextBeat - AudioSourceTotalTime)) / secondsPerBeat;
        }

        lastAudioSourceTime = audioSourceTime;
        lastAudioSourceTotalTime = AudioSourceTotalTime;
    }
}
