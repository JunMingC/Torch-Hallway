using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixer masterAudio;

    public void Mute()
    {
        masterAudio.SetFloat("Volume", -80);
    }

    public void Unmute()
    {
        masterAudio.SetFloat("Volume", 0);
    }
}
