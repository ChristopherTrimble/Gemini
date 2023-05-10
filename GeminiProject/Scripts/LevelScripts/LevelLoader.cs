using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private SO_PlayerSave playerSave;
    [SerializeField] private SO_PlayerSave.Levels level;
    
    [Header("Flag Area")]
    [SerializeField] private List<bool> flags;
    [SerializeField] private List<bool> defaultFlags;
    
    [Header("Music Area")] 
    [SerializeField] private SoundType introSound;
    [SerializeField] private SoundType loopSound;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        playerSave = Resources.Load<SO_PlayerSave>("PlayerSaveSO");
        if (playerSave.GrabLevelFlags(level).Count != 0)
            flags = playerSave.GrabLevelFlags(level);
        else
            SetupDefault();

        SO_SoundManager soundManager = Resources.Load<SO_SoundManager>("SO_SoundManager");
        if (introSound != SoundType.None)
        {
            soundManager.PlayMusic(this, introSound, false);
            StartCoroutine(PlayIntroSound(soundManager));
        }
        else
            soundManager.PlayMusic(this, loopSound, true);
    }

    IEnumerator PlayIntroSound(SO_SoundManager soundManager)
    {
        yield return new WaitForSeconds(soundManager.GetClipLength(introSound));
        soundManager.PlayMusic(this, loopSound, true);
    }
    
    private void SetupDefault()
    {
        flags = defaultFlags;
        playerSave.SetLevelFlags(level, flags);
    }

    public bool GrabFlag(int index)
    {
        return flags[index];
    }
    
    public void ChangeFlag(int index)
    {
        flags[index] = !flags[index];
        playerSave.SetLevelFlags(level, flags);
    } 
    
}
