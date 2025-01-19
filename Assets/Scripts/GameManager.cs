using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton instance
    private static GameManager _instance;

    //TODO:Make sure to clear out these test vals before build

    public int currentScore = 0;     // Score in a current round
    public int currentMult = 5;    // Multiplier in a current round
    public List<int> highScoreList = new List<int> { 0, 0, 0, 0, 0, 0 };     // Top 6 high scores
    public float elapsedSeconds = 0; // Time elapsed in a given game 
    public float elapsedMinutes = 0;
    public float elapsedSneakySeconds = 0;

    private float elapsedSecondsSeverity = 0;
    private float severityIncrementTimeBenchmark = 10f; 
    public int severityLevel = 0;
    public int lightCount;

    public bool gameActive;

    // A severity level is added every 10 seconds.
    // Collecting a round of trash adds a severity level.
    // 6 severity levels a minute means that at 5 minutes, or level 30, everything should be active.
    // Player action will probably get this figure down to a healthy 3 minutes? Or that's the goal.

    public static GameManager Instance
    {
        get
        {
            // setup GameManager as a singleton class
            if (_instance == null)
            {
                // create new game manager object
                GameObject newManager = new();
                newManager.name = "Game Manager";
                newManager.AddComponent<GameManager>();
                DontDestroyOnLoad(newManager);
                _instance = newManager.GetComponent<GameManager>();
                
            }
            return _instance;
        }
    }

    private void Update() {
        if (gameActive)
        {
            elapsedSeconds += Time.deltaTime;
            elapsedSecondsSeverity += Time.deltaTime;
            
            if(Mathf.Round(GameManager.Instance.elapsedSeconds) == 60)
            {
                elapsedMinutes++;
                elapsedSeconds = 0;
            }

            if(elapsedSecondsSeverity > severityIncrementTimeBenchmark)
            {
                severityLevel++;
                elapsedSecondsSeverity = 0;        
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("0_Start");
        }
    
    }

    public void AddToScore(int points)
    {
        currentScore += points;
    }

    public void AddToLightCount()
    {
        lightCount++;
    }

    public void SubtractFromLightCount()
    {
        lightCount--;
        if(lightCount < 0)
        {
            lightCount = 0;
        }
    }

    public void ResetGame()
    {
        currentScore = 0;
        elapsedSeconds = 0;
        elapsedMinutes = 0;
        elapsedSneakySeconds = 0;
        currentMult = 5;
        severityLevel = 0;
    }

    public bool checkHighScore(int score)
    {
        bool isHigh = false;
        for(int i = 5; i >= 0; i--)
        {
            if(score > highScoreList[i])
            {
                isHigh = true;
                if(i == 5)
                {
                    highScoreList[i] = score;
                }
                else
                {
                    highScoreList[i+1] = highScoreList[i];
                    highScoreList[i] = score;
                }
            }
        }
        return isHigh;
    }


    
}
