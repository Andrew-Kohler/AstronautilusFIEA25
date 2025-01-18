using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Light_Collision : MonoBehaviour
{
    public float timeToBeCaught = 5f;
    public float lightMeter;    // The meter that determines when the raccoon is caught
    public float sneakyTime;    // How much time the player has spent being SNEAKY
    private int lights;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        lightMeter = 0;
        lights = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lights > 0)
        {
            lightMeter += Time.deltaTime;
        }
        if (lights == 0)
        {
            lightMeter -= Time.deltaTime / 2;
        }
        if (lightMeter < 0)
        {
            lightMeter = 0;
        }
        if(lightMeter > timeToBeCaught)
        {
            SceneManager.LoadScene("3_GameOver");
        }
        AssignCurrentScoreMult();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            lights = 1;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            lights = 0;
            audioSource.Stop();
        }
    }

    public void AssignCurrentScoreMult()
    {
        if(lightMeter / timeToBeCaught >= 1)
        {
            GameManager.Instance.currentMult = 0;
        }
        else if (lightMeter / timeToBeCaught > .8f)
        {
            GameManager.Instance.currentMult = 1;
        }
        else if (lightMeter / timeToBeCaught > .6f)
        {
            GameManager.Instance.currentMult = 2;
        }
        else if (lightMeter / timeToBeCaught > .4f)
        {
            GameManager.Instance.currentMult = 3;
        }
        else if (lightMeter / timeToBeCaught > .2f)
        {
            GameManager.Instance.currentMult = 4;
            GameManager.Instance.elapsedSneakySeconds += Time.deltaTime;
        }
        else
        {
            GameManager.Instance.currentMult = 5;
        }
    }
}
