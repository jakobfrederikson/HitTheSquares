using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public static float roundTimer = 4;
    private int seconds;

    // Update is called once per frame
    void Update()
    {
        if(roundTimer > 0)
        {
            roundTimer -= Time.deltaTime;
            seconds = Mathf.RoundToInt(roundTimer);
        }
        else
        {
            GameManager.roundIsOver = true;
        }
        timerText.text = seconds.ToString();
    }
}
