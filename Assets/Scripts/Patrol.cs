using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Animations;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform point1, point2;
    private Vector3 target;

    [SerializeField] Transform player;

    public int speed;

    private bool raccoonInSight;

    private bool guardSurprised;

    [SerializeField] GameObject leg1;
    [SerializeField] GameObject leg2;

    public AudioClip huh;
    AudioSource audioSource;

    public bool IsOn; // Variable for enabling the extra 
    bool turned = true;
    bool fullyTurned = true;

    // Start is called before the first frame update
    void Start()
    {
        target = point1.position;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOn)
        {
            if (!raccoonInSight)
            {
                leg1.GetComponent<Animator>().Play("PatrollerAnim");
                leg2.GetComponent<Animator>().Play("PatrollerAnim2");


                if (Vector3.Distance(transform.position, point1.position) < 0.1f)
                {
                    target = point2.position;
                    //turned = false;
                   // fullyTurned = false;

                }
                else if (Vector3.Distance(transform.position, point2.position) < 0.1f)
                {
                    //turned = false;
                    //fullyTurned = false;
                    target = point1.position;

                }

                /*if (!turned)
                {
                    turned = true;
                    StartCoroutine(TurnAround());
                }
                else if (fullyTurned)
                {*/
                    transform.LookAt(target);
                //}

                

                /*Vector3 newRot = Vector3.RotateTowards(transform.rotation.eulerAngles, target, .01f, .01f);
                transform.rotation = Quaternion.Euler(newRot);*/

                guardSurprised = true;
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            }
            else
            {
                //StopAllCoroutines();
                //fullyTurned = true;

                leg1.GetComponent<Animator>().Play("PatrolStatic1");
                leg2.GetComponent<Animator>().Play("PatrolStatic2");

                Vector3 playerTarget = player.position;
                playerTarget.y = transform.position.y;
                transform.LookAt(playerTarget);
                transform.position = transform.position;
                if (guardSurprised)
                {
                    audioSource.PlayOneShot(huh);
                    guardSurprised = false;
                }
            }
        }
       

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered");
        if (other.gameObject.tag == "Player")
        {
            raccoonInSight = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exited");
        if (other.gameObject.tag == "Player")
        {
            raccoonInSight = false;
            
        }
    }

    /*private IEnumerator TurnAround()
    {
        float degreeCounter = 0;
        float speed = 30;
        while (degreeCounter < 180)
        {
            transform.eulerAngles += new Vector3(0, speed * Time.deltaTime, 0);
            degreeCounter += speed * Time.deltaTime;
        }

        transform.LookAt(target);
        fullyTurned = true;

        yield return null;
    }*/
}
