using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraMotion : MonoBehaviour
{
    public int rotateSpeed;

    public int rotateTime;

    private float time;
    private float rightTime;
    private float leftTime;

    private bool rotatedRight;

    private bool soundPlayed;

    public AudioClip whirr;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= rotateTime)
        {
            if (!soundPlayed)
            {
                audioSource.PlayOneShot(whirr);

            }

            if (!rotatedRight)
            { 

                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
                rightTime += Time.deltaTime;
                if (rightTime >= rotateTime)
                {
                    time = 0;
                    rightTime = 0;
                    rotatedRight = true;
                }
            }
            else
            {
                transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
                leftTime += Time.deltaTime;
                if (leftTime >= rotateTime)
                {
                    time = 0;
                    leftTime = 0;
                    rotatedRight = false;
                    audioSource.PlayOneShot(whirr);

                }
            }

        }
    }
}
