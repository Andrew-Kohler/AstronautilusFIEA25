using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [SerializeField] private int pointVal;
    void Start()
    {
        //GameManager.Instance.gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddToScore(pointVal);
            Destroy(gameObject);
        }
    }
}
