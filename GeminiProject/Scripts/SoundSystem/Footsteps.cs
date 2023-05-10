using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Footsteps : MonoBehaviour
{
    public SoundType[] footstepClips;
    public SoundType[] jumpClips;
    [SerializeField] private SO_SoundManager soundManager;

    private void Awake()
    {
        soundManager = Resources.Load<SO_SoundManager>("SO_SoundManager");
    }

    private void Step()
    {
        soundManager.PlayOnGameObjectWithAudioSource(GetRandomFootstepClip(), gameObject, true);
    }

    private SoundType GetRandomFootstepClip()
    {
        return footstepClips[Random.Range(0, footstepClips.Length)];
    }

    public void PlayJump()
    {
        soundManager.PlayOnGameObjectWithAudioSource(GetRandomJumpClip(), gameObject, true);
    }

    private SoundType GetRandomJumpClip()
    {
        return jumpClips[Random.Range(0, jumpClips.Length)];
    }
}
