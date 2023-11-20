using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Civilian_AnimationManager : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset giving_thing, idle1, idle2, idle3, idle_panic1, idle_panic2, idle_panic3, running, standing, talking, talking_calling, talking_explaining, talking_panic;
    private string currentAnimation;
    [HideInInspector] public string[] idleAnimationNames;
    private void Start()
    {
        idleAnimationNames = new string[] { "idle1", "idle2", "idle3", "idle_panic1", "idle_panic3" };
        StartCoroutine(IdleAnimation());
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
        if (state.Equals("giving_thing"))
        {
            SetAnimation(giving_thing, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("idle1"))
        {
            SetAnimation(idle1, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("idle2"))
        {
            SetAnimation(idle2, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("idle3"))
        {
            SetAnimation(idle3, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("idle_panic1"))
        {
            SetAnimation(idle_panic1, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("idle_panic2"))
        {
            SetAnimation(idle_panic2, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("idle_panic3"))
        {
            SetAnimation(idle_panic3, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("running"))
            SetAnimation(running, true, 1f);
        if (state.Equals("standing"))
            SetAnimation(standing, true, 1f);
        if (state.Equals("talking"))
            SetAnimation(talking, true, 1f);
        if (state.Equals("talking_calling"))
            SetAnimation(talking_calling, true, 1f);
        if (state.Equals("talking_explaining"))
            SetAnimation(talking_explaining, true, 1f);
        if (state.Equals("talking_panic"))
            SetAnimation(talking, true, 1f);
    }
    private void AddAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(0, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
    IEnumerator IdleAnimation()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(6f, 9f));
            var animation = Random.Range(0, idleAnimationNames.Length);
            SetCharacterState(idleAnimationNames[animation]);
        }
    }
}
