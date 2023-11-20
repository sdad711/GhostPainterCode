using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Guard_AnimationManager : MonoBehaviour
{
    public SkeletonAnimation animator;
    [SerializeField] private AnimationReferenceAsset change_normal2gun, gun_aiming, gun_aimPrepare, gun_idling1, gun_idling2, gun_idling3, gun_punch, gun_running_right, gun_running_right_down, gun_running_right_up, gun_shooting, gun_standing, mix_blink, normal_go_away, normal_idling1, normal_idling2, normal_sleeping_wakeup, normal_sleeping_wall, normal_standing, normal_standing_LookUp, normal_talking, normal_talking_accuse, normal_talking_offended, normal_walking_right, normal_walking_right_down, normal_walking_right_up;
    private string currentAnimation;
    [SerializeField] private float blinkingSpeed, blinkingOccurance, walkingSpeed, runningSpeed, patrolingTime;
    public bool sleeping, patroling;
    [SerializeField] private GameObject guardLook;
    [SerializeField] private float sleepingTime, wakeTime;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private int currentPostion;
    private GameObject target;
    private bool isSleeping;
    private bool chase;
    Vector3 karlsPosition;
    public float startDelay;
    private bool walking;
    public static Guard_AnimationManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("Blinking", 0, blinkingOccurance);
        if (sleeping)
        {
            Invoke("GuardSleeping", startDelay);
        }
        else if (patroling)
        {
            transform.position = wayPoints[currentPostion].transform.position;
            currentPostion++;
            target = wayPoints[currentPostion];
            Invoke("StartWalking", startDelay);
            
        }
        //ako stoji treba mu rendom ubacivat idlove

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
        if (state.Equals("change_normal2gun"))
        {
            SetAnimation(change_normal2gun, false, 1f);
            AddAnimation(0, gun_standing, true, 1f);
        }
        if (state.Equals("gun_aiming"))
            SetAnimation(gun_aiming, true, 1f);

        if (state.Equals("gun_aimPrepare"))
        {
            SetAnimation(gun_aimPrepare, false, 1f);
            AddAnimation(0, gun_aiming, true, 1f);
        }
        if (state.Equals("gun_idling1"))
        {
            SetAnimation(gun_idling1, false, 1f);
            AddAnimation(0, gun_standing, true, 1f);
        }
        if (state.Equals("gun_idling2"))
        {
            SetAnimation(gun_idling2, false, 1);
            AddAnimation(0, gun_standing, true, 1f);
        }
        if (state.Equals("gun_idling3"))
        {
            SetAnimation(gun_idling3, false, 1);
            AddAnimation(0, gun_standing, true, 1f);
        }
        if (state.Equals("gun_punch"))
        {
            SetAnimation(gun_punch, false, 1);
            AddAnimation(0, gun_standing, true, 1f);
        }
        if (state.Equals("gun_running_right"))
            SetAnimation(gun_running_right, true, runningSpeed);
        if (state.Equals("gun_running_right_down"))
            SetAnimation(gun_running_right_down, true, runningSpeed);
        if (state.Equals("gun_running_right_up"))
            SetAnimation(gun_running_right_up, true, runningSpeed);
        if (state.Equals("gun_shooting"))
            SetAnimation(gun_shooting, true, 1f);
        if (state.Equals("gun_standing"))
            SetAnimation(gun_standing, true, 1f);
        if (state.Equals("mix_blink"))
            SetAnimation(mix_blink, false, 1f);
        if (state.Equals("normal_go_away"))
        {
            SetAnimation(normal_go_away, false, 1f);
            AddAnimation(0, normal_standing, true, 1f);
        }
        if (state.Equals("normal_idling1"))
        {
            SetAnimation(normal_idling1, false, 1f);
            AddAnimation(0, normal_standing, true, 1f);
        }
        if (state.Equals("normal_idling2"))
        {
            SetAnimation(normal_idling2, false, 1f);
            AddAnimation(0, normal_standing, true, 1f);
        }
        if (state.Equals("normal_sleeping_wakeup"))
        {
            SetAnimation(normal_sleeping_wakeup, false, 1f);
            AddAnimation(0, normal_standing_LookUp, true, 1f);
        }
        if (state.Equals("normal_sleeping_wall"))
        {
            SetAnimation(normal_idling2, false, 1f);
            AddAnimation(0, normal_sleeping_wall, true, 1f);
        }
        if (state.Equals("normal_standing"))
            SetAnimation(normal_standing, true, 1f);
        if (state.Equals("normal_standing_LookUp"))
            SetAnimation(normal_standing_LookUp, true, 1f);
        if (state.Equals("normal_talking"))
            SetAnimation(normal_talking, true, 1f);
        if (state.Equals("normal_talking_accuse"))
            SetAnimation(normal_talking_accuse, true, 1f);
        if (state.Equals("normal_talking_offended"))
            SetAnimation(normal_talking_offended, true, 1f);
        if (state.Equals("normal_walking_right"))
            SetAnimation(normal_walking_right, true, 1f);
        if (state.Equals("normal_walking_right_down"))
            SetAnimation(normal_walking_right_down, true, 1f);
        if (state.Equals("normal_walking_right_up"))
            SetAnimation(normal_walking_right_up, true, 1f);

    }
    private void Blinking()
    {
        AddAnimation(1, mix_blink, false, blinkingSpeed);
    }
    private void AddAnimation(int track, AnimationReferenceAsset animationName, bool loop, float timeScale)
    {
        Spine.TrackEntry animationEntry = animator.state.AddAnimation(track, animationName, loop, 0f);
        animationEntry.TimeScale = timeScale;
    }
    //private void AnimationEntry_Complete(Spine.TrackEntry animationEntry)
    //{
    //    animator.Skeleton.SetSkin("gun");
    //    SetAnimation(gun_standing, true, 1f);
    //}
    private void GuardSleeping()
    {
        if (!isSleeping)
        {
            Invoke("GuardSleeping", sleepingTime);
            SetCharacterState("normal_sleeping_wall");
            isSleeping = true;
            guardLook.gameObject.SetActive(false);
        }
        else if (isSleeping)
        {
            Invoke("GuardSleeping", wakeTime);
            SetCharacterState("normal_sleeping_wakeup");
            isSleeping = false;
            guardLook.gameObject.SetActive(true);
        }
    }
    private void StartWalking()
    {
        walking = true;
    }
    private void GuardWalking()
    {
        SetCharacterState("normal_walking_right");

        if (transform.position.x < target.transform.position.x && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        else if (transform.position.x > target.transform.position.x && transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        if (transform.position != target.transform.position)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, walkingSpeed * Time.deltaTime);
        else if (transform.position == target.transform.position)
        {
            if (currentPostion >= wayPoints.Length - 1)
            {
                walking = false;
                SetCharacterState("normal_standing");
                Invoke("Patroling", patrolingTime);
                currentPostion = 0;
                target = wayPoints[currentPostion];
            }
            else
            {
                walking = false;
                SetCharacterState("normal_standing");
                Invoke("Patroling", patrolingTime);
                currentPostion++;
                target = wayPoints[currentPostion];
            }
        }
    }
    private void GuardRunning()
    {
        if (transform.position.x < karlsPosition.x && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        else if (transform.position.x > karlsPosition.x && transform.localScale.x > 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        if (transform.position.y < karlsPosition.y)
            SetCharacterState("gun_running_right_up");
        else if (transform.position.y > karlsPosition.y)
            SetCharacterState("gun_running_right_down");
        else
            SetCharacterState("gun_running_right");
        if (Mathf.Abs(karlsPosition.x - transform.position.x) >= 0.1f && Mathf.Abs(karlsPosition.y - transform.position.y) >= 0.15f)
            transform.position = Vector2.MoveTowards(transform.position, karlsPosition, runningSpeed * Time.deltaTime);
        else
        {
            chase = false;
            GunAim();
        }
            
    }
    private void Patroling()
    {
        walkingSpeed = walkingSpeed * 0.5f;
        walking = true;
        SetCharacterState("normal_walking_right");
        Invoke("Turn", 0.5f);
    }
    private void Turn()
    {
        walkingSpeed /= 0.5f;
    }
    public void UnderArrest()
    {
        CancelInvoke();
        sleeping = false;
        walking = false;
        target = Karl_AnimationManager.Instance.animator.gameObject;
        karlsPosition = new Vector3(target.transform.position.x, target.transform.position.y - 1, target.transform.position.z);
        AudioManager.Instance.Whistle();
        SetCharacterState("change_normal2gun");
        Invoke("GunCockFX", 0.75f);
        Invoke("StartRunning", 1f);
    }
    private void StartRunning()
    {
        if (target.transform.position.x < transform.position.x && target.transform.localScale.x > 0)
            target.transform.localScale = new Vector3(target.transform.localScale.x * -1, target.transform.localScale.y, target.transform.localScale.z);
        else if (target.transform.position.x > transform.position.x && target.transform.localScale.x < 0)
            target.transform.localScale = new Vector3(target.transform.localScale.x * -1, target.transform.localScale.y, target.transform.localScale.z);
        chase = true;
        Karl_AnimationManager.Instance.StartRuning();
    }
    private void GunAim()
    {
        SetCharacterState("gun_aimPrepare");
        Invoke("GunShoot", 0.5f);
    }
    private void GunShoot()
    {
        SetCharacterState("gun_shooting");
        AudioManager.Instance.GunShot();
        Invoke("KarlDies", 0.2f);
    }
    private void KarlDies()
    {
        Karl_AnimationManager.Instance.SetCharacterState("fall");
        Karl_AnimationManager.Instance.runningForYourLife = false;
        SetCharacterState("gun_idling2");
        Invoke("RestartLevel", 1f);
    }
    private void GunCockFX()
    {
        AudioManager.Instance.GunCock();
    }
    private void RestartLevel()
    {
        SceneManagerScript.Instance.RestartLevel();
    }
    private void Update()
    {
        if(walking)
        {
            GuardWalking();
        }
        if(chase)
        {
            GuardRunning();
        }
        
    }
}

