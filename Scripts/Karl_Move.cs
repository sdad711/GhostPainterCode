using UnityEngine;
using UnityEngine.InputSystem;

public class Karl_Move : MonoBehaviour
{
    [SerializeField] private float speedMin, speedMax;
    private bool karlIsWalking;
    private float movementX, movementY;
    [HideInInspector] public bool freeze;
    private Rigidbody2D rb;
    [SerializeField] private GameObject follower;
    private bool moveFollower;
    private bool startFollower;
    [SerializeField] private GameObject youAreHere;

    [SerializeField] private float followerSpeed;
    public InputAction karlMoveButton;
    private void OnEnable()
    {
        karlMoveButton.Enable();
    }
    private void OnDisable()
    {
        karlMoveButton.Disable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("karlIs")).value == "walking")
            karlIsWalking = true;
        else if (((Ink.Runtime.StringValue)DialogueManager.Instance.GetVariableState("karlIs")).value == "running")
            karlIsWalking = false;
        else
            karlIsWalking = true;
        Invoke("StartFollower", 1.5f);
    }
    private void StartFollower()
    {
        startFollower = true;
    }
    private void MoveFollower()
    {
        startFollower = false;
        moveFollower = true;
    }
    private void Move()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");
        if(karlIsWalking)
            rb.velocity = new Vector2(movementX * speedMin, movementY * speedMin);
        else if (!karlIsWalking)
            rb.velocity = new Vector2(movementX * speedMax, movementY * speedMax);

    }
    private void Update()
    {
        if (!DialogueManager.Instance.dialogueIsPlaying)
        {
            if (!freeze)
                Move();
            else if (freeze)
                rb.velocity = Vector2.zero;
            if (karlMoveButton.WasPressedThisFrame())
            {
                freeze = true;
                CitizenSpawner.Instance.HideCurrentCitizens();
                CopSpawner.Instance.EnlargeCurrentCops();
                Destinations.Instance.EnlargeDestination();
                //rb.velocity = Vector2.zero;
                ShowKarlPositionOnMap();
            }
            if (karlMoveButton.IsPressed())
            {
                SceneManagerScript.Instance.Camera3();
                freeze = true;
            }
            if (karlMoveButton.WasReleasedThisFrame())
            {
                SceneManagerScript.Instance.Camera1();
                CitizenSpawner.Instance.ShowCurrentCitizens();
                CopSpawner.Instance.ShrinkCurrentCops();
                Destinations.Instance.ShrinkDestination();
                freeze = false;
                RemoveKarlPositionOnMap();
            }
        }
        if(startFollower)
        {
            follower.transform.position = Vector2.MoveTowards(follower.transform.position, transform.position, followerSpeed * Time.deltaTime);
            if (follower.transform.position == transform.position)
                MoveFollower();
        }
        if(moveFollower)
            follower.transform.position = transform.position;

    }
    private void ShowKarlPositionOnMap()
    {
        youAreHere.SetActive(true);
    }
    private void RemoveKarlPositionOnMap()
    {
        youAreHere.SetActive(false);
    }
    //private void FixedUpdate()
    //{
    //    //rb.velocity = new Vector2(movementX * speed, movementY * speed);
    //}

}
