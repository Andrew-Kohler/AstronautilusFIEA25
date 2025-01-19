using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrashSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _trashConfigs;

    bool activeConfig;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!activeConfig)
        {
            StartCoroutine(SpawnNewTrash());
        }
    }

    private IEnumerator SpawnNewTrash()
    {
        activeConfig = true;
        GameObject configToSpawn = _trashConfigs[Random.Range(0, _trashConfigs.Count)]; // Get the config to spawn

        GameObject activeTrash = Instantiate(configToSpawn); // Spawn it
        yield return new WaitUntil(()=>activeTrash.transform.childCount == 0); // Wait until all the trash has been collected
        GameManager.Instance.severityLevel++; // Add a severity level
        Destroy(activeTrash);

        activeConfig = false; // Begin again
        yield return null;
    }
}
