using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    // UI Elements
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timeElapsed;
    [SerializeField] private Image dangerMeter;

    // Necessary Info
    [SerializeField] private Light_Collision playerLight;

    [Range(.16f, .82f)]
    private float danger = .16f;

    void Start()
    {
        GameManager.Instance.gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        updateScoreboard();
        updateTimeElapsed();
        updateDangerMeter();
    }

    private void updateDangerMeter()
    {
        dangerMeter.fillAmount = math.remap(0, playerLight.timeToBeCaught, .16f, .82f, playerLight.lightMeter);
    }

    private void updateTimeElapsed()
    {
        string time = "";
        if(GameManager.Instance.elapsedMinutes < 10)
        {
            time += "0";
        }
        time += GameManager.Instance.elapsedMinutes + ":";

        if (Mathf.Round(GameManager.Instance.elapsedSeconds) < 10)
        {
            time += "0";
        }
        time += Mathf.Round(GameManager.Instance.elapsedSeconds);
        timeElapsed.text = time;
    }

    private void updateScoreboard()
    {
        string scorestring = "";
        int digCount = scoreDigitCount(); // Get the number of digits in the score

        if (digCount < 1)
        {
            scorestring = "000000000";
        }
        else
        {
            for (int i = 0; i < 9 - digCount; i++)
            {
                scorestring += "0";
            }
            scorestring += GameManager.Instance.currentScore;
        }
        
        if (GameManager.Instance.currentScore > 999999999)
        {
            scorestring = "999999999";
        }

        score.text = scorestring;
    }

    private int scoreDigitCount()
    {
        int cScore = GameManager.Instance.currentScore;
        int digitCount = 0;
        while(cScore > 0)
        {
            cScore = cScore / 10;
            digitCount++;
        }

        return digitCount;
    }
}
