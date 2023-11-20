using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Beggar_AnimationManager : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset singing, sitting_default, sitting_idle_saying, sitting_idle_scratching, talk_left, talk_right;
    private string currentAnimation;
    public static Beggar_AnimationManager Instance { get; private set; }
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
        if (state.Equals("singing"))
        {
            SetAnimation(singing, true, 1f);
        }
        if (state.Equals("sitting_default"))
        {
            SetAnimation(sitting_default, true, 1f);
        }
        if (state.Equals("sitting_idle_saying"))
        {
            SetAnimation(sitting_idle_saying, true, 1f);
        }
        if (state.Equals("sitting_idle_scratching"))
        {
            SetAnimation(sitting_idle_scratching, false, 1f);
            AddAnimation(sitting_default, true, 1f);
        }
        if (state.Equals("talk_left"))
        {
            SetAnimation(talk_left, true, 1f);
        }
        if (state.Equals("talk_right"))
        {
            SetAnimation(talk_right, true, 1f);
        }

    }
    private void AddAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(0, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
    private void Update()
    {
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("beggarConversation")).value == "singing")
            SetCharacterState("singing");
    }
}
