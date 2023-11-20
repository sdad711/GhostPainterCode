using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Sjedatelj_AnimationManager : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset sitting, turnPage;
    private string currentAnimation;
    [SerializeField] private float turnPageSpeed;
    [SerializeField] private float turnPageOccurance;
    public static Sjedatelj_AnimationManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("TurnPage", 2.5f, Random.Range(7, 17));
    }
    private void SetAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        if (loop)
        {
            if (animationName.name.Equals(currentAnimation))
                return;
            animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
            currentAnimation = animationName.name;
        }
        else if (!loop)
        {
            animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
            currentAnimation = animationName.name;
            animator.state.AddAnimation(0, sitting, true, 1f);
        }
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("sitting"))
            SetAnimation(sitting, true, 1f);
        if (state.Equals("turnPage"))
            SetAnimation(turnPage, false, 1f);
    }
    private void AddAnimation(int track, AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(track, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
    private void TurnPage()
    {
        SetAnimation(turnPage, false, turnPageSpeed);
    }
}
