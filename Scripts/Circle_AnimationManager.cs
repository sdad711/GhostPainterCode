using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Circle_AnimationManager : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset invisible, create, visible_loop, destroy;
    private string currentAnimation;
    public static Circle_AnimationManager Instance { get; private set; }
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
        if (state.Equals("create"))
        {
            SetAnimation(create, false, 1f);
            AddAnimation(visible_loop, true, 1f);
        }
        if (state.Equals("destroy"))
        {
            SetAnimation(destroy, false, 1f);
            AddAnimation(invisible, true, 1f);
        }
    }
    private void AddAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(0, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
}
