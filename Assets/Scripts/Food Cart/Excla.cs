using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excla : MonoBehaviour
{
    [SerializeField] GameObject excla;
    [SerializeField] float time;
    void Start()
    {
        StartCoroutine(DoExcla());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DoExcla()
    {
        yield return new WaitForSeconds(time);
        excla.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        excla.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        excla.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        excla.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
