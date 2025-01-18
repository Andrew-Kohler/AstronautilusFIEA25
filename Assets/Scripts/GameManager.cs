using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton instance
    private static GameManager _instance;

    public int currentScore;     // Score in a current round
    List<int> highScoreList;     // Top 10 high scores
    public float elapsedSeconds; // Time elapsed in a given game in seconds

    public bool gameActive;

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
        }
    
    }

    public void AddToScore(int points)
    {
        currentScore += points;
    }

    public void ResetGame()
    {
        currentScore = 0;
        elapsedSeconds = 0;
    }


    
}
