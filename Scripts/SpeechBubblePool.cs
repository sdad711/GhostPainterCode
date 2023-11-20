using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubblePool : MonoBehaviour
{
    /// <summary>
    /// The SpeechBubblePool script controls the pooling of speech bubbles in and out of the scene. It can be found on the DialogueManager in the Hyerarchy.
    /// </summary>

    [SerializeField] private SpeechBubble speechBubblePrefab;
    private Queue<SpeechBubble> speechBubbles = new Queue<SpeechBubble>();

    public static SpeechBubblePool Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public SpeechBubble Get()
    {
        if (speechBubbles.Count == 0)
        {
            AddSpeechBubble(1);
        }
        return speechBubbles.Dequeue();
    }
    private void AddSpeechBubble(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpeechBubble speechBubble = Instantiate(speechBubblePrefab);
            speechBubble.gameObject.SetActive(false);
            speechBubbles.Enqueue(speechBubble);
        }
    }
    public void ReturnToPool(SpeechBubble speechBubble)
    {
        speechBubble.gameObject.SetActive(false);
        speechBubbles.Enqueue(speechBubble);
    }
}

