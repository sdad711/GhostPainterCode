
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Kandinsky_AnimationManager : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset sitting_0, sitting_talking_accenting, sitting_talking_normal, sitting_waving;
    private string currentAnimation;
    public static Kandinsky_AnimationManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void SetAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        if (animationName.name.Equals(currentAnimation))
            return;
        animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        currentAnimation = animationName.name;
        //AddAnimation(mix_blink, true, 0.1f);
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("sitting_0"))
            SetAnimation(sitting_0, true, 1f);
        if (state.Equals("sitting_talking_accenting"))
            SetAnimation(sitting_talking_accenting, true, 1f);
        if (state.Equals("sitting_talking_normal"))
            SetAnimation(sitting_talking_normal, true, 1f);
        if (state.Equals("sitting_waving"))
            SetAnimation(sitting_waving, true, 1f);
    }
    private void AddAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(1, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
}


