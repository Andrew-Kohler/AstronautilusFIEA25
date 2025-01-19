using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cart : MonoBehaviour
{
    public string animNumber;
    public float delay;
    private Animator _anim; // 1, 2, 3, or 4
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(DoCart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("3_GameOver");
        }
    }

    private IEnumerator DoCart()
    {
        yield return new WaitForSeconds(delay);
        _anim.Play(animNumber, 0, 0);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
