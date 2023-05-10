using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PageFlip : MonoBehaviour
{
    private int index = 0;
    private bool isDone;
    private Coroutine animation = null;
    [SerializeField] private float timer;
    [SerializeField] private Image menuBookImage;
    [SerializeField] private List<Sprite> rightPageTurn;
    [SerializeField] private List<Sprite> leftPageTurn;
    [SerializeField] private SO_SoundManager soundManager;
    
    public float AnimationTime => rightPageTurn.Count * timer;

    public void Awake()
    {
        soundManager = Resources.Load<SO_SoundManager>("SO_SoundManager");
    }

    public void FlipRight()
    {
        ResetAnimation();
        animation = StartCoroutine(PageFlipAnim(rightPageTurn));
        soundManager.PlayOnGameObject(this, SoundType.Pageflip, gameObject, true, UnityEngine.Random.Range(70, 150) / 100f);
    }

    public void FlipLeft()
    {
        ResetAnimation();
        animation = StartCoroutine(PageFlipAnim(leftPageTurn));
        soundManager.PlayOnGameObject(this, SoundType.Pageflip, gameObject, true, UnityEngine.Random.Range(70, 150) / 100f);
    }

    private void ResetAnimation()
    {
        if (animation != null) StopCoroutine(animation);
        isDone = false;
        index = 0;
    }
    
    IEnumerator PageFlipAnim(List<Sprite> spriteList)
    {
        yield return new WaitForSecondsRealtime(timer);
        if (index >= spriteList.Count - 1)
            isDone = true;
        
        menuBookImage.sprite = spriteList[index];
        index += 1;

        animation = !isDone ? StartCoroutine(PageFlipAnim(spriteList)) : null;
    }

    private void OnDisable()
    {
        AudioSource[] audioSource = gameObject.GetComponents<AudioSource>();
        foreach (var Source in audioSource)
            Destroy(Source);
        
        menuBookImage.sprite = leftPageTurn[^1];
    }
}
