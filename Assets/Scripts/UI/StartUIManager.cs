using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartUIManager : MonoBehaviour
{
    [SerializeField] Animator startUIAnim;
    [SerializeField] List<TextMeshProUGUI> hiScores;
    [SerializeField] List<TextMeshProUGUI> hiScorers;
    [SerializeField] TMP_InputField nameInput;
    void Start()
    {
        nameInput.text = GameManager.Instance.GamePersistent.currentName;
        for(int i = 0; i < hiScores.Count; i++) // Set the high scores
        {
            hiScores[i].text = GameManager.Instance.GamePersistent.highScoreList[i].ToString();
            hiScorers[i].text = GameManager.Instance.GamePersistent.highScoreNames[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.GamePersistent.currentName = nameInput.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("2_Game");
        GameManager.Instance.ResetGame();
    }

    public void ToPractice()
    {
        SceneManager.LoadScene("1_Practice");
        GameManager.Instance.ResetGame();
    }

    public void HighScores()
    {
        startUIAnim.Play("Scores", 0, 0);
    }

    public void Back()
    {
        startUIAnim.Play("Back", 0, 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
