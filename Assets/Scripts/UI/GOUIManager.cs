using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GOUIManager : MonoBehaviour
{
    // UI Elements
    [SerializeField] private TextMeshProUGUI baseScore;
    [SerializeField] private TextMeshProUGUI timeBonus;
    [SerializeField] private TextMeshProUGUI timeMult;
    [SerializeField] private TextMeshProUGUI sneakyBonus;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private GameObject highScoreTXT;
    void Start()
    {
        StartCoroutine(DoGameOverSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("2_Game");
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("0_Start"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator DoGameOverSequence()
    {
        // Start on black

        // Do our math
        int timeBonusVal = (int)(GameManager.Instance.elapsedMinutes * 60 + Mathf.Round(GameManager.Instance.elapsedSeconds));
        int timeMultVal = (int)(GameManager.Instance.elapsedMinutes + 1); // Just minutes lived + 1
        int sneakyMultVal = (int)(GameManager.Instance.elapsedSneakySeconds / 60) + 1; // SNEAKY minutes lived + 1
        int finalScoreVal = (GameManager.Instance.currentScore + timeBonusVal) * timeMultVal * sneakyMultVal;

        // Count up from 0 to show the score
        int tempScore = 0;
        while(tempScore < GameManager.Instance.currentScore)
        {
            /*if (GameManager.Instance.currentScore - tempScore > 100000)
            {
                tempScore += 100000;
            }*/
            /*else if (GameManager.Instance.currentScore - tempScore > 10000)
            {
                tempScore += 10000;
            }*/
            /*if (GameManager.Instance.currentScore - tempScore > 1000)
            {
                tempScore += 1000;
            }*/
            if (GameManager.Instance.currentScore - tempScore > 100)
            {
                tempScore += 100;
            }
            else if (GameManager.Instance.currentScore - tempScore > 10)
            {
                tempScore += 10;
            }
            else
            {
                tempScore += 1;
            }

            baseScore.text = tempScore.ToString();
            yield return null;
        }
        baseScore.color = Color.white;

        yield return new WaitForSeconds(1f);
        
        // Count up from 0 to show the time bonus
        int tempTB = 0;
        while (tempTB < timeBonusVal)
        {
            /*if (GameManager.Instance.currentScore - tempScore > 100)
            {
                tempTB += 100;
            }*/
            if (timeBonusVal - tempTB > 10)
            {
                tempTB += 10;
            }
            else
            {
                tempTB += 1;
            }

            timeBonus.text = tempTB.ToString();
            yield return null;
        }
        timeBonus.color = Color.white;


        yield return new WaitForSeconds(1f);

        // Count up from 0 for the time mult
        int tempTM = 0;
        while (tempTM < timeMultVal)
        {
            tempTM += 1;
            timeMult.text = "x" + tempTM.ToString();
            yield return new WaitForSeconds(.3f);
        }
        timeMult.color = Color.white;
        yield return new WaitForSeconds(1f);

        // Count up from 0 for the sneaky bonus
        int tempSN = 0;
        while (tempSN < sneakyMultVal)
        {
            tempSN += 1;
            sneakyBonus.text = "x" + tempSN.ToString();
            yield return new WaitForSeconds(.3f);
        }
        sneakyBonus.color = Color.white;
        yield return new WaitForSeconds(1f);

        // Tally up the whole score
        int tempFinal = 0;
        while (tempFinal < finalScoreVal)
        {
            if (GameManager.Instance.currentScore - tempScore > 10000)
            {
                tempScore += 10000;
            }
            if (GameManager.Instance.currentScore - tempScore > 1000)
            {
                tempScore += 1000;
            }
            if (finalScoreVal - tempFinal > 100)
            {
                tempFinal += 100;
            }
            else if (finalScoreVal - tempFinal > 10)
            {
                tempFinal += 10;
            }
            else
            {
                tempFinal += 1;
            }

            finalScore.text = tempFinal.ToString();
            yield return null;
        }
        finalScore.color = Color.white;
        yield return new WaitForSeconds(.3f);

        // Find out if this is a new high score
        if (GameManager.Instance.checkHighScore(finalScoreVal))
        {
            highScoreTXT.SetActive(true);
        }

        yield return null;
    }
}
