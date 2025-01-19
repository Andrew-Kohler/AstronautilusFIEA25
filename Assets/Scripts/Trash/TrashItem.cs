using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [SerializeField] private int pointVal;
    public GameObject trashSound;

    [Header("Options")]
    [SerializeField] List<GameObject> junks = new List<GameObject>();
    void Start()
    {
        //GameManager.Instance.gameActive = true;
        int index = Random.Range(0, junks.Count);
        junks[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(trashSound);

            GameManager.Instance.AddToScore(pointVal * GameManager.Instance.currentMult);
            Destroy(gameObject);
        }
    }
}
