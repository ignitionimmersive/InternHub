using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAudio : MonoBehaviour
{
    public AudioSource ReverseAudio;
    // Start is called before the first frame update
    void Start()
    {
        ReverseAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
       // StopAudioNow();
    }

    public void StopAudioNow()
    {
        ReverseAudio.Stop();
    }
}
