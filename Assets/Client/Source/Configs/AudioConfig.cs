using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig")]
public class AudioConfig : ScriptableObject
{
    public AudioClip laserShootSound;
    public AudioClip cannonBalSound;
    public AudioClip ThunderSound;

    public AudioClip expSound;
    public AudioClip healthUpSound;
    public AudioClip explosionSound;
}
