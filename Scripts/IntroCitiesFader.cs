using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCitiesFader : MonoBehaviour
{
    [SerializeField] private GameObject frame, city;
    [SerializeField] private float fadeOutTime, fadeInTime, inbetweenTime, startDelay;
    private void Start()
    {
        StartCoroutine(FrameFadeIn());
        StartCoroutine(CityFadeIn());
        StartCoroutine(FrameFadeOut());
        StartCoroutine(CityFadeOut());
    }
    private IEnumerator FrameFadeIn()
    {
        yield return new WaitForSeconds(startDelay);
        var color = frame.gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        frame.gameObject.GetComponent<SpriteRenderer>().color = color;
        frame.gameObject.SetActive(true);

        while (color.a < 1)
        {
            color.a += Time.deltaTime / fadeInTime;
            frame.gameObject.GetComponent<SpriteRenderer>().color = color;
            if (color.a >= 1)
                color.a = 1;
            yield return null;
        }
    }
    private IEnumerator CityFadeIn()
    {
        yield return new WaitForSeconds(startDelay);
        var color = city.gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        city.gameObject.GetComponent<SpriteRenderer>().color = color;
        city.gameObject.SetActive(true);

        while (color.a < 1)
        {
            color.a += Time.deltaTime / fadeInTime;
            city.gameObject.GetComponent<SpriteRenderer>().color = color;
            if (color.a >= 1)
                color.a = 1;
            yield return null;
        }
    }
    private IEnumerator FrameFadeOut()
    {
        yield return new WaitForSeconds(startDelay + inbetweenTime + fadeInTime);
        var color = frame.gameObject.GetComponent<SpriteRenderer>().color;
            color.a = 2;
            while (color.a > 0)
            {
                color.a -= Time.deltaTime / fadeOutTime;
                frame.gameObject.GetComponent<SpriteRenderer>().color = color;
                if (color.a <= 0)
                    color.a = 0;
                yield return null;
            }
        frame.gameObject.SetActive(false);
    }
    private IEnumerator CityFadeOut()
    {
        yield return new WaitForSeconds(startDelay + inbetweenTime + fadeInTime);
        var color = city.gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 2;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime / fadeOutTime;
            city.gameObject.GetComponent<SpriteRenderer>().color = color;
            if (color.a <= 0)
                color.a = 0;
            yield return null;
        }
        city.gameObject.SetActive(false);
    }
}
