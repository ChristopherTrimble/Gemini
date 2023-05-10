using UnityEngine;

public class AnimatedSpike : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        animator.Play("SpikeWarning");
    }
}
