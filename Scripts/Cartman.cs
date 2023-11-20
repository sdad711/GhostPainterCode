using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Cartman : MonoBehaviour
{
    public SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset pull, standing, start, stop;
    [SerializeField] private float speed;
    [SerializeField] private float delayWalking;
    private float randomTime;
    private string currentAnimation;
    private bool walking;
    private bool firstWalk = true;
    private void SetAnimation(AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        if (animationName.name.Equals(currentAnimation))
            return;
        animator.state.SetAnimation(0, animationName, loop).TimeScale = timeScale;
        currentAnimation = animationName.name;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "karl")
        {
            collision.transform.GetComponent<Karl_MoveRestricted>().crouching = true;
            Karl_AnimationManager.Instance.invisible = true;
            if (firstWalk)
            {
                Invoke("StopAndGo", delayWalking);
                firstWalk = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "karl")
        {
            collision.transform.GetComponent<Karl_MoveRestricted>().crouching = false;
            Karl_AnimationManager.Instance.invisible = false; 
        }
    }
    private void AddAnimation(int track, AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(track, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("pull"))
        {
            SetAnimation(start, false, 1f);
            AddAnimation(0, pull, true, 1);
        }
        if (state.Equals("standing"))
        {
            SetAnimation(stop, false, 1f);
            AddAnimation(0, standing, true, 1);
        }
    }
    private void StopAndGo()
    {
        if (!walking)
        {
            PickUp();
            Invoke("StartWalking", 0.5f);
            randomTime = Random.Range(7, 12);
            Invoke("StopAndGo", randomTime);
        }
        else if (walking)
        {
            walking = false;
            SetCharacterState("standing");
            randomTime = Random.Range(7, 12);
            Invoke("StopAndGo", randomTime);
        }
    }
    private void PickUp()
    {
        SetCharacterState("pull");
    }
    private void StartWalking()
    {
        walking = true;
    }
    private void CartmanWalking()
    {
        if (transform.localScale.x > 0)
            transform.position += Vector3.right * speed * Time.deltaTime;
        else if (transform.localScale.x < 0)
            transform.position += Vector3.left * speed * Time.deltaTime;
    }
    
    private void Update()
    {
        if (walking)
            CartmanWalking();
    }
}
