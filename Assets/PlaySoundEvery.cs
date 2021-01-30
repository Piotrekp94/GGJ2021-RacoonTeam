using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEvery : MonoBehaviour
{
    public AudioSource _sources;
    public float time;
    private bool stillWaiting = false;


    // Update is called once per frame
    void Update()
    {
        if (!stillWaiting)
        {
            Invoke("playSoundRandomly", time);
            stillWaiting = true;
        }
    }

    public void playSoundRandomly()
    {
        _sources.Play();
        stillWaiting = false;
    }
    
}
