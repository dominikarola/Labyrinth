using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    AudioSource source; //nasze źródło dźwięku
    double pauseClipTime = 0; //moment zapauzowania muzyki

    public AudioClip[] clips; //tablica z utworami, które będą odtwarzane
    int actualClip = 0; //obecnie odtwarzany plik

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        
        source.clip = clips[0];
        source.Play();
        PitchThis(1);
    }

    public void OnPauseGame()
    {
        pauseClipTime = source.time;
        source.Pause();
    }

    public void OnResumeGame()
    {
        
        source.PlayScheduled(pauseClipTime);
        pauseClipTime = 0;
    }

    void Update()
    {
        if(source.time >= clips[actualClip].length)
        {
            actualClip++;
            if(actualClip > clips.Length - 1)
            {
                actualClip = 0;
            }

            source.clip = clips[actualClip];
            source.Play();
        }
    }

    public void PitchThis(float pitch)
    {
        source.pitch = pitch;
    }

}
