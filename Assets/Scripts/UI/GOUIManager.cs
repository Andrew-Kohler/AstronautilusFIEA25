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
        SceneManager.LoadScene("2_Game");
    }

    public void BackToMainMenu()
    {
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
        int timeBonus = GameManager.Instance.elapsedMinutes * 60 + Mathf.Round(GameManager.Instance.elapsedMinutes);

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
            yield return new WaitForSeconds(.0001f);
        }
        baseScore.color = Color.white;

        yield return new WaitForSeconds(2f);

        // Count up from 0 to show the time bonus
        int tempTB = 0;
        while (tempTB < ());
        {
            if (GameManager.Instance.currentScore - tempScore > 100)
            {
                tempTB += 100;
            }
            else if (GameManager.Instance.currentScore - tempScore > 10)
            {
                tempTB += 10;
            }
            else
            {
                tempTB += 1;
            }

            baseScore.text = tempScore.ToString();
            yield return new WaitForSeconds(.0001f);
        }
        // Count up from 0 for the time mult
        // Count up from 0 for the sneaky bonus
        // Tally up the whole score
        yield return null;
    }
}
