using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karl_MoveRestricted : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runForLifeSpeed;
    [SerializeField] private GameObject[] positions;
    public int currentPosition;
    private GameObject target;
    private float movementX;
    public bool crouching;
    [SerializeField] private bool running;
    [SerializeField] private bool walkToRight;
    private bool timeForIdle;
    private bool oneShot = true;
    private float timer;
    public bool idleAnimation;
    
    private void Start()
    {
        transform.position = positions[currentPosition].transform.position;
    }
    private void Move()
    {
        movementX = Input.GetAxis("Horizontal");

        if (movementX > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (movementX < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (movementX != 0)
        {
            if (!crouching && !running)
                Karl_AnimationManager.Instance.SetCharacterState("normal_walking");
            else if (crouching)
                Karl_AnimationManager.Instance.SetCharacterState("crouched_walking");
            else if (running)
                Karl_AnimationManager.Instance.SetCharacterState("normal_running");
            timeForIdle = false;
            oneShot = true;
            idleAnimation = false;
        }
        else
        {
            if (!crouching || running)
            {
                if (!idleAnimation)
                    Karl_AnimationManager.Instance.SetCharacterState("normal_standing");
            }
            else if (crouching)
                Karl_AnimationManager.Instance.SetCharacterState("crouched_standing");
            timeForIdle = true;
        }
        //If you are moving to the right i.e. returning back your target will always be your starting point, i.e. current position.
        //When you reach it, or stand on it at the beggining, the current position will be reduced by one, so you will move towards it again.
        if (!walkToRight)
        {
            if (movementX > 0)
            {
                target = positions[currentPosition];
                if (transform.position == target.transform.position)
                {
                    currentPosition--;
                    if (currentPosition < 0)
                        currentPosition = 0;
                }
            }
            //if you are moving to the left, you will always be traveling to the current position +1. When you reach your target the current position increases.
            if (movementX < 0)
            {
                target = positions[currentPosition + 1];
                if (transform.position == target.transform.position)
                {
                    currentPosition++;
                    if (currentPosition > positions.Length)
                        currentPosition = positions.Length;
                }
            }
        }
        else if (walkToRight)
        {
            if (movementX < 0)
            {
                target = positions[currentPosition];
                if (transform.position == target.transform.position)
                {
                    currentPosition--;
                    if (currentPosition < 0)
                        currentPosition = 0;
                }
            }
            //if you are moving to the left, you will always be traveling to the current position +1. When you reach your target the current position increases.
            if (movementX > 0)
            {
                target = positions[currentPosition + 1];
                if (transform.position == target.transform.position)
                {
                    currentPosition++;
                    if (currentPosition > positions.Length)
                        currentPosition = positions.Length;
                }
            }
        }
        if (movementX != 0)
           transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    private void RunForLife()
    {
        if (transform.localScale.x > 0)
            transform.position += Vector3.right * runForLifeSpeed * Time.deltaTime;
        else if (transform.localScale.x < 0)
            transform.position += Vector3.left * runForLifeSpeed * Time.deltaTime;
    }
    private void Update()
    {
        if (!DialogueManager.Instance.dialogueIsPlaying && !Dialogue.Instance.karlStopMoving && !Karl_AnimationManager.Instance.freeze)
        {
            Move();
            if (timeForIdle)
            {
                if (oneShot)
                {
                    timer = Random.Range(6f, 9f);
                    oneShot = false;
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    idleAnimation = true;
                    var animation = Random.Range(0, Karl_AnimationManager.Instance.idleAnimationNames.Length);
                    Karl_AnimationManager.Instance.SetCharacterState(Karl_AnimationManager.Instance.idleAnimationNames[animation]);
                    Debug.Log(Karl_AnimationManager.Instance.idleAnimationNames[animation]);
                    oneShot = true;
                }
            }
        }
        if (Karl_AnimationManager.Instance.runningForYourLife)
        {
            RunForLife();
        }

    }
}
