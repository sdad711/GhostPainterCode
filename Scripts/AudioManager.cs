using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceText;
    public AudioSource audioSourceFX;
    [SerializeField] private AudioClip[] audioClipsText;
    [SerializeField] private AudioClip[] audioClipsFX;
    [SerializeField] private AudioClip[] audioClipsMusic;
    [SerializeField] private float fadeOutTime, fadeOutFromINKTime, fadeInTime;
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public void NextText()
    {
        foreach (AudioClip clip in audioClipsText)
        {
            if (clip.name == "text_next") 
            audioSourceText.PlayOneShot(clip);
        }
    }
    public void TextOnPanel()
    {
        foreach (AudioClip clip in audioClipsText)
        {
            if (clip.name == "heartbeat") 
            audioSourceText.PlayOneShot(clip);
        }
    }
    public void MadeChoice()
    {
        foreach (AudioClip clip in audioClipsText)
        {
            if (clip.name == "text_choice_select") 
            audioSourceText.PlayOneShot(clip);
        }
    }
    public void OpenDoor()
    {
        foreach (AudioClip clip in audioClipsFX)
        {
            if (clip.name == "door_open") 
            audioSourceFX.PlayOneShot(clip);
        }
    }
    public void BreakGlass()
    {
        foreach (AudioClip clip in audioClipsFX)
        {
            if (clip.name == "glass_break") 
            audioSourceFX.PlayOneShot(clip);
        }
    }
    public void BalconyDrop()
    {
        foreach (AudioClip clip in audioClipsFX)
        {
            if (clip.name == "balcony_drop")
                audioSourceFX.PlayOneShot(clip);
        }
    }
    public void GunCock()
    {
        foreach (AudioClip clip in audioClipsFX)
        {
            if (clip.name == "gun_cock")
                audioSourceFX.PlayOneShot(clip);
        }
    }
    public void GunShot()
    {
        foreach (AudioClip clip in audioClipsFX)
        {
            if (clip.name == "gun")
                audioSourceFX.PlayOneShot(clip);
        }
    }
    public void Whistle()
    {
        foreach (AudioClip clip in audioClipsFX)
        {
            if (clip.name == "whistle")
                audioSourceFX.PlayOneShot(clip);
        }
    }
    public void PlayFXFromInk(string clipName)
    {
        foreach (AudioClip clip in audioClipsFX)
        {
            if (clip.name == clipName)
                audioSourceFX.PlayOneShot(clip);
        }
    }
    public void PlayMusicFromInk(string clipName)
    {
        FadeOutMusicFromInk();
        StartCoroutine(PlayingINKMusicAfterFadeOut(clipName));
    }
    private IEnumerator PlayingINKMusicAfterFadeOut(string clipName)
    {
        yield return new WaitForSeconds(fadeOutFromINKTime + 0.5f);
        foreach (AudioClip clip in audioClipsMusic)
        {
            if (clip.name == clipName)
            {
                audioSourceMusic.clip = clip;
                audioSourceMusic.loop = true;
                audioSourceMusic.volume = 1;
                audioSourceMusic.Play();
            }
        }
    }
    public void PlayMusic()
    {
        audioSourceMusic.Play();
    }
    public void FadeOutMusic()
    {
        StartCoroutine(FadeOutMusicCoroutine(fadeOutTime));
    }
    private void FadeOutMusicFromInk()
    {
        StartCoroutine(FadeOutMusicCoroutine(fadeOutFromINKTime));
    }
    IEnumerator FadeOutMusicCoroutine(float fadeOutTime)
    {
        var volume = audioSourceMusic.volume;
        while (volume > 0)
        {
            volume -= Time.deltaTime / fadeOutTime;
            audioSourceMusic.volume = volume;
            if (volume <= 0)
                volume = 0;
            yield return null;
        }
    }
    public void FadeInMusic()
    {
        StartCoroutine(FadeInMusicCoroutine());
    }
    IEnumerator FadeInMusicCoroutine()
    {
        audioSourceMusic.volume = -1;
        var volume = audioSourceMusic.volume;
        while (volume < 1)
        {
            volume += Time.deltaTime / fadeInTime;
            audioSourceMusic.volume = volume;
            if (volume >= 1)
                volume = 1;
            yield return null;
        }
    }
}
