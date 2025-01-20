using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton instance
    private static GameManager _instance;

    //TODO:Make sure to clear out these test vals before build

    public int currentScore = 0;     // Score in a current round
    public int currentMult = 5;    // Multiplier in a current round
    public float elapsedSeconds = 0; // Time elapsed in a given game 
    public float elapsedMinutes = 0;
    public float elapsedSneakySeconds = 0;
    public bool runOver = false;

    private float elapsedSecondsSeverity = 0;
    private float severityIncrementTimeBenchmark = 10f; 
    public int severityLevel = 0;
    public int lightCount;

    // Save data
    

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
        runOver = false;
    }

    public bool checkHighScore(int score)
    {
        bool isHigh = false;
        for(int i = 5; i >= 0; i--)
        {
            if(score > _gamePersistent.highScoreList[i])
            {
                isHigh = true;
                if(i == 5)
                {
                    _gamePersistent.highScoreList[i] = score;
                    _gamePersistent.highScoreNames[i] = _gamePersistent.currentName;
                }
                else
                {
                    _gamePersistent.highScoreList[i+1] = _gamePersistent.highScoreList[i];
                    _gamePersistent.highScoreList[i] = score;

                    _gamePersistent.highScoreNames[i + 1] = _gamePersistent.highScoreNames[i];
                    _gamePersistent.highScoreNames[i] = _gamePersistent.currentName;
                }
            }
        }
        return isHigh;
    }

    #region GAME PERSISTENT DATA
    // permanent upgrades, settings, etc. (saved between sessions)
    [Serializable]
    public class GamePersistentData
    {
        public string currentName;
        public List<int> highScoreList;     // Top 6 high scores
        public List<string> highScoreNames;    // Top 6 high scorers
    }

    // private stored save data
    private GamePersistentData _gamePersistent;

    // public accessor for save data
    public GamePersistentData GamePersistent
    {
        get
        {
            // initialize if necessary and possible
            if (_gamePersistent == null)
            {
                InitializeSaveData();
            }

            return _gamePersistent;
        }
        private set
        {
            _gamePersistent = value;
        }
    }

    /// <summary>
    /// initializes base stats of save data (used for first time playing).
    /// Used both for reading existing save data AND for creating new save data if none is found.
    /// </summary>
    public void InitializeSaveData(bool deleteOldSave = false)
    {
        // new persistent data instance to initialize/load
        GamePersistentData newSaveData = new GamePersistentData();

        // default data in case player prefs not found
        newSaveData.currentName = "---";
        newSaveData.highScoreList = new List<int> { 0, 0, 0, 0, 0, 0 };
        newSaveData.highScoreNames = new List<string> { "---", "---", "---", "---", "---", "---" };

        // read save data, overriding existing data as it is found
        string filePath = Application.persistentDataPath + "/GameData.json";
        if (!deleteOldSave)
        {
            if (System.IO.File.Exists(filePath))
            {
                string saveData = System.IO.File.ReadAllText(filePath);
                newSaveData = JsonUtility.FromJson<GamePersistentData>(saveData);
                Instance.GamePersistent = newSaveData;
                return;
            }
        }

        // Apply read/initialized data to instance
        Instance.GamePersistent = newSaveData;
    }

    private void OnApplicationQuit()
    {
        string saveData = JsonUtility.ToJson(GamePersistent);
        string filePath = Application.persistentDataPath + "/GameData.json";
        System.IO.File.WriteAllText(filePath, saveData);
    }
    #endregion



}
