using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class TrashItem : MonoBehaviour
{
    [SerializeField] private int pointVal;
    public GameObject trashSound;
    public bool disappear;
    public float disTime = 5f;
    void Start()
    {
        //GameManager.Instance.gameActive = true;
        /*int index = Random.Range(0, junks.Count);
        junks[index].SetActive(true);*/
        if (disappear)
        {
            StartCoroutine(DoShrink());
        }
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

    private IEnumerator DoShrink()
    {
        float timer = disTime;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            float scaleVal = math.remap(disTime, 0, 1, 0, timer);
            Debug.Log(scaleVal);
            this.transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);
            yield return null;
        }
        Destroy(gameObject);

    }
}
