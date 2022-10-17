using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 Distance;
    public Vector3 MovementFrequency;
    Vector3 Moveposition;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
       
        
    }
    void Update()
    {
        if(startPosition.z+Distance.z -0.01f <=transform.position.z )
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Debug.Log("ad");
        }else
        {
            Moveposition.x = startPosition.x + Mathf.Sin(Time.time * MovementFrequency.x) * Distance.x;
            Moveposition.y = startPosition.y + Mathf.Sin(Time.timeSinceLevelLoad * MovementFrequency.y) * Distance.y;
            Moveposition.z = startPosition.z + Mathf.Sin(Time.timeSinceLevelLoad * MovementFrequency.z) * Distance.z;
            transform.position = new Vector3(Moveposition.x, Moveposition.y, Moveposition.z);
        }
        
    }
}
