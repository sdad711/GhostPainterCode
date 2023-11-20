using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Wagon_Luggage_AnimationManager : MonoBehaviour
{
    public SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset wagon_drawMe, wagon_finished, wagon_startEmpty;
    private string currentAnimation;
    public static Wagon_Luggage_AnimationManager Instance { get; private set; }
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
        if (state.Equals("wagon_drawMe"))
        {
            SetAnimation(wagon_drawMe, false, 0.6f);
            AddAnimation(0, wagon_finished, true, 1);
        }
        if (state.Equals("wagon_startEmpty"))
        {
            SetAnimation(wagon_startEmpty, true, 1f);
        }
    }
}
