using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject camera2, camera3;
    [SerializeField] private float loadSceneDelay;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private float introFadeOutTime, exitFadeInTime, shaderIntroFadeOutTime, shaderExitFadeInTime;
    //[HideInInspector] public List<bool> walkingBooleans = new List<bool>();
    [HideInInspector] public bool isWalking;
    [SerializeField] private bool introFadeOutShader;
    [SerializeField] private bool exitFadeInShader;
    [SerializeField] private bool noIntroFadeOut, noExitFadeIn, noMusicExitFadeOut;
    [SerializeField] private bool carryingAudioManager;
    [SerializeField] private float delayMusicOnStart;
    [SerializeField] private bool destroyCarryingAudioManager;
    [SerializeField] private bool noDialogue;
    private string restart;


    public static SceneManagerScript Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        restart = PlayerPrefs.GetString("Restart");
        var sceneName = SceneManager.GetActiveScene().name;
        if (restart == "yes" && sceneName == "weimar_square_stealth")
        {
            GameObject karl = GameObject.FindGameObjectWithTag("karl");
            karl.GetComponent<Karl_MoveRestricted>().currentPosition = 1;
        }
    }
    private void Start()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        if (restart == "yes")
        {
            introFadeOutShader = true;
            if (sceneName == "weimar_square_stealth")
            {
                Guard2_AnimationManager.Instance.startDelay = 0;
                Guard_AnimationManager.Instance.startDelay = 10;
            }
        }
        if(!noIntroFadeOut)
            StartCoroutine(IntroFadeOut());
        if(!carryingAudioManager)
            Invoke("PlayMusic", delayMusicOnStart);
        if(destroyCarryingAudioManager)
        {
            Destroy(GameObject.FindWithTag("carryingAudioManager"));
        }
    }
    IEnumerator IntroFadeOut()
    {
        if (introFadeOutShader)
        {
            Material material = blackScreen.gameObject.GetComponent<SpriteRenderer>().material;
            float fade = 1;
            while (material.GetFloat("_Fade") > 0)
            {
                fade -= Time.deltaTime / shaderIntroFadeOutTime;
                material.SetFloat("_Fade", fade);
                if (material.GetFloat("_Fade") <= 0)
                    material.SetFloat("_Fade", 0);
                yield return null;
            }
            blackScreen.gameObject.SetActive(false);
        }
        else if (!introFadeOutShader)
        {
            var color = blackScreen.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 2;
            while (color.a > 0)
            {
                color.a -= Time.deltaTime / introFadeOutTime;
                blackScreen.gameObject.GetComponent<SpriteRenderer>().color = color;
                if (color.a <= 0)
                    color.a = 0;
                yield return null;
            }
            blackScreen.gameObject.SetActive(false);
        }
    }
    IEnumerator ExitFadeIn()
    {
        if (exitFadeInShader)
        {
            Material material = blackScreen.gameObject.GetComponent<SpriteRenderer>().material;
            float fade = 0;
            material.SetFloat("_Fade", fade);
            blackScreen.gameObject.SetActive(true);
            while (material.GetFloat("_Fade") < 1)
            {
                fade += Time.deltaTime / shaderExitFadeInTime;
                material.SetFloat("_Fade", fade);
                if (material.GetFloat("_Fade") >= 1)
                    material.SetFloat("_Fade", 1);
                yield return null;
            }
        }
        else if(!exitFadeInShader)
        {
            Material material = blackScreen.gameObject.GetComponent<SpriteRenderer>().material;
            float fade = 1;
            material.SetFloat("_Fade", fade);
            var color = blackScreen.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            blackScreen.gameObject.SetActive(true);
            while (color.a < 1)
            {
                color.a += Time.deltaTime / exitFadeInTime;
                blackScreen.gameObject.GetComponent<SpriteRenderer>().color = color;
                if (color.a >= 1)
                    color.a = 1;
                Debug.Log(color.a);
                yield return null;
            }
            
        }
    }
public void GoForAWalk()
    {
        GameObject target = GameObject.FindGameObjectWithTag(DialogueManager.Instance.target);
        string tag = target.tag;
        string upperTag = tag.Substring(0, 1).ToUpper() + tag.Substring(1).ToLower() + "_AnimationManager";
        GameObject waypoint = GameObject.FindGameObjectWithTag(DialogueManager.Instance.goTo);
        var goToSpeed = DialogueManager.Instance.inkWalkingSpeed;
        if (target.transform.position.x < waypoint.transform.position.x && target.transform.localScale.x < 0)
            target.transform.localScale = new Vector3(target.transform.localScale.x * -1, target.transform.localScale.y, target.transform.localScale.z);
        else if (target.transform.position.x > waypoint.transform.position.x && target.transform.localScale.x > 0)
            target.transform.localScale = new Vector3(target.transform.localScale.x * -1, target.transform.localScale.y, target.transform.localScale.z);
        StartCoroutine(Walking(target, waypoint, upperTag, goToSpeed));
    }
    IEnumerator Walking(GameObject target, GameObject waypoint, string upperTag, float goToSpeed)
    {
        //int index = walkingBooleans.Count;
        //bool currentlyWalkingBool = true;
        if (DialogueManager.Instance.targetTimer == "target")
        {
            //walkingBooleans.Add(currentlyWalkingBool);
            //index++;
            //walkingBooleans[index - 1] = currentlyWalkingBool;
            //DialogueManager.Instance.WalkingTimer(index);
            isWalking = true;
            DialogueManager.Instance.WalkingTimer();
        }
        while (target.transform.position != waypoint.transform.position)
        {
            target.transform.position = Vector2.MoveTowards(target.transform.position, waypoint.transform.position, goToSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //walkingBooleans[index - 1] = false;
        isWalking = false;
        if (upperTag == "Karl_AnimationManager")
            Karl_AnimationManager.Instance.SetCharacterState("normal_standing");
        if (upperTag == "Johann_AnimationManager")
            Johann_AnimationManager.Instance.SetCharacterState("idle_panic2");
        if (upperTag == "Guard_AnimationManager")
            Guard_AnimationManager.Instance.SetCharacterState("normal_standing");
        if (upperTag == "Guard2_AnimationManager")
            Guard2_AnimationManager.Instance.SetCharacterState("normal_standing");
        if (upperTag == "Guard3_AnimationManager")
            Guard3_AnimationManager.Instance.SetCharacterState("normal_standing");
        if (upperTag == "Guard4_AnimationManager")
            Guard4_AnimationManager.Instance.SetCharacterState("normal_standing");
        if (upperTag == "Cat_AnimationManager")
            Cat_AnimationManager.Instance.SetCharacterState("normal_standing");
        if (upperTag == "Shopkeeper_AnimationManager")
            Shopkeeper_AnimationManager.Instance.SetCharacterState("standing");
        //etc etc
    }
    public void FlipACharacter()
    {
        GameObject target = GameObject.FindGameObjectWithTag(DialogueManager.Instance.flipTarget);
        target.transform.localScale = new Vector3(target.transform.localScale.x * -1, target.transform.localScale.y, target.transform.localScale.z);
    }
    public void ChangeCamera()
    {
       if(DialogueManager.Instance.sceneCamera == "camera1")
       {
            DisableExtraCameras();
       }
       else if(DialogueManager.Instance.sceneCamera == "camera2")
       {
            DisableExtraCameras();
            camera2.SetActive(true);
       }
       else if(DialogueManager.Instance.sceneCamera == "camera3")
       {
            DisableExtraCameras();
            camera3.SetActive(true);
       }
       else
       {
            DisableExtraCameras();
       }
    }
    public void Camera1()
    {
        DisableExtraCameras();
    }
    public void Camera2()
    {
        DisableExtraCameras();
        camera2.SetActive(true);
    }
    public void Camera3()
    {
        DisableExtraCameras();
        camera3.SetActive(true);
    }
    private void DisableExtraCameras()
    {
        camera2.SetActive(false);
        camera3.SetActive(false);
    }
    public void LoadSceneFromInk(string sceneName)
    {
        PlayerPrefs.SetString("Restart", "no");
        LoadScene(sceneName);
    }
    public void LoadSceneFromGame(string sceneName)
    {
        PlayerPrefs.SetString("Restart", "no");
        LoadScene(sceneName);
    }
    public void RestartLevel()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("Restart", "yes");
        exitFadeInShader = true;
        LoadScene(sceneName);
    }
    private void LoadScene(string sceneName)
    {
        if(!noExitFadeIn)
            StartCoroutine(ExitFadeIn());
        if(!noMusicExitFadeOut)
            AudioManager.Instance.FadeOutMusic();
        if(!noDialogue)
            DialogueManager.Instance.SaveVariables();
        StartCoroutine(LoadingScene(sceneName));
    }
    IEnumerator LoadingScene(string sceneName)
    {
        yield return new WaitForSecondsRealtime(loadSceneDelay);
        SceneManager.LoadScene(sceneName);
    }

    private void PlayMusic()
    {
        AudioManager.Instance.PlayMusic();
        AudioManager.Instance.FadeInMusic();
    }

    
}
