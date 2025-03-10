using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip BreakingSound, PlaceSound, JumpSound, HitSound, Music;
    AudioSource source;
    public static AudioManager instance;
    void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void StopMusic()
    {
        source.loop = false;
        source.Stop();

    }

    public void Break()
    {
        source.loop = false;
        source.clip = BreakingSound;
        source.Play();
    }

    public void Hit()
    {
        source.loop = false;
        source.clip = HitSound;
        source.Play();
    }

    public void Place()
    {
        source.loop = false;
        source.clip = PlaceSound;
        source.Play();
    }

    public void Jump()
    {
        source.loop = false;
        source.clip = JumpSound;
        source.Play();
    }

}
