using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Light_Collision : MonoBehaviour
{
    public float timeToBeCaught = 5f;
    public float lightMeter; // The meter that determines when the raccoon is caught
    private int lights;

    // Start is called before the first frame update
    void Start()
    {
        lightMeter = 0;
        lights = 0;
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
            lightMeter -= Time.deltaTime;
        }
        if (lightMeter < 0)
        {
            lightMeter = 0;
        }
        AssignCurrentScoreMult();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            lights = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light")
        {
            lights = 0;
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
        }
        else
        {
            GameManager.Instance.currentMult = 5;
        }
    }
}
