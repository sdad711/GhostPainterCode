using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Dialog script goes on every gameobject that Karl will interact with. It contanins the ink dialogue that is send to the Dialogue Manager on interaction where it is procesed.
/// The Dialoge script finds the speakers on the scene and places the speech bubble (which is created in Dialogue Manager) by its speaker. It also creates a list of speech bubbles so that with every new
/// bubble old ones go up. 
/// </summary>
public class Dialogue : MonoBehaviour
{
    [SerializeField] private DialogueBubblesPlacement dialogueBubblesPlacement;
    public TextAsset inkJSON; //Dialogue in INKY
    private List<string> speakers = new List<string>();
    private List<GameObject> speakersGameObjects = new List<GameObject>(); //?
    private List<GameObject> speechBubbles = new List<GameObject>(); //List of speech bubbles which go up
    [HideInInspector] public float sendBubbleUpCorrection, speechBubbleRightCorrection, speechBubbleYCorrection, speechBubbleLeftCorrection;
    [HideInInspector] public float karlSpeechBubbleRightCorrection, karlSpeechBubbleYCorrection, karlSpeechBubbleLeftCorrection;
    public bool automaticDialogue;
    public bool repeatConversation;
    [HideInInspector] public Coroutine movingBubbleUpCoroutine;
    [SerializeField] private bool conversationDelay;
    [SerializeField] private float conversationDelayTime;
    [HideInInspector] public bool karlStopMoving;
    [SerializeField] private bool doorIn, doorOut;
    private SpriteRenderer doorSprite;
    //private GameObject shaderDoor;
    private bool canClick;
    private bool goBaby;
    [SerializeField] private Transform circleLocation;
    [SerializeField] private bool noInteractionWhenDialogueIsOn;
    public InputAction dialogueButton;

    public static Dialogue Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        dialogueButton.Enable();
    }
    private void OnDisable()
    {
        dialogueButton.Disable();
    }
    private void Start()
    {
        if (doorIn || doorOut)
        {
            doorSprite = GetComponent<SpriteRenderer>();
            //shaderDoor = gameObject.transform.Find("ShaderDoor").gameObject;
            //shaderDoor.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //On a collision the conversation starts
    {
        if (collision.gameObject.tag == "karl" && automaticDialogue)
        {
            if (noInteractionWhenDialogueIsOn && DialogueManager.Instance.dialogueIsPlaying)
                return;
            else
            {
                if (conversationDelay)
                {
                    DialogueManager.Instance.firstSentanceBetweenTwoInksNotShowingSolutionBool = true;
                    karlStopMoving = true;
                    Invoke("StartConversation", conversationDelayTime);
                }
                else if (!conversationDelay)
                    StartConversation();
                if (!repeatConversation)
                    this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        else if(collision.gameObject.tag == "karl" && !automaticDialogue)
        {
            if (noInteractionWhenDialogueIsOn && DialogueManager.Instance.dialogueIsPlaying)
                return;
            else
            {
                GameObject circle = GameObject.FindGameObjectWithTag("circle");
                circle.transform.position = circleLocation.position;
                Circle_AnimationManager.Instance.SetCharacterState("create");
                //if(doorIn || doorOut)
                //{
                //    //shaderDoor.SetActive(true);
                //}
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl" && !automaticDialogue)
        {
            if (noInteractionWhenDialogueIsOn && DialogueManager.Instance.dialogueIsPlaying)
                return;
            else
            {
                if (!DialogueManager.Instance.dialogueIsPlaying)
                    canClick = true;
            
                //if (doorIn)
                //{
                //    if (!DialogueManager.Instance.isTimer)
                //    {
                //        canClick = true;
                //    }
                //    if (goBaby)
                //    {
                //        DialogueManager.Instance.EnterDialogueMode(inkJSON); //The INKY dialogue is sent to the dialogue manager
                //        if (!repeatConversation)
                //            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                //        //shaderDoor.SetActive(false);
                //        Circle_AnimationManager.Instance.SetCharacterState("destroy");
                //    }

                //}
            }
            //if (goBaby)
            //{
            //    StartConversation();
            //    goBaby = false;
            //    canClick = false;
            //    if (!repeatConversation)
            //        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //    //shaderDoor.SetActive(false);
            //    Circle_AnimationManager.Instance.SetCharacterState("destroy");
            //}
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "karl" && !automaticDialogue)
        {
            if (noInteractionWhenDialogueIsOn && DialogueManager.Instance.dialogueIsPlaying)
                return;
            else
            {
                Circle_AnimationManager.Instance.SetCharacterState("destroy");
                canClick = false;
                goBaby = false;
                //if (doorIn || doorOut)
                //shaderDoor.SetActive(false);
            }
        }
    }
    private void StartConversation()
    {
        DialogueManager.Instance.PlaceBubblesCorrections(dialogueBubblesPlacement);
        DialogueManager.Instance.EnterDialogueMode(inkJSON); //The INKY dialogue is sent to the dialogue manager
        karlStopMoving = false;
    }
    /// <summary>
    /// Dialogue script checks for any speaker in the conversation, it adds them to the list and activates a function to find them on the screen
    /// </summary>
    /// <param name="speaker"></param>
    public void CheckForNewSpeakers(string speaker)
    {
        if (!speakers.Contains(speaker))
        {
            speakers.Add(speaker);
            FindSpeaker(speaker);
        }
    }
    private void FindSpeaker(string speaker)
    {
        if (speaker != "none" && speaker != "narator" && speaker != "end" && speaker != null && speaker != "")
        {
            GameObject character = GameObject.FindGameObjectWithTag(speaker);
            speakersGameObjects.Add(character);
        }
    }
    public void DefaultAnimationStates(string speaker)
    {
        foreach ( string speakerInList in speakers)
        {
            if(speakerInList == speaker)
            {
                string name = speakerInList;
                string animationManagerName = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower() + "_AnimationManager";
                if (animationManagerName == "Karl_AnimationManager")
                    Karl_AnimationManager.Instance.SetCharacterState(DialogueManager.Instance.defaultTalking);
                if (animationManagerName == "Kandinsky_AnimationManager")
                    Kandinsky_AnimationManager.Instance.SetCharacterState("sitting_talking_normal");
                if (animationManagerName == "Johann_AnimationManager")
                    Johann_AnimationManager.Instance.SetCharacterState("talking");
                if (animationManagerName == "Guard_AnimationManager")
                    Guard_AnimationManager.Instance.SetCharacterState("normal_talking");
                if (animationManagerName == "Guard2_AnimationManager")
                    Guard2_AnimationManager.Instance.SetCharacterState("normal_talking");
                if (animationManagerName == "Guard3_AnimationManager")
                    Guard3_AnimationManager.Instance.SetCharacterState("normal_talking");
                if (animationManagerName == "Guard4_AnimationManager")
                    Guard4_AnimationManager.Instance.SetCharacterState("normal_talking");
                if (animationManagerName == "Beggar_AnimationManager")
                    Beggar_AnimationManager.Instance.SetCharacterState("talk_left");
                if (animationManagerName == "Cat_AnimationManager")
                    Cat_AnimationManager.Instance.SetCharacterState("normal_standing");
                if (animationManagerName == "Prostitute_AnimationManager")
                    Prostitute_AnimationManager.Instance.SetCharacterState("stand_idle2");
                //etc etc
            }
            else if (speakerInList != speaker)
            {
                string name = speakerInList;
                string animationManagerName = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower() + "_AnimationManager";
                if (animationManagerName == "Karl_AnimationManager")
                    Karl_AnimationManager.Instance.SetCharacterState(DialogueManager.Instance.defaultStanding);
                if (animationManagerName == "Kandinsky_AnimationManager")
                    Kandinsky_AnimationManager.Instance.SetCharacterState("sitting_0");
                if (animationManagerName == "Johann_AnimationManager")
                    Johann_AnimationManager.Instance.SetCharacterState("standing");
                if (animationManagerName == "Guard_AnimationManager")
                    Guard_AnimationManager.Instance.SetCharacterState("normal_standing");
                if (animationManagerName == "Guard2_AnimationManager")
                    Guard2_AnimationManager.Instance.SetCharacterState("normal_standing");
                if (animationManagerName == "Guard3_AnimationManager")
                    Guard3_AnimationManager.Instance.SetCharacterState("normal_standing");
                if (animationManagerName == "Guard4_AnimationManager")
                    Guard4_AnimationManager.Instance.SetCharacterState("normal_standing");
                if (animationManagerName == "Beggar_AnimationManager")
                    Beggar_AnimationManager.Instance.SetCharacterState("sitting_default");
                if (animationManagerName == "Cat_AnimationManager")
                    Cat_AnimationManager.Instance.SetCharacterState("normal_standing");
                if (animationManagerName == "Prostitute_AnimationManager")
                    Prostitute_AnimationManager.Instance.SetCharacterState("stand");
                //etc etc
            }
        }
    }
    public void RecieveSpeechBubble(GameObject speechBubble)
    {
        MoveBubblesUp(speechBubble);
        speechBubbles.Add(speechBubble);
        PlaceSpeechBubble(speechBubble);
    }
    private void MoveBubblesUp(GameObject speechBubble)
    {
        float height = speechBubble.transform.Find("Background").gameObject.GetComponent<SpriteRenderer>().size.y;
        foreach (GameObject bubble in speechBubbles)
        {
            var background = bubble.transform.Find("Background").gameObject;
            //var color = background.GetComponent<SpriteRenderer>().color;
            //color.a = 0.6f;
            //background.GetComponent<SpriteRenderer>().color = color;
            var thinking = background.transform.Find("Thinking").gameObject;
            thinking.gameObject.SetActive(false);
            var talking = background.transform.Find("Talking").gameObject;
            talking.gameObject.SetActive(false);
            Vector3 up = new Vector3(bubble.transform.position.x, bubble.transform.position.y + height + sendBubbleUpCorrection, bubble.transform.position.z);
            StartCoroutine(MovingUp(bubble, up));
        }
    }
    IEnumerator MovingUp(GameObject bubble, Vector3 up)
    {
        while (bubble.transform.position != up)
        {
            bubble.transform.position = Vector2.MoveTowards(bubble.transform.position, up, DialogueManager.Instance.movingUpSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    private void PlaceSpeechBubble(GameObject speechBubble)
    {
        var background = speechBubble.transform.Find("Background").gameObject;
        var thinking = background.transform.Find("Thinking").gameObject;
        var talking = background.transform.Find("Talking").gameObject;
        if(DialogueManager.Instance.dialogueMode == "talking")
        {
            thinking.SetActive(false);
            talking.SetActive(true);
        }
        else if(DialogueManager.Instance.dialogueMode == "thinking")
        {
            thinking.SetActive(true);
            talking.SetActive(false);
        }
        else
        {
            thinking.SetActive(false);
            talking.SetActive(true);
        }
        for (int i = 0; i < speakersGameObjects.Count; i++)
        {
            if (speakersGameObjects[i].tag == DialogueManager.Instance.speaker)
            {
                GameObject speaker = speakersGameObjects[i].gameObject;
                if (DialogueManager.Instance.speaker != "karl")
                {
                    if (DialogueManager.Instance.textPosition == "right")
                    {
                        speechBubble.transform.position = new Vector3(speaker.transform.position.x + speechBubbleRightCorrection, speaker.transform.position.y + speechBubbleYCorrection, speaker.transform.position.z);
                        if (background.transform.localScale.x > 0)
                            background.transform.localScale = new Vector3(background.transform.localScale.x * -1, background.transform.localScale.y, background.transform.localScale.z);
                       
                    }
                    else if (DialogueManager.Instance.textPosition == "left")
                    {
                        speechBubble.transform.position = new Vector3(speaker.transform.position.x - speechBubbleLeftCorrection, speaker.transform.position.y + speechBubbleYCorrection, speaker.transform.position.z);
                        //speechBubble.transform.position = new Vector3(speaker.transform.position.x - bubblePositions.speechBubbleLeftCorrection, speaker.transform.position.y + bubblePositions.speechBubbleYCorrection, speaker.transform.position.z);
                        if (background.transform.localScale.x < 0)
                            background.transform.localScale = new Vector3(background.transform.localScale.x * -1, background.transform.localScale.y, background.transform.localScale.z);
                        
                    }
                    else
                    {
                        if (speaker.transform.localScale.x > 0)
                        {
                            speechBubble.transform.position = new Vector3(speaker.transform.position.x - speechBubbleLeftCorrection, speaker.transform.position.y + speechBubbleYCorrection, speaker.transform.position.z);
                            //speechBubble.transform.position = new Vector3(speaker.transform.position.x - bubblePositions.speechBubbleLeftCorrection, speaker.transform.position.y + bubblePositions.speechBubbleYCorrection, speaker.transform.position.z);
                            if (background.transform.localScale.x < 0)
                                background.transform.localScale = new Vector3(background.transform.localScale.x * -1, background.transform.localScale.y, background.transform.localScale.z);
                           
                        }
                        else if (speaker.transform.localScale.x < 0)
                        {
                            speechBubble.transform.position = new Vector3(speaker.transform.position.x + speechBubbleRightCorrection, speaker.transform.position.y + speechBubbleYCorrection, speaker.transform.position.z);
                            //speechBubble.transform.position = new Vector3(speaker.transform.position.x + bubblePositions.speechBubbleRightCorrection, speaker.transform.position.y + bubblePositions.speechBubbleYCorrection, speaker.transform.position.z);
                            if (background.transform.localScale.x > 0)
                                background.transform.localScale = new Vector3(background.transform.localScale.x * -1, background.transform.localScale.y, background.transform.localScale.z);
                        
                        }
                        else
                            Debug.Log("You don't exist.");
                    }
                }
                else if (DialogueManager.Instance.speaker == "karl")
                {
                    if (DialogueManager.Instance.karlTextPosition == "right")
                    {
                        speechBubble.transform.position = new Vector3(speaker.transform.position.x + karlSpeechBubbleRightCorrection, speaker.transform.position.y + karlSpeechBubbleYCorrection, speaker.transform.position.z);
                        //speechBubble.transform.position = new Vector3(speaker.transform.position.x + bubblePositions.karlSpeechBubbleRightCorrection, speaker.transform.position.y + bubblePositions.karlSpeechBubbleYCorrection, speaker.transform.position.z);
                        if (background.transform.localScale.x > 0)
                            background.transform.localScale = new Vector3(background.transform.localScale.x * -1, background.transform.localScale.y, background.transform.localScale.z);
              
                    }
                    else if (DialogueManager.Instance.karlTextPosition == "left")
                    {
                        speechBubble.transform.position = new Vector3(speaker.transform.position.x - karlSpeechBubbleLeftCorrection, speaker.transform.position.y + karlSpeechBubbleYCorrection, speaker.transform.position.z);
                        //speechBubble.transform.position = new Vector3(speaker.transform.position.x - bubblePositions.karlSpeechBubbleLeftCorrection, speaker.transform.position.y + bubblePositions.karlSpeechBubbleYCorrection, speaker.transform.position.z);
                        if (background.transform.localScale.x < 0)
                            background.transform.localScale = new Vector3(background.transform.localScale.x * -1, background.transform.localScale.y, background.transform.localScale.z);
                   
                    }
                    else
                    {
                        if (speaker.transform.localScale.x > 0)
                        {
                            speechBubble.transform.position = new Vector3(speaker.transform.position.x - karlSpeechBubbleLeftCorrection, speaker.transform.position.y + karlSpeechBubbleYCorrection, speaker.transform.position.z);
                            //speechBubble.transform.position = new Vector3(speaker.transform.position.x - bubblePositions.karlSpeechBubbleLeftCorrection, speaker.transform.position.y + bubblePositions.karlSpeechBubbleYCorrection, speaker.transform.position.z);
                            if (background.transform.localScale.x < 0)
                                background.transform.localScale = new Vector3(background.transform.localScale.x * -1, background.transform.localScale.y, background.transform.localScale.z);
              
                        }
                        else if (speaker.transform.localScale.x < 0)
                        {
                            speechBubble.transform.position = new Vector3(speaker.transform.position.x + karlSpeechBubbleRightCorrection, speaker.transform.position.y + karlSpeechBubbleYCorrection, speaker.transform.position.z);
                            //speechBubble.transform.position = new Vector3(speaker.transform.position.x + bubblePositions.karlSpeechBubbleRightCorrection, speaker.transform.position.y + bubblePositions.karlSpeechBubbleYCorrection, speaker.transform.position.z);
                            if (background.transform.localScale.x > 0)
                                background.transform.localScale = new Vector3(background.transform.localScale.x * -1, background.transform.localScale.y, background.transform.localScale.z);
                
                        }
                        else
                            Debug.Log("You don't exist.");
                    }
                }
            }
        }
    }
    public void CloseAllBubbles()
    {
        foreach (GameObject speechBubble in speechBubbles)
        {
            var bubble = speechBubble.gameObject.GetComponent<SpeechBubble>();
            SpeechBubblePool.Instance.ReturnToPool(bubble);
        }
    }
    public void ClearBubblesList()
    {
        speechBubbles.Clear();
    }
    public void ClearAllLists()
    {
        speakers.Clear();
        speakersGameObjects.Clear();
        speechBubbles.Clear();
    }
    public void RemoveFromList(GameObject bubble)
    {
        speechBubbles.Remove(bubble);
    }

    public void RemoveFromSpeakers(string speaker)
    {
        if (speakers.Contains(speaker))
        {
            speakers.Remove(speaker);
            for (int i = 0; i < speakersGameObjects.Count; i++)
            {
                if (speakersGameObjects[i].tag == speaker)
                    speakersGameObjects.Remove(speakersGameObjects[i]);
            }
        }
    }
    IEnumerator GoInDoor()
    {
                yield return new WaitForSeconds(0.5f);
                AudioManager.Instance.OpenDoor();
                doorSprite.enabled = false;
                Invoke("CloseDoorIn", 0.3f);
    }
    IEnumerator GoOutDoor()
    {
                yield return new WaitForSeconds(0.1f);
                AudioManager.Instance.OpenDoor();
                doorSprite.gameObject.SetActive(false);
                Invoke("CloseDoorOut", 0.2f);
    }
    private void CloseDoorIn()
    {
        StopAllCoroutines();
        doorSprite.enabled = true;
    }
    private void CloseDoorOut()
    {
        StopAllCoroutines();
        doorSprite.gameObject.SetActive(true);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

    }
    private void Update()
    {
        if(Karl_AnimationManager.Instance.goInDoor && doorIn)
        {
            StartCoroutine(GoInDoor());
            Karl_AnimationManager.Instance.goInDoor = false;
        }
        if(Karl_AnimationManager.Instance.goOutDoor && doorOut)
        {
            StartCoroutine(GoOutDoor());
            Karl_AnimationManager.Instance.goOutDoor = false;
        }
        if (dialogueButton.WasPressedThisFrame() && canClick)
        {
            goBaby = true;
        }    
        if (goBaby)
        {
            StartConversation();
            goBaby = false;
            canClick = false;
            if (!repeatConversation)
                this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            //shaderDoor.SetActive(false);
            Circle_AnimationManager.Instance.SetCharacterState("destroy");
        }
    }
}
