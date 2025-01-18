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

    // Start is called before the first frame update
    void Start()
    {
        target = point1.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (!raccoonInSight)
        {

            if (Vector3.Distance(transform.position, point1.position) < 0.1f)
            {
                target = point2.position;

            }
            else if (Vector3.Distance(transform.position, point2.position) < 0.1f)
            {
                target = point1.position;

            }

            transform.LookAt(target);

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        }
        else
        {
            transform.LookAt(player);
            transform.position = transform.position;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.gameObject.tag == "Player")
        {
            raccoonInSight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        if (other.gameObject.tag == "Player")
        {
            raccoonInSight = false;
        }
    }
}
