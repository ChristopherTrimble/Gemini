//Author: Lauren Davis

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum SoundType
{
    None,
    ThemeInto, ThemeLoop,
    Footsteps1, Footsteps2, Footsteps3,
    Footsteps4, Footsteps5, Footsteps6,
    Footsteps7, Footsteps8, Footsteps9,
    Jump1, Jump2,
    Pageflip,
    Dungeon,
    WaterSplash, Arrows,
    Rock1, Rock2, Rock3,
    Tree1, Tree2, Tree3,
    BossLoop
};

[Serializable]
public enum AudioType
{
    Music, 
    SFX
};

[Serializable] 
public struct AudioInfo
{
    public AudioType type;
    public AudioClip clip;
    [Range(0, 1)]
    public float maxVolume;
}

[CreateAssetMenu(menuName = "Scriptable Objects/SO_SoundManager")]
public class SO_SoundManager : ScriptableObject
{
    public List<AudioInfo> sounds;
    [SerializeField] private GameObject audioObject;
    [SerializeField] private UtilitySO utils;
    private AudioSpot music;

    public float GetClipLength(SoundType sound)
    {
        return sounds[(int)sound].clip.length;
    }
    
    public void PlayMusic(MonoBehaviour caller, SoundType sound, bool looping)
    {
        if (sound == SoundType.None) return;

        Debug.Log("Playing music");
        AudioInfo audioInfo = sounds[(int)sound];
        GameObject tempObject = Instantiate(audioObject);
        AudioSpot spot = tempObject.GetComponent<AudioSpot>();

        if (!looping)
        {
            if (music != null)
                Destroy(music.gameObject);
            
            music = spot;
            spot.PlaySound(audioInfo.clip, audioInfo.maxVolume * utils.musicVolume, true);
            caller.StartCoroutine(DestroySoundObject(tempObject, audioInfo.clip.length));
        }
        else
        {
            music = spot;
            spot.PlayLoopingSound(audioInfo.clip, audioInfo.maxVolume * utils.musicVolume, true);   
        }
    }

    public void PlayOnGameObject(MonoBehaviour caller, SoundType sound, GameObject targetObject, bool twoD, float pitch = 1f)
    {
        if (sound == SoundType.None) return;
        AudioInfo audioInfo = sounds[(int)sound];
        AudioSource tempSource = targetObject.AddComponent<AudioSource>();
        tempSource.clip = audioInfo.clip;
        tempSource.volume = utils.sfxVolume * audioInfo.maxVolume;
        tempSource.spatialBlend = twoD ? 0f : 1f;
        tempSource.pitch = pitch;
        tempSource.Play();
        caller.StartCoroutine(RemoveComponent(targetObject, audioInfo.clip.length));
    }

    public void PlayOnGameObjectWithAudioSource(SoundType sound, GameObject targetObject, bool twoD)
    {
        if (sound == SoundType.None) return;
        AudioInfo audioInfo = sounds[(int)sound];
        AudioSource tempSource = targetObject.GetComponent<AudioSource>();
        tempSource.clip = audioInfo.clip;
        tempSource.volume = utils.sfxVolume * audioInfo.maxVolume;
        tempSource.spatialBlend = twoD ? 0f : 1f;
        tempSource.Play();
    }
    
    public void PlayAtLocation(MonoBehaviour caller, SoundType sound, Vector3 targetLocation)
    {
        AudioInfo audioInfo = sounds[(int)sound];
        GameObject tempObject = Instantiate(audioObject);
        AudioSpot spot = tempObject.GetComponent<AudioSpot>();
        
        tempObject.transform.position = targetLocation;
        spot.PlaySound(audioInfo.clip, audioInfo.maxVolume * utils.sfxVolume, false);
        caller.StartCoroutine(DestroySoundObject(tempObject, audioInfo.clip.length));
    }

    public void ChangeMusicvolume()
    {
        if (music != null)
            music.SetVolume(utils.musicVolume);
        
        if(music != null)
            music.SetVolume(utils.musicVolume);
    }
    
    IEnumerator DestroySoundObject(GameObject soundObject, float time)
    {
        yield return new WaitForSeconds(time);
        music = null;
        Destroy(soundObject);
    }

    IEnumerator RemoveComponent(GameObject soundObject, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(soundObject.GetComponent<AudioSource>());
    }
}
