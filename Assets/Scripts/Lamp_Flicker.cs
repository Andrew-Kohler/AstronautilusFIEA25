using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp_Flicker : MonoBehaviour
{

    [SerializeField] GameObject lampLight;
    private float time;
    public int flickerTime;
    private bool lightOn;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        lightOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= flickerTime)
        {
            if (lightOn)
            {
                lampLight.SetActive(false);
                lightOn = false;
            }
            else
            {
                lampLight.SetActive(true);
                lightOn = true;
            }

            time = 0;
        }
    }
}
