using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{
    public string animName;
    private Animator _anim; // 1, 2, 3, or 4

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        _anim = GetComponent<Animator>();
        _anim.Play(animName, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
