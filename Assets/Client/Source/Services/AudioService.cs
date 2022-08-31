using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    public AudioSource source;
    public AudioConfig clips;
    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
