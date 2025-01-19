using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void BacktoStart()
    {
        SceneManager.LoadScene("0_Start");
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("4_Credits");
    }


}
