using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Shopkeeper_AnimationManager : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset chasing, chasing_fast, hatered, leaning, leaning_back, leaning_idle1, punch, standing, talk_to_beggar;
    private string currentAnimation;
    private float timer;
    private string[] angryAnimationNames;
    public static Shopkeeper_AnimationManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        angryAnimationNames = new string[] { "hatered", "talk_to_beggar" };
        timer = Random.Range(1.5f, 3f);
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
        if (state.Equals("chasing"))
        {
            SetAnimation(chasing, true, 1f);
        }
        if (state.Equals("chasing_fast"))
        {
            SetAnimation(chasing_fast, true, 1f);
        }
        if (state.Equals("hatered"))
        {
            SetAnimation(hatered, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("leaning"))
        {
            SetAnimation(leaning, true, 1f);
        }
        if (state.Equals("leaning_back"))
        {
            SetAnimation(leaning_back, true, 1f);
        }
        if (state.Equals("leaning_idle1"))
        {
            SetAnimation(leaning_idle1, false, 1f);
            AddAnimation(leaning, true, 1f);
        }
        if (state.Equals("punch"))
        {
            SetAnimation(punch, false, 1f);
            AddAnimation(standing, true, 1f);
        }
        if (state.Equals("standing"))
        {
            SetAnimation(standing, true, 1f);
        }
        if (state.Equals("talk_to_beggar"))
        {
            SetAnimation(talk_to_beggar, true, 1f);
            
        }

    }
    private void AddAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(0, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
        currentAnimation = animationName.name;
    }
    private void Update()
    {
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("beggarConversation")).value == "singing")
        {
            if (!DialogueManager.Instance.dialogueIsPlaying)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    var animation = Random.Range(0, angryAnimationNames.Length);
                    SetCharacterState(angryAnimationNames[animation]);
                    timer = Random.Range(1.5f,3f);
                }
            }
        }
    }
}
