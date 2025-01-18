using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp_Collider : MonoBehaviour
{
    [SerializeField] Transform point1, point2;
    private Vector3 target;

    public int speed;

    private float time;
    public int flickerTime;
    private bool lightOn;

    // Start is called before the first frame update
    void Start()
    {
        target = point2.position;

        time = 0;
        lightOn = true;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= flickerTime)
        {
            if (lightOn)
            {
                target = point1.position;
                lightOn = false;
            }
            else
            {
                target = point2.position;
                lightOn = true;
            }

            time = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }
}
