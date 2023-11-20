using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class GamepadUI_AnimationManager : MonoBehaviour
{
    public SkeletonGraphic animator;
    [SerializeField] private AnimationReferenceAsset all_red, move_L, padCreate, padDisappear, padGreyNoButton, padMissing, START, switch_LT, switch_LT_RT, use_A, use_X;
    private string currentAnimation;
    public static GamepadUI_AnimationManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void SetAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        //if (loop)
        //{
        //    if (animationName.name.Equals(currentAnimation))
        //        return;
        //    animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        //    currentAnimation = animationName.name;
        //}
        //else if (!loop)
        //{
        //    animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        //    currentAnimation = animationName.name;
        //    if (currentAnimation == "padCreate")
        //    {
        //        animator.state.AddAnimation(0, padGreyNoButton, true, 1f);
        //        currentAnimation = animationName.name;
        //    }
        //    else if (currentAnimation == "padDisappear")
        //    {
        //        animator.state.AddAnimation(0, padMissing, true, 1f);
        //        currentAnimation = animationName.name;
        //    }
        //}
        if (animationName.name.Equals(currentAnimation))
            return;
        animator.AnimationState.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        currentAnimation = animationName.name;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("all_red"))
            SetAnimation(all_red, true, 1f);
        if (state.Equals("move_L"))
            SetAnimation(move_L, true, 1f);
        if (state.Equals("padGreyNoButton"))
            SetAnimation(padGreyNoButton, true, 1f);
        if (state.Equals("padMissing"))
            SetAnimation(padMissing, true, 1f);
        if (state.Equals("START"))
            SetAnimation(START, true, 1);
        if (state.Equals("switch_LT"))
            SetAnimation(switch_LT, true, 1f);
        if (state.Equals("switch_LT_RT"))
            SetAnimation(switch_LT_RT, true, 1f);
        if (state.Equals("use_A"))
            SetAnimation(use_A, true, 1);
        if (state.Equals("use_X"))
            SetAnimation(use_X, true, 1);
        if (state.Equals("padDisappear"))
        {
            SetAnimation(padDisappear, false, 1f);
            AddAnimation(0, padMissing, true, 1f);
        }
        if (state.Equals("padCreate"))
        {
            SetAnimation(padCreate, false, 1f);
            AddAnimation(0, padGreyNoButton, true, 1f);
        }
        if (state.Equals("padCreate_move_L"))
        {
            SetAnimation(padCreate, false, 1f);
            AddAnimation(0, move_L, true, 1f);
        }
        if (state.Equals("padCreate_START"))
        {
            SetAnimation(padCreate, false, 1f);
            AddAnimation(0, START, true, 1f);
        }
        if (state.Equals("padCreate_switch_LT"))
        {
            SetAnimation(padCreate, false, 1f);
            AddAnimation(0, switch_LT, true, 1f);
        }
        if (state.Equals("padCreate_switch_LT_RT"))
        {
            SetAnimation(padCreate, false, 1f);
            AddAnimation(0, switch_LT_RT, true, 1f);
        }
        if (state.Equals("padCreate_use_A"))
        {
            SetAnimation(padCreate, false, 1f);
            AddAnimation(0, use_A, true, 1f);
        }
        if (state.Equals("padCreate_use_X"))
        {
            SetAnimation(padCreate, false, 1f);
            AddAnimation(0, use_X, true, 1f);
        }

    }
    private void AddAnimation(int track, AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.AnimationState.AddAnimation(track, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
}
