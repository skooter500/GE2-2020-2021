using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{

    public List<Vector3> waypoints;
    public bool IsLooped;

    void awake()
    {
        Vector3 curPos = transform.position;
        waypoints[0] = curPos;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
