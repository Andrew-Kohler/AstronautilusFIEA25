using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour
{
    [SerializeField] PlayerMovement pMove;
    [SerializeField] Transform raccoon;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(
            raccoon.position.x + pMove.xDir, raccoon.position.y, raccoon.position.z + pMove.yDir);
    }
}
