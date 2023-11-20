using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Ink.Runtime;
using TMPro;

/// <summary>
/// Dialogue Manager processes everything from the INKY dialogue which is sent from the Dialogue script. It controls the dialogue text, and reads the tags for speakers, animations and everything else.
/// It also controls the choices
/// </summary>

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject[] choiceButtons; //These are the choice buttons in coverstaions
    private TMP_Text[] choiceTexts; //These are the texts inside of the choice buttons, they are connected via code 
    /*[SerializeField] private TMP_Text mainText; */
    private string mainText; //This is the main text of the dialogue

    private Story currentStory; //

    [HideInInspector] public bool dialogueIsPlaying; //Bool that disables movemenet in Movements scripts
    private bool isChoosing;//Bool that enables the choice mode of dialogue
   
    [SerializeField] private DialogueVariables dialogueVariables; //Conecting with the DialogueVariables which reads and saves any variables inside the INKY dialogue
    [SerializeField] private TextAsset loadGlobalsJSON; //The link for every INKY dialogue to connect with the Dialogue Variables
    /// <summary>
    /// Tags that are used in the INKY dialogue
    /// </summary>
    private const string KARL_TAG = "karl";
    private const string TARGET_TAG = "target";
    private const string GOTO_TAG = "goto";
    private const string SPEED_TAG = "speed";
    private const string DEFAULT_TALKING_TAG = "default_talking";
    private const string DEFAULT_STANDING_TAG = "default_standing";
    private const string SPEAKER_TAG = "speaker"; //In INKY the tags for speakers are #speaker:karl #speaker:kandinsky etc. !!FOR #speaker:none no speech bubble is made but the tags are still active like animations
    private const string BUBBLE_TAG = "bubble";
    private const string ANIMATION_KARL_TAG = "animation_karl"; //The tags for animations are #animation_karl: + name of the animation from animation manager like #animation_karl:normal_idle_1 
    private const string ANIMATION_KANDINSKY_TAG = "animation_kandinsky";
    private const string ANIMATION_GAMEPAD_TAG = "animation_gamepad";
    private const string ANIMATION_GAMEPADUI_TAG = "animation_gamepadUI";
    private const string ANIMATION_JOHANN_TAG = "animation_johann";
    private const string ANIMATION_GUARD_TAG = "animation_guard";
    private const string ANIMATION_GUARD2_TAG = "animation_guard2";
    private const string ANIMATION_GUARD3_TAG = "animation_guard3";
    private const string ANIMATION_GUARD4_TAG = "animation_guard4";
    private const string ANIMATION_BEGGAR_TAG = "animation_beggar";
    private const string ANIMATION_CAT_TAG = "animation_cat";
    private const string ANIMATION_PROSTITUTE_TAG = "animation_prostitute";
    private const string ANIMATION_SHOPKEEPER_TAG = "animation_shopkeeper";
    private const string ANIMATION_WAGON_LUGGAGE_TAG = "animation_wagon_luggage";
    private const string ANIMATION_WSTREET_DRAWME_TAG = "animation_wstreet_drawMe";
    private const string KARL_TEXT_POSITION_TAG = "karl_text_position";
    private const string TEXT_POSITION_TAG = "text_position"; //The tag for positioning speech bubbles right or left from the speaker, nevertheless of the speakers orientation: #text_position:left, #text_position:right
    private const string TIMER_TAG = "timer"; //The tag for timer which blocks player from skipping that sentence for desired time: #timer:6, #timer:2.3 etc
    private const string MODE_TAG = "mode"; //The tag that changes the images for thinking or talking in the speechbubble: #mode:thinking #mode:talking
    private const string CAMERA_TAG = "camera";
    private const string LOAD_TAG = "load";
    private const string CLEAR_TAG = "clear";
    private const string FLIP_TAG = "flip";
    private const string CURRENT_POSITION_TAG = "current_position";
    private const string AUDIO_FX_TAG = "audio_fx";
    private const string AUDIO_TAG = "audio";
    /// <summary>
    /// Variables that store the value of the tags from the INKY dialogue
    /// </summary>
    private string karl;
    [HideInInspector] public string target;
    [HideInInspector] public string goTo;
    [HideInInspector] public float inkWalkingSpeed;
    private string InvisibleBubble;
    [HideInInspector] public string karlAnimationChange, kandinskyAnimationChange, gamepadAnimationChange, gamepadUIAnimationChange, johannAnimationChange, guardAnimationChange, guard2AnimationChange, guard3AnimationChange, guard4AnimationChange, beggarAnimationChange, catAnimationChange, prostituteAnimationChange, shopkeeperAnimationChange;
    [HideInInspector] public string wagonLuggageAnimationChange, wStreetDrawMeAnimationChange;
    [HideInInspector] public string speaker;
    [HideInInspector] public string defaultTalking, defaultStanding;
    [HideInInspector] public string karlTextPosition, textPosition;
    private float timerTime = 0;
    [HideInInspector] public string dialogueMode;
    [HideInInspector] public string sceneCamera;
    [HideInInspector] public string loadScene;
    [HideInInspector] public string clearBubbles;
    [HideInInspector] public string targetTimer;
    [HideInInspector] public string flipTarget;
    [HideInInspector] public string audioFX, audioMusic;
    [HideInInspector] public int currentPosition;
    [HideInInspector] public bool isTimer; //Bool for timer, when activated player cant continue story with button press
    private GameObject speechBubble; //Pooled bubble is saved in this gameobject which is send to the dialoge script
    const string alphaCode = "<color=#00000000>";
    [SerializeField] private float textSpeed;
    private TMP_Text skippingSpeechBubble;
    private string copyMainText;
    private Coroutine displayLineCoroutine;
    [SerializeField] private float showBubbleDelay;
    [SerializeField] private float enableNaratorToContinueTime;
    public float movingUpSpeed;
    [HideInInspector] public bool goForAWalk;
    [SerializeField] private GameObject upPanel, downPanel;
    private bool inkIntroWalking;
    private bool delayingSpeechBubble;
    private bool delayingNaratorPanel;
    private bool oneShot;
    private bool audioTimer, audioTimerOneShot;
    
    [HideInInspector] public bool firstSentanceBetweenTwoInksNotShowingSolutionBool;
    public bool naratorOnly;
    public InputAction dialogueManagerButton;

    public static DialogueManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON); //Connects the Dialogue Variables with Dialogue Manager
    }
    private void OnEnable()
    {
        dialogueManagerButton.Enable();
    }
    private void OnDisable()
    {
        dialogueManagerButton.Disable();
    }
    private void Start()
    {
        //Connects the texts inside the buttons with the buttons (you can add any amount of buttons on the scene)
        choiceTexts = new TMP_Text[choiceButtons.Length];
        int index = 0;
        foreach (GameObject choiceButton in choiceButtons)
        {
            choiceTexts[index] = choiceButton.GetComponentInChildren<TMP_Text>();
            index++;
        }

    }
    /// <summary>
    /// Entering dialogue. The current story is the story that is transfered from the Dialogue script. Dialogue Variables starts to read and save any variables in the dialogue.
    /// Dialogue Manager starts to read and examine tags in the dialogue. And the main text gets the text from the story. Dialogue Manager creates a bubble which is sent to the Dialogue script.
    /// </summary>
    /// <param name="inkJSON"></param>
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        //if (Dialogue.Instance.karlInConversation)
        //    dialogueIsPlaying = true;
        //else if (!Dialogue.Instance.karlInConversation)
        //    dialogueIsPlaying = false;
        currentStory = new Story(inkJSON.text);
        dialogueVariables.StartListening(currentStory);
        mainText = currentStory.Continue();
        HandleTags(currentStory.currentTags);
        if (speaker != "narator" && speaker != "none")
            CreateBubble();
        else if (speaker == "narator")
            Monologue();
        else if (speaker == "none")
            inkIntroWalking = true;
    }
    public void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);
        dialogueIsPlaying = false;
        if (!naratorOnly)
        {
            Dialogue.Instance.CloseAllBubbles();
            Dialogue.Instance.ClearAllLists();
        }
        speaker = null;
        textPosition = null;
        karlTextPosition = null;
        dialogueMode = null;
        timerTime = 0;
        karl = null;
        StopAllCoroutines();
        delayingSpeechBubble = false;
        delayingNaratorPanel = false;
        if (!naratorOnly)
        {
            Dialogue.Instance.StopAllCoroutines();
            SceneManagerScript.Instance.StopAllCoroutines();
        }
        ClosePanels();
        if (loadScene != null && loadScene != "")
        {
            SceneManagerScript.Instance.LoadSceneFromInk(loadScene);
        }
    }
    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            oneShot = true;
            isChoosing = false;
            delayingSpeechBubble = true;
            delayingNaratorPanel = true;
            inkWalkingSpeed = 0;
            if (speaker != "none" && speaker != "narator")
                inkIntroWalking = false;
            if (!inkIntroWalking)
                FillSpeechBubble(skippingSpeechBubble, copyMainText);
            if (!naratorOnly)
            {
                Dialogue.Instance.StopAllCoroutines();
            }
            mainText = currentStory.Continue();
            HandleTags(currentStory.currentTags);

            if (speaker != "none" && speaker != "narator" && speaker != "end")
            {
                ClosePanels();
                CreateBubble();
            }
            else if (speaker == "narator")
            {
                Monologue();
            }
            else if (speaker == "end")
            {
                delayingSpeechBubble = false;
                delayingNaratorPanel = false;
            }
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }
    public void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("Vi?e odabira nego ?to ima gumbi. Broj odabira: " + currentChoices.Count);
        }
        if (currentChoices.Count == choiceButtons.Length)
        {
            isChoosing = true;
        }
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            //choiceButtons[index].gameObject.SetActive(true);
            StartCoroutine(ShowChoice(index));
            choiceTexts[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
        if(index > 0)
            StartCoroutine(SelectFirstChoice(index));
    }
    private IEnumerator ShowChoice(int index)
    {
        yield return new WaitForSecondsRealtime(showBubbleDelay);
        choiceButtons[index].gameObject.SetActive(true);
    }
    private IEnumerator SelectFirstChoice(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choiceButtons[index - 1].gameObject);
    }
    public void MakeChoice(int choiceIndex)
    {
        AudioManager.Instance.MadeChoice();
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
    public void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Nije se dobro parsao tag: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            switch (tagKey)
            {
                case KARL_TAG:
                    karl = tagValue;
                    if (karl == "yes")
                        dialogueIsPlaying = true;
                    else if (karl == "no")
                        dialogueIsPlaying = false;
                    break;
                case DEFAULT_TALKING_TAG:
                    defaultTalking = tagValue;
                    break;
                case DEFAULT_STANDING_TAG:
                    defaultStanding = tagValue;
                    break;
                case SPEAKER_TAG:
                    speaker = tagValue;
                    if (!naratorOnly)
                    {
                        Dialogue.Instance.CheckForNewSpeakers(speaker);
                        Dialogue.Instance.DefaultAnimationStates(speaker);
                    }
                    break;
                case BUBBLE_TAG:
                    InvisibleBubble = tagValue;
                    break;
                case ANIMATION_KARL_TAG: 
                    karlAnimationChange = tagValue;
                    Karl_AnimationManager.Instance.SetCharacterState(karlAnimationChange);
                    break;
                case ANIMATION_KANDINSKY_TAG: 
                    kandinskyAnimationChange = tagValue;
                    Kandinsky_AnimationManager.Instance.SetCharacterState(kandinskyAnimationChange);
                    break;
                case ANIMATION_GAMEPAD_TAG:
                    gamepadAnimationChange = tagValue;
                    Gamepad_AnimationManager.Instance.SetCharacterState(gamepadAnimationChange);
                    break;
                case ANIMATION_GAMEPADUI_TAG:
                    gamepadUIAnimationChange = tagValue;
                    GamepadUI_AnimationManager.Instance.SetCharacterState(gamepadUIAnimationChange);
                    break;
                case ANIMATION_JOHANN_TAG:
                    johannAnimationChange = tagValue;
                    Johann_AnimationManager.Instance.SetCharacterState(johannAnimationChange);
                    break;
                case ANIMATION_GUARD_TAG:
                    guardAnimationChange = tagValue;
                    Guard_AnimationManager.Instance.SetCharacterState(guardAnimationChange);
                    break;
                case ANIMATION_GUARD2_TAG:
                    guard2AnimationChange = tagValue;
                    Guard2_AnimationManager.Instance.SetCharacterState(guard2AnimationChange);
                    break;
                case ANIMATION_GUARD3_TAG:
                    guard3AnimationChange = tagValue;
                    Guard3_AnimationManager.Instance.SetCharacterState(guard3AnimationChange);
                    break;
                case ANIMATION_GUARD4_TAG:
                    guard4AnimationChange = tagValue;
                    Guard4_AnimationManager.Instance.SetCharacterState(guard4AnimationChange);
                    break;
                case ANIMATION_BEGGAR_TAG:
                    beggarAnimationChange = tagValue;
                    Beggar_AnimationManager.Instance.SetCharacterState(beggarAnimationChange);
                    break;
                case ANIMATION_CAT_TAG:
                    catAnimationChange = tagValue;
                    Cat_AnimationManager.Instance.SetCharacterState(catAnimationChange);
                    break;
                case ANIMATION_PROSTITUTE_TAG:
                    prostituteAnimationChange = tagValue;
                    Prostitute_AnimationManager.Instance.SetCharacterState(prostituteAnimationChange);
                    break;
                case ANIMATION_SHOPKEEPER_TAG:
                    shopkeeperAnimationChange = tagValue;
                    Shopkeeper_AnimationManager.Instance.SetCharacterState(shopkeeperAnimationChange);
                    break;
                case ANIMATION_WAGON_LUGGAGE_TAG:
                    wagonLuggageAnimationChange = tagValue;
                    Wagon_Luggage_AnimationManager.Instance.SetCharacterState(wagonLuggageAnimationChange);
                    break;
                case ANIMATION_WSTREET_DRAWME_TAG:
                    wStreetDrawMeAnimationChange = tagValue;
                    WStreet_DrawMe_AnimationManager.Instance.SetCharacterState(wStreetDrawMeAnimationChange);
                    break;
                case KARL_TEXT_POSITION_TAG: karlTextPosition = tagValue; break;
                case TEXT_POSITION_TAG: textPosition = tagValue; break;
               
                case MODE_TAG: dialogueMode = tagValue; break;
                case TIMER_TAG:
                    if (tagValue != "target" && tagValue != "" && tagValue != null && tagValue != "0" && tagValue != "audio")
                    {
                        timerTime = float.Parse(tagValue) / 100;
                        isTimer = true;
                        StartCoroutine(TimerControl());
                    }
                    else if (tagValue == "target")
                    {
                        isTimer = true;
                        targetTimer = tagValue;
                    }
                    else if (tagValue == "audio")
                    {
                        Debug.Log("audio timer ink");
                        isTimer = true;
                        audioTimer = true;
                        audioTimerOneShot = true;
                    }
                    break;
                case SPEED_TAG:
                    if(tagValue != null && tagValue != "" && tagValue != "0")
                        inkWalkingSpeed = float.Parse(tagValue) / 100;
                break;
                case GOTO_TAG:
                    goTo = tagValue;
                    break;
                case TARGET_TAG:
                    target = tagValue;
                    goForAWalk = true;
                    SceneManagerScript.Instance.GoForAWalk();
                    if (target == "karl")
                        dialogueIsPlaying = true;
                    if (timerTime != 0)
                        Dialogue.Instance.RemoveFromSpeakers(target);
                    break;
                
                case CAMERA_TAG:
                    sceneCamera = tagValue;
                    SceneManagerScript.Instance.ChangeCamera();
                    break;
                case LOAD_TAG:
                    loadScene = tagValue;
                    break;
                case CLEAR_TAG:
                    clearBubbles = tagValue;
                    if (clearBubbles == "text")
                    {
                        Dialogue.Instance.CloseAllBubbles();
                        Dialogue.Instance.ClearBubblesList();
                        clearBubbles = "";
                    }
                    else if (clearBubbles == "panels")
                    {
                        ClosePanels();
                        clearBubbles = "";
                    }
                    break;
                case FLIP_TAG:
                    flipTarget = tagValue;
                    SceneManagerScript.Instance.FlipACharacter();
                    break;
                case CURRENT_POSITION_TAG:
                    currentPosition = int.Parse(tagValue);
                    GameObject.FindGameObjectWithTag("karl").GetComponent<Karl_MoveRestricted>().currentPosition = currentPosition;
                    break;
                case AUDIO_FX_TAG:
                    audioFX = tagValue;
                    AudioManager.Instance.PlayFXFromInk(audioFX);
                    break;
                case AUDIO_TAG:
                    audioMusic = tagValue;
                    AudioManager.Instance.PlayMusicFromInk(audioMusic);
                    break;
                default:
                    Debug.LogWarning("Tag je dosao ali nije handlan" + tag);
                    break;
            }
        }
    }
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null; " + variableValue);
        }
        return variableValue;
    }
    private void Monologue()
    {
        ClosePanels();
        AudioManager.Instance.TextOnPanel();
        if(textPosition == "up")
        {
            upPanel.gameObject.SetActive(true);
            TMP_Text speechText = upPanel.GetComponentInChildren<TMP_Text>();
            ColorText(speechText);
            copyMainText = mainText;
            skippingSpeechBubble = speechText;
            displayLineCoroutine = StartCoroutine(DisplayText(speechText));
            StartCoroutine(NaratorDelaySpeechFix());
        }
        else if (textPosition == "down")
        {
            downPanel.gameObject.SetActive(true);
            TMP_Text speechText = downPanel.GetComponentInChildren<TMP_Text>();
            ColorText(speechText);
            copyMainText = mainText;
            skippingSpeechBubble = speechText;
            displayLineCoroutine = StartCoroutine(DisplayText(speechText));
            StartCoroutine(NaratorDelaySpeechFix());
        }
        else
        {
            downPanel.gameObject.SetActive(true);
            TMP_Text speechText = downPanel.GetComponentInChildren<TMP_Text>();
            ColorText(speechText);
            copyMainText = mainText;
            skippingSpeechBubble = speechText;
            displayLineCoroutine = StartCoroutine(DisplayText(speechText));
            StartCoroutine(NaratorDelaySpeechFix());
        }
    }
    private IEnumerator FinishMonologueText()
    {
        StopCoroutine(displayLineCoroutine);
        if(textPosition == "up")
        {
            TMP_Text speechText = upPanel.GetComponentInChildren<TMP_Text>();
            ColorText(speechText);
            speechText.text = mainText;
        }
        else if(textPosition == "down")
        {
            TMP_Text speechText = downPanel.GetComponentInChildren<TMP_Text>();
            ColorText(speechText);
            speechText.text = mainText;
        }
        else
        {
            TMP_Text speechText = upPanel.GetComponentInChildren<TMP_Text>();
            ColorText(speechText);
            speechText.text = mainText;
        }
        yield return new WaitForSeconds(0.5f);
        delayingNaratorPanel = false;
    }
    private void ClosePanels()
    {
        upPanel.gameObject.SetActive(false);
        downPanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// The Bubble is pooled from the SpeechBubblePool script and the main text is added to it. The bubble is send to the Dialogue script
    /// </summary>
    private void CreateBubble()
    {
        var bubble = SpeechBubblePool.Instance.Get();
        bubble.gameObject.SetActive(true);
        speechBubble = bubble.gameObject;
        TMP_Text speechText = speechBubble.GetComponentInChildren<TMP_Text>();
        ColorText(speechText);
        copyMainText = mainText;
        skippingSpeechBubble = speechText;
        displayLineCoroutine = StartCoroutine(DisplayText(speechText));
        speechText.ForceMeshUpdate();
        Vector2 textSize = speechText.GetRenderedValues(false);
        Vector2 padding = new Vector2(0.5f, 0.25f);
        SpriteRenderer background = speechBubble.transform.Find("Background").GetComponent<SpriteRenderer>();
        background.size = textSize + padding;
        speechBubble.SetActive(false);
        StartCoroutine(DelaySpeechBubble(speechBubble));
        Dialogue.Instance.RecieveSpeechBubble(speechBubble);
    }
    IEnumerator DelaySpeechBubble(GameObject speechnubble)
    {
        yield return new WaitForSeconds(showBubbleDelay);
        if (InvisibleBubble != "invisible")
        {
            AudioManager.Instance.NextText();
            speechBubble.SetActive(true);
        }
        delayingSpeechBubble = false;
        delayingNaratorPanel = false;
        firstSentanceBetweenTwoInksNotShowingSolutionBool = false;
        InvisibleBubble = "";
    }
    IEnumerator NaratorDelaySpeechFix()
    {
        yield return new WaitForSeconds(enableNaratorToContinueTime);
        delayingSpeechBubble = false;
    }
    private void ColorText(TMP_Text speechText)
    {
        if (speaker == "karl")
            speechText.color = new Color32(240, 234, 148, 255);
        else if (speaker == "kandinsky")
            speechText.color = new Color32(95, 213, 234, 255);
        else if (speaker == "johann")
            speechText.color = new Color32(251, 125, 100, 255);
        else if (speaker == "narator")
            speechText.color = new Color32(240, 234, 148, 255);
        else if (speaker == "beggar")
            speechText.color = new Color32(209, 98, 54, 255);
        else if (speaker == "prostitute")
            speechText.color = new Color32(245, 66, 66, 255);
        else if (speaker == "shopkeeper")
            speechText.color = new Color32(163, 103, 168, 255);
        else if (speaker == "guard")
            speechText.color = new Color32(66, 135, 245, 255);
        else if (speaker == "guard2")
            speechText.color = new Color32(66, 135, 245, 255);
        else if (speaker == "guard3")
            speechText.color = new Color32(66, 135, 245, 255);
        else if (speaker == "guard4")
            speechText.color = new Color32(66, 135, 245, 255);
        else if (speaker == "cat")
            speechText.color = new Color32(129, 134, 166, 255);


    }
    /// <summary>
    /// The IEnumerator which blocks the player's ability to change the sentance in the dialogue. It can only be changed via timer.
    /// </summary>
    /// <returns></returns>
    IEnumerator TimerControl()
    {
        yield return new WaitForSeconds(timerTime);
        isTimer = false;
        timerTime = 0;
        ContinueStory();
        
    }
    //public void WalkingTimer(int index)
    //{
    //    StartCoroutine(TimerForWalk(index));
    //}
    public void WalkingTimer()
    {
        StartCoroutine(TimerForWalk());
    }
    IEnumerator TimerForWalk(/*int index*/)
   {             
        //while (SceneManagerScript.Instance.walkingBooleans[index - 1])
        while (SceneManagerScript.Instance.isWalking)
        {
            yield return new WaitForEndOfFrame();
        }
        isTimer = false;
        targetTimer = "";
        ContinueStory();
    }
    IEnumerator DisplayText(TMP_Text text)
    {
        string displayText;
        text.text = "";
        int alphaIndex = 0;
        foreach (char c in mainText.ToCharArray())
        {
            alphaIndex++;
            text.text = mainText;
            displayText = text.text.Insert(alphaIndex, alphaCode);
            text.text = displayText;
            yield return new WaitForSeconds(.1f / textSpeed);
        }
        yield return null;
        delayingNaratorPanel = false;
    }
    private void FillSpeechBubble(TMP_Text bubbleText, string text)
    {
        if (displayLineCoroutine != null)
            StopCoroutine(displayLineCoroutine);
        bubbleText.text = text;
    }
    //private void CreateBubbleAfterChoice(int choiceIndex)
    //{
    //    var bubble = SpeechBubblePool.Instance.Get();
    //    speechBubble = bubble.gameObject;
    //    TMP_Text speechText = speechBubble.gameObject.GetComponentInChildren<TMP_Text>();
    //    speechText.text = choiceTexts[choiceIndex].text;
    //    speakerExchange = speaker;
    //    speaker = "karl";
    //    Dialogue.Instance.RecieveSpeechBubble(speechBubble);

    //}
    public void PlaceBubblesCorrections(DialogueBubblesPlacement dialogueBubblesPlacement)
    {
        Dialogue.Instance.sendBubbleUpCorrection = dialogueBubblesPlacement.sendBubbleUpCorrection;
        Dialogue.Instance.speechBubbleRightCorrection = dialogueBubblesPlacement.speechBubbleRightCorrection;
        Dialogue.Instance.speechBubbleYCorrection = dialogueBubblesPlacement.speechBubbleYCorrection;
        Dialogue.Instance.speechBubbleLeftCorrection = dialogueBubblesPlacement.speechBubbleLeftCorrection;
        Dialogue.Instance.karlSpeechBubbleRightCorrection = dialogueBubblesPlacement.karlSpeechBubbleRightCorrection;
        Dialogue.Instance.karlSpeechBubbleYCorrection = dialogueBubblesPlacement.karlSpeechBubbleYCorrection;
        Dialogue.Instance.karlSpeechBubbleLeftCorrection = dialogueBubblesPlacement.karlSpeechBubbleLeftCorrection;
        defaultTalking = "talking";
        defaultStanding = "normal_standing";
    }
    public void SaveVariables()
    {
        dialogueVariables.SaveVariables();
    }
    public void CloseDialogueRunningInBackground()
    {
        if (currentStory.canContinue)
            ExitDialogueMode();
    }
    private void ContinueStoryDelay()
    {
        isTimer = false;
        audioTimer = false;
        Debug.Log("audio timer off");
        ContinueStory();
     
    }
    private void Update()
    {
       
        if (dialogueIsPlaying && !isChoosing && !isTimer && !delayingSpeechBubble && !firstSentanceBetweenTwoInksNotShowingSolutionBool && !delayingNaratorPanel)
        {
            if(dialogueManagerButton.WasPressedThisFrame())
            {
                ContinueStory();
            }       
        }
        if (dialogueIsPlaying && !isChoosing && !isTimer && !delayingSpeechBubble && !firstSentanceBetweenTwoInksNotShowingSolutionBool && delayingNaratorPanel)
        {
            if (dialogueManagerButton.WasPressedThisFrame() && oneShot)
            {
                oneShot = false;
                StartCoroutine(FinishMonologueText());
            }


        }
        if(audioTimer)
        {
            if(!AudioManager.Instance.audioSourceFX.isPlaying && audioTimerOneShot)
            {
                audioTimerOneShot = false;
                Debug.Log("audio timer update");
                Invoke("ContinueStoryDelay", 0.5f);
            }
        }
    }
}
