using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{

    [Header("Guard Management")]
    [SerializeField] private List<Patrol> inactiveGuards = new List<Patrol>();

    [Header("Lamp Management")]
    [SerializeField] private List<Lamp_Flicker> lamps = new List<Lamp_Flicker>();

    [Header("Cam Management")]
    [SerializeField] private List<SecurityCameraMotion> cams = new List<SecurityCameraMotion>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePatrols();
        UpdateLamps();
        UpdateCams();

    }

    private void UpdatePatrols()
    {

    }

    private void UpdateLamps()
    {

    }

    private void UpdateCams()
    {

    }
}
