using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{

    public float timer, refresh;
    private float avgFramerate;
    string display = "{0} FPS";
    private Text m_Text;
    public bool smoothDeltaTime, deltaTime, fixedDeltaTime, unscaledDeltaTime;

    private void Start()
    {
        m_Text = GetComponent<Text>();
    }


    private void Update()
    {
        if(smoothDeltaTime)
        {
            //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
            float timelapse = Time.smoothDeltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;

            if (timer <= 0) avgFramerate = (int)(1f / timelapse);
            m_Text.text = string.Format(display, avgFramerate.ToString());
        }
        if (deltaTime)
        {
            //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
            float timelapse = Time.deltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;

            if (timer <= 0) avgFramerate = (int)(1f / timelapse);
            m_Text.text = string.Format(display, avgFramerate.ToString());
        }
        if (fixedDeltaTime)
        {
            //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
            float timelapse = Time.fixedDeltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;

            if (timer <= 0) avgFramerate = (int)(1f / timelapse);
            m_Text.text = string.Format(display, avgFramerate.ToString());
        }
        if (unscaledDeltaTime)
        {
            //Change smoothDeltaTime to deltaTime or fixedDeltaTime to see the difference
            float timelapse = Time.unscaledDeltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;

            if (timer <= 0) avgFramerate = (int)(1f / timelapse);
            m_Text.text = string.Format(display, avgFramerate.ToString());
        }
    }
}