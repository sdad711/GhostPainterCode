using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;

public class Vitraj_Moving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    public TextAsset[] inkJSON;
    public TextAsset inkIntro;
    public TextAsset inkOutro;
    private float movementX, movementY;
    private int index = 0;
    Vector3 resetPosition;
    [SerializeField] private int INKOcassion;
    private int counter;
    private int INKIndex;
    public InputAction vitrajMoveRightTrigger;
    public InputAction vitrajMoveLeftTrigger;
    public static Vitraj_Moving Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        vitrajMoveLeftTrigger.Enable();
        vitrajMoveRightTrigger.Enable();
    }
    private void OnDisable()
    {
        vitrajMoveRightTrigger.Disable();
        vitrajMoveLeftTrigger.Disable();
    }
    private void Start()
    {
        resetPosition = items[index].gameObject.transform.position;
        StopGlow();
        items[index].transform.Find("Bloom").gameObject.SetActive(true);
        items[index].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        DialogueManager.Instance.EnterDialogueMode(inkIntro);
    }
    private void Update()
    {
        if (!DialogueManager.Instance.dialogueIsPlaying)
        {
            if (vitrajMoveLeftTrigger.WasPressedThisFrame())
            {
                PrevItem();
            }
            if (vitrajMoveRightTrigger.WasPressedThisFrame())
            {
                NextItem();
            }
            Move();
        }
    }
    private void Move()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");
        if(items.Count != 0)
            items[index].gameObject.transform.position += new Vector3(movementX * speed, movementY * speed, 0) * Time.deltaTime;
        //if(Input.GetKeyDown(KeyCode.Space) && !sweetPoint)
        //{
        //    items[index].gameObject.transform.position = resetPosition;
        //}
    }
    private void StopGlow()
    {
        foreach (GameObject item in items)
        {
            item.transform.Find("Bloom").gameObject.SetActive(false);

            //var bloom = item.gameObject.GetComponentInChildren<SpriteRenderer>();
            //bloom.gameObject.SetActive(false);
        }

    }
    public void RemoveFromList(GameObject item)
    {
        items.Remove(item);
        Counter();
        CheckIfFinished();
    }
    public void PrevItem()
    {
        items[index].gameObject.transform.position = resetPosition;
        items[index].gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if (index <= 0)
            index = items.Count - 1;
        else
            index--;
        resetPosition = items[index].gameObject.transform.position;
        StopGlow();
        items[index].transform.Find("Bloom").gameObject.SetActive(true);
        items[index].gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void NextItem()
    {
        items[index].gameObject.transform.position = resetPosition;
        items[index].gameObject.GetComponent<SpriteRenderer>().enabled = true;
        if (index >= items.Count - 1)
            index = 0;
        else
            index++;
        resetPosition = items[index].gameObject.transform.position;
        StopGlow();
        items[index].transform.Find("Bloom").gameObject.SetActive(true);
        items[index].gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void Counter()
    {
        counter++;
        if(counter == INKOcassion)
        {
            counter = 0;
            StartConversation();
        }
    }
    private void StartConversation()
    {
        DialogueManager.Instance.EnterDialogueMode(inkJSON[INKIndex]);
        INKIndex++;
        if (INKIndex > inkJSON.Length)
            INKIndex = inkJSON.Length;
    }
    private void CheckIfFinished()
    {
        if (items.Count != 0)
            return;
        else if (items.Count == 0)
          DialogueManager.Instance.EnterDialogueMode(inkOutro);
    }
    public void SelectItem()
    {
        if (items.Count != 0)
        {
            index = Random.Range(0, items.Count - 1);
            resetPosition = items[index].gameObject.transform.position;
            StopGlow();
            items[index].transform.Find("Bloom").gameObject.SetActive(true);
            items[index].gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
