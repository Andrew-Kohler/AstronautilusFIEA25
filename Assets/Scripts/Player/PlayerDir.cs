using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour
{
    [SerializeField] PlayerMovement pMove;
    [SerializeField] Transform raccoon;

    private float lastXDir;
    private float lastYDir;

    public float lerpVal = .25f;
    public float cutoff = .5f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xValAdded = pMove.xDir;
        float yValAdded = pMove.yDir;
        if(pMove.xDir == 0 && pMove.yDir == 0) // We want to maintain the last direction the player was looking at
        {
            xValAdded = lastXDir;
            yValAdded = lastYDir;
        }

        this.transform.position = new Vector3(
           raccoon.position.x + xValAdded, raccoon.position.y, raccoon.position.z + yValAdded);

        /*Vector3 target = new Vector3(raccoon.position.x + xValAdded, raccoon.position.y, raccoon.position.z + yValAdded);

        
        if (Vector3.Distance(transform.position, target) < cutoff)
        {
            this.transform.position = target;
        }
        else
        {
            this.transform.position = Vector3.Lerp(transform.position, target, lerpVal * Time.deltaTime);
        }*/

        if (pMove.xDir != 0 || pMove.yDir != 0) 
        {
            lastXDir = pMove.xDir;
            lastYDir = pMove.yDir;
        }

    }
}
