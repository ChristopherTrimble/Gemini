using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class AudioSpot : MonoBehaviour
{
    private AudioSource source;
    
    public void PlaySound(AudioClip clip, float volume, bool twoD)
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = twoD ? 0 : 1;
        source.Play();
    }

    public void PlayLoopingSound(AudioClip clip, float volume, bool twoD)
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.spatialBlend = twoD ? 0 : 1;
        source.Play();
    }

    public void SetVolume(float volume)
    {
        source.volume = volume;
    }
}
