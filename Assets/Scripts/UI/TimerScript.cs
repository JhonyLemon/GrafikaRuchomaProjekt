using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [HideInInspector]
    public bool isStarted = false;
    [HideInInspector]
    public long timeSinceStart = 0;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceStart = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            if (Input.anyKeyDown)
            {
                timeSinceStart = 0;
                isStarted = true;
            }
        }
        timeSinceStart += (int)(Time.deltaTime * 1000);

        if (text != null)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(timeSinceStart);
            text.text = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds,
                                    t.Milliseconds);
        }
    }
}
