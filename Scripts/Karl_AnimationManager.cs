using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Karl_AnimationManager : MonoBehaviour
{
    public SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset balcony_drop, balcony_drop_prepare, cat_cuddle, climb_tree, climb_tree_cut1, climb_tree_cut2, crouched_standing, crouched_walking, drop_landing, drunk_standing, drunk_walking, fall, give_coin, sitting_mix_blink, mix_blink, normal_idle1, normal_idle2_coat, normal_idle3, normal_idle5, normal_running, normal_sitting, normal_sitting_talking, normal_sitting_talking_facepalm, normal_sitting_thinking, normal_standing, normal_standing_dramatic, normal_walking, normal_walking_down, normal_walking_in, normal_walking_out, normal_walking_up, sitting_writing_side, sitting_writing_side_fadeIn, sitting_writing_side_unFaded, surrender, take_object, talking, talking_accusing, talking_angry, talking_sorry, thinking;
    private string currentAnimation;
    [SerializeField] private float blinkingSpeed, blinkingOccurance, walkingSpeed, runningSpeed;
    [HideInInspector] public bool freeze;
    [HideInInspector] public bool invisible, INKinvisible;
    [HideInInspector] public bool runningForYourLife;
    [HideInInspector] public bool karlDied;
    [HideInInspector] public bool goInDoor, goOutDoor;
    private bool changeSortingOrderCorectionBool;
    [HideInInspector] public string[] idleAnimationNames;
    [SerializeField] private bool karlInTransit;
    public static Karl_AnimationManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (karlInTransit)
            InvokeRepeating("SittingBlinking", 0, blinkingOccurance);
        else if(!karlInTransit)
            InvokeRepeating("Blinking", 0, blinkingOccurance);
        idleAnimationNames = new string[] { "normal_idle1", "normal_idle2_coat", "normal_idle3", "normal_idle5" };
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
        //    //if (animationName.name.Equals(currentAnimation))
        //    //    return;
        //    animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        //    currentAnimation = animationName.name;
        //    animator.state.AddAnimation(0, normal_standing, true, 1f);
        //    //currentAnimation = animationName.name;
        //}
        if (!changeSortingOrderCorectionBool)
            StopAllCoroutines();
        if (animationName.name.Equals(currentAnimation))
            return;
        animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        currentAnimation = animationName.name;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("balcony_drop_prepare"))
        {
            ChangeOrderInLayer(2, "Frontground");
            SetAnimation(balcony_drop_prepare, true, 1f);
        }
        if (state.Equals("balcony_drop"))
        {
            Invoke("BalconyDropFX", 1.5f);
            SetAnimation(balcony_drop, false, 1f);
            AddAnimation(0, drop_landing, false, 1);
            AddAnimation(0, normal_standing, true, 1);
        }
        if (state.Equals("cat_cuddle"))
        {
            SetAnimation(cat_cuddle, false, 1f);
            AddAnimation(0, normal_standing, true, 1);
        }
        if (state.Equals("climb_tree"))
        {
            Invoke("BreakingGlassFX", 3);
            if (transform.localScale.x < 0)
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            changeSortingOrderCorectionBool = true;
            StartCoroutine(ChangeOrderInLayerCoroutine(1, "Background", 2.5f));
            Invoke("TurnInvisible", 2.5f);
            SetAnimation(climb_tree, false, 1f);
        }
        if (state.Equals("climb_tree_cut1"))
        {
            SetAnimation(climb_tree_cut1, false, 1f);
            AddAnimation(0, climb_tree_cut2, false, 1);
        }
        if (state.Equals("climb_tree_cut2"))
        {
            SetAnimation(climb_tree_cut2, false, 1f);
            AddAnimation(0, normal_standing, true, 1);
        }
        if (state.Equals("crouched_standing"))
            SetAnimation(crouched_standing, true, 1f);
        if (state.Equals("crouched_walking"))
            SetAnimation(crouched_walking, true, 1f);
        if (state.Equals("drunk_standing"))
            SetAnimation(drunk_standing, true, 1f);
        if (state.Equals("drunk_walking"))
            SetAnimation(drunk_walking, true, 1f);
        if (state.Equals("fall"))
            SetAnimation(fall, false, 1f);
        if (state.Equals("give_coin"))
        {
            SetAnimation(give_coin, false, 1f);
            AddAnimation(0, normal_standing, true, 1);
        }
        if (state.Equals("mix_blink"))
            SetAnimation(mix_blink, false, blinkingSpeed);
        if (state.Equals("normal_idle1"))
        {
            Invoke("FinishIdleAnimation", 1.6f);
            SetAnimation(normal_idle1, false, 1f);
            AddAnimation(0, normal_standing, true, 1);
        }
        if (state.Equals("normal_idle2_coat"))
        {
            Invoke("FinishIdleAnimation", 2.4f);
            SetAnimation(normal_idle2_coat, false, 1f);
            AddAnimation(0, normal_standing, true, 1);
        }
        if (state.Equals("normal_idle3"))
        {
            Invoke("FinishIdleAnimation", 1.4f);
            SetAnimation(normal_idle3, false, 1f);
            AddAnimation(0, normal_standing, true, 1);
        }
        if (state.Equals("normal_idle5"))
        {
            Invoke("FinishIdleAnimation", 4.1f);
            SetAnimation(normal_idle5, false, 1f);
            AddAnimation(0, normal_standing, true, 1);
        }
        if (state.Equals("normal_running"))
            SetAnimation(normal_running, true, runningSpeed);
        if (state.Equals("normal_sitting"))
            SetAnimation(normal_sitting, true, 1);
        if (state.Equals("normal_sitting_talking"))
            SetAnimation(normal_sitting_talking, true, 1);
        if (state.Equals("normal_sitting_talking_facepalm"))
            SetAnimation(normal_sitting_talking_facepalm, true, 1f);
        if (state.Equals("normal_sitting_thinking"))
            SetAnimation(normal_sitting_thinking, true, 1);
        if (state.Equals("normal_standing"))
            SetAnimation(normal_standing, true, 1f);
        if (state.Equals("normal_standing_dramatic"))
            SetAnimation(normal_standing_dramatic, true, 1f);
        if (state.Equals("normal_walking"))
            SetAnimation(normal_walking, true, walkingSpeed);
        if (state.Equals("normal_walking_down"))
            SetAnimation(normal_walking_down, true, 1f);
        if (state.Equals("normal_walking_in"))
        {
            goInDoor = true;
            TurnInvisible();
            SetAnimation(normal_walking_in, false, 1f);
            StartCoroutine(ChangeOrderInLayerCoroutine(0, "Background", 0.8f));
        }
        if (state.Equals("normal_walking_out"))
        {
            goOutDoor = true;
            Invoke("TurnVisible", 0.5f);
            SetAnimation(normal_walking_out, false, 1f);
            AddAnimation(0, normal_standing, true, 1);
            StartCoroutine(ChangeOrderInLayerCoroutine(2, "Frontground", 0.2f));
        }
        if (state.Equals("normal_walking_up"))
            SetAnimation(normal_walking_up, true, 1f);
        if (state.Equals("sitting_writing_side"))
            SetAnimation(sitting_writing_side, true, 1f);
        if (state.Equals("sitting_writing_side_fadeIn"))
        {
            SetAnimation(sitting_writing_side_fadeIn, false, 1f);
            AddAnimation(0, sitting_writing_side, true, 1);
        }
        if (state.Equals("sitting_writing_side_unFaded"))
            SetAnimation(sitting_writing_side_unFaded, true, 1f);
        if (state.Equals("surrender"))
            SetAnimation(surrender, true, 1f);
        if (state.Equals("take_object"))
            SetAnimation(take_object, true, 1f);
        if (state.Equals("talking"))
            SetAnimation(talking, true, 1f);
        if (state.Equals("talking_accusing"))
            SetAnimation(talking_accusing, true, 1f);
        if (state.Equals("talking_angry"))
            SetAnimation(talking_angry, true, 1f);
        if (state.Equals("talking_sorry"))
            SetAnimation(talking_sorry, true, 1f);
        if (state.Equals("thinking"))
            SetAnimation(thinking, true, 1f);
    }
    private void Blinking()
    {
        AddAnimation(1, mix_blink, false, blinkingSpeed);
    }
    private void SittingBlinking()
    {
        AddAnimation(1, sitting_mix_blink, false, blinkingSpeed);
    }
    private void AddAnimation(int track, AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(track, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
        //animationEntry.Complete += AnimationEntry_Complete;
    }
    public void FreezeMotherMucker()
    {
        freeze = true;
        SetCharacterState("surrender");
    }
    public void StartRuning()
    {
        SetCharacterState("normal_running");
        runningForYourLife = true;
    }
    public IEnumerator ChangeOrderInLayerCoroutine(int order, string layerName, float time)
    {
        MeshRenderer karl = animator.gameObject.GetComponent<MeshRenderer>();
        yield return new WaitForSeconds(time);
        changeSortingOrderCorectionBool = false;
        karl.sortingOrder = order;
        karl.sortingLayerName = layerName;
        Debug.Log(karl.sortingLayerName);
        Debug.Log(karl.sortingOrder);
    }
    public void ChangeOrderInLayer(int order, string layerName)
    {
        MeshRenderer karl = animator.gameObject.GetComponent<MeshRenderer>();
        karl.sortingOrder = order;
        karl.sortingLayerName = layerName;
        Debug.Log(karl.sortingLayerName);
        Debug.Log(karl.sortingOrder);
    }
    public void TurnInvisible()
    {
        INKinvisible = true;
    }
    public void TurnVisible()
    {
        INKinvisible = false;
    }
    private void BalconyDropFX()
    {
        AudioManager.Instance.BalconyDrop();
    }
    private void BreakingGlassFX()
    {
        AudioManager.Instance.BreakGlass();
    }
    private void FinishIdleAnimation()
    {
        animator.gameObject.GetComponent<Karl_MoveRestricted>().idleAnimation = false;
    }
    //private void SetUnloopedAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    //{
    //    if (animationName.name.Equals(currentAnimation))
    //        return;
    //    animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
    //    currentAnimation = animationName.name;
    //    animator.state.Complete += UnloopedAnimationComplete;
    //    Debug.Log("unloop");
    //}
    //private void UnloopedAnimationComplete(Spine.TrackEntry animationEntry)
    //{
    //    //animatior.Skeleton.SetSkin("");
    //    animator.state.SetAnimation(0,normal_standing, true).TimeScale = 1;
    //}
    //private void Update()
    //{
    //    //if (DialogueManager.Instance.dialogueIsPlaying)
    //    //{
    //    //    if(currentAnimation != DialogueManager.Instance.karlAnimationChange)
    //    //    {
    //    //        currentAnimation = DialogueManager.Instance.karlAnimationChange;
    //    //        SetCharacterState(currentAnimation);
    //    //    }
    //    //}
    //}
}
