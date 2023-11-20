using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class WStreet_DrawMe_AnimationManager : MonoBehaviour
{
    public SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset drawMe, empty;
    private string currentAnimation;
    public static WStreet_DrawMe_AnimationManager Instance { get; private set; }
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
    private void AddAnimation(int track, AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(track, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("drawMe"))
        {
            SetAnimation(drawMe, false, 0.6f);
        }
        if (state.Equals("empty"))
        {
            SetAnimation(empty, true, 1f);
        }
    }
}
