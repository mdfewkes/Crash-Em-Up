using UnityEngine;
using System.Collections.Generic;

public class MoveAndLoop : MonoBehaviour
{
 
    public Vector3 speed = new Vector3(-11f,0f,0f);
    public Vector3 minPos = new Vector3(-100f,-9999f,-9999f);
    public Vector3 maxPos = new Vector3(100f,9999f,9999f);
  
    void Update()
    {
        foreach (Transform thing in transform)
        {
            // scroll
            thing.position = thing.position + (speed * Time.deltaTime);

            // loop in either direction on separate axes
            if (thing.position.x < minPos.x) thing.position = new Vector3(maxPos.x,thing.position.y,thing.position.z);
            else if (thing.position.x > maxPos.x) thing.position = new Vector3(minPos.x,thing.position.y,thing.position.z);
            if (thing.position.y < minPos.y) thing.position = new Vector3(thing.position.x,maxPos.y,thing.position.z);
            else if (thing.position.y > maxPos.y) thing.position = new Vector3(thing.position.x,minPos.y,thing.position.z);
            if (thing.position.z < minPos.z) thing.position = new Vector3(thing.position.x,thing.position.y,maxPos.z);
            else if (thing.position.z > maxPos.z) thing.position = new Vector3(thing.position.x,thing.position.y,minPos.z);
        } // loop
    
    } 
}
