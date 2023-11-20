using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Cat_AnimationManager : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset cat_cuddle, mix_blink, normal_standing, go_out;
    private string currentAnimation;
    [SerializeField] private float blinkingSpeed, blinkingOccurance;
    public static Cat_AnimationManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("Blinking", 0, blinkingOccurance);
    }
    private void SetAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        if (animationName.name.Equals(currentAnimation))
            return;
        animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        currentAnimation = animationName.name;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("cat_cuddle"))
        {
            SetAnimation(cat_cuddle, false, 1f);
            AddAnimation(0, normal_standing, true, 1f);
        }
        if (state.Equals("go_out"))
        {
            SetAnimation(go_out, true, 1.4f);
        }
        if (state.Equals("mix_blink"))
        {
            SetAnimation(mix_blink, false, 1f);
        }
        if (state.Equals("normal_standing"))
        {
            SetAnimation(normal_standing, true, 1f);
        }

    }
    private void AddAnimation(int track, AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(track, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
    private void Blinking()
    {
        AddAnimation(1, mix_blink, false, blinkingSpeed);
    }
}
