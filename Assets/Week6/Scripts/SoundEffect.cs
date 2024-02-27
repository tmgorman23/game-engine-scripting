using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    AudioSource audio;

    private bool didPlay;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Init(AudioClip clip)
    {
        audio.clip = clip;
    }

    public void Play()
    {
        audio.Play();
        didPlay = true;
    }


    /// <summary>
    /// Changes the volume of the currently playing audio clip
    /// </summary>
    /// <param name="val">Has a range of 0 to 1</param>
    public void SetVolume(float val)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (didPlay == false) return;
        if (audio.isPlaying == false) Destroy(gameObject);
    }
}
