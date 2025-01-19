using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{

    [Header("Guard Management")]
    [SerializeField] private List<Patrol> inactiveGuards = new List<Patrol>();
    [SerializeField] private List<int> guardDeployLevels = new List<int>(); // Activation levels for 1/2/3/4
    private int nextGuardIndex;

    [Header("Lamp Management")]
    [SerializeField] private List<Lamp_Flicker> lamps = new List<Lamp_Flicker>(); // List of the lamps
    [SerializeField] private List<int> lampActiveLevels = new List<int>(); // List of levels where a change occurs
    private int nextLampIndex;

    [Header("Cam Management")]
    [SerializeField] private List<SecurityCameraMotion> cams = new List<SecurityCameraMotion>();
    [SerializeField] private List<int> camActiveLevels = new List<int>(); // Activation levels for 1/2/3/4
    private int nextCamIndex;

    [Header("Cart Management")]
    [SerializeField] private List<GameObject> cartPrefabs = new List<GameObject>(); // Prefabs to spawn for cart paths
    [SerializeField] private List<int> cartActiveLevels = new List<int>(); // The starting level, & levels where the time between spawns decreases
    [SerializeField] private List<int> cartTimers = new List<int>(); // Timer requirements for each decrease in spawn time reqs
    private float currentCartSpawnTime = 9999999; // How long it takes to spawn a new cart
    private float cartSpawnTimer;
    private int nextCartIndex;

    [Header("Spotlight Management")]
    [SerializeField] private List<GameObject> floodPrefabs = new List<GameObject>(); // Prefabs to spawn for floodlights
    [SerializeField] private List<int> floodActiveLevels = new List<int>(); // Starting levels, and levels where the time between spawns decreases
    [SerializeField] private List<int> floodTimers = new List<int>(); // Timer requirements for each decrease in spawn time reqs
    private float currentFloodSpawnTime = 9999999; // How long it takes to spawn a new floodlight
    private float floodSpawnTimer;
    private int nextFloodIndex;
    void Start()
    {
        nextGuardIndex = 0;
        nextLampIndex = 0;
        nextCamIndex = 0;
        nextCartIndex = 0;
        nextFloodIndex = 0;

        cartSpawnTimer = currentCartSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdatePatrols();
        UpdateLamps();
        //UpdateCams();
        UpdateCarts();
        //UpdateFloods();

    }

    private void UpdatePatrols()
    {
        if (inactiveGuards.Count != 0) // If they're not all out
        {
            if (GameManager.Instance.severityLevel == guardDeployLevels[nextGuardIndex])
            {
                nextGuardIndex++;
                int deploy = Random.Range(0, inactiveGuards.Count);
                inactiveGuards[deploy].IsOn = true;
                inactiveGuards.RemoveAt(deploy);
            }
        }
        
    }

    private void UpdateLamps() 
    {
        if (lamps.Count != 0) // If they're not all active
        {
            if (GameManager.Instance.severityLevel == lampActiveLevels[nextLampIndex])
            {
                nextLampIndex++;
                int deploy = Random.Range(0, lamps.Count);
                lamps[deploy].IsOn = true;
                lamps.Remove(lamps[deploy]);
            }
        }
    }

    private void UpdateCams()
    {
        if (cams.Count != 0) // If they're not all out
        {
            if (GameManager.Instance.severityLevel == camActiveLevels[nextCamIndex])
            {
                nextCamIndex++;
                int deploy = Random.Range(0, cams.Count);
                cams[deploy].isOn = true;
                cams.Remove(cams[deploy]);
            }
        }
    }

    private void UpdateCarts()
    {
        if(nextCartIndex < cartActiveLevels.Count) // If there's further to go
        {
            if (GameManager.Instance.severityLevel == cartActiveLevels[nextCartIndex]) // If the severity level makes it there, drop the time between spawns
            {
                currentCartSpawnTime = cartTimers[nextCartIndex];
                nextCartIndex++;

                if (cartSpawnTimer > currentCartSpawnTime)
                {
                    cartSpawnTimer = currentCartSpawnTime;
                }
            }
        }
        

        cartSpawnTimer -= Time.deltaTime;

        if(cartSpawnTimer <= 0) // If it's time to spawn, we spawn
        {
            cartSpawnTimer = currentCartSpawnTime;

            // Randomly select a cart prefab
            int cartIndex = Random.Range(0, cartPrefabs.Count);
            Instantiate(cartPrefabs[cartIndex]);
        }


    }

    private void UpdateFloods() //TODO
    {
        if (nextFloodIndex < floodActiveLevels.Count) // If there's further to go
        {
            if (GameManager.Instance.severityLevel == floodActiveLevels[nextFloodIndex]) // If the severity level makes it there, drop the time between spawns
            {
                currentFloodSpawnTime = floodTimers[nextFloodIndex];
                nextFloodIndex++;

                if (floodSpawnTimer > currentFloodSpawnTime)
                {
                    floodSpawnTimer = currentFloodSpawnTime;
                }
            }
        }


        floodSpawnTimer -= Time.deltaTime;

        if (floodSpawnTimer <= 0) // If it's time to spawn, we spawn
        {
            cartSpawnTimer = currentCartSpawnTime;

            // Randomly select a cart prefab
            int floodIndex = Random.Range(0, floodPrefabs.Count);
            Instantiate(floodPrefabs[floodIndex]);
        }
    }
}
