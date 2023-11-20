using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Prostitute_AnimationManager : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset stand, stand_idle, stand_idle2;
    private string currentAnimation;
    public static Prostitute_AnimationManager Instance { get; private set; }
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
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("stand"))
        {
            SetAnimation(stand, true, 1f);
        }
        if (state.Equals("stand_idle"))
        {
            SetAnimation(stand_idle, false, 1f);
            AddAnimation(stand, true, 1f);
        }
        if (state.Equals("stand_idle2"))
        {
            SetAnimation(stand_idle2, false, 1f);
            AddAnimation(stand, true, 1f);
        }

    }
    private void AddAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(0, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
}
