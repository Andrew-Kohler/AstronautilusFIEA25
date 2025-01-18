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
}
