using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp_Flicker : MonoBehaviour
{

    [SerializeField] GameObject lampLight;
    private float time;
    public int flickerTime;
    private bool lightOn;

    public bool IsOn; // For if this hazard is active at all
    [SerializeField] private GameObject lights;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        lightOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOn)
        {
            lights.SetActive(true);
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
}
