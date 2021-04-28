using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public List<Vector3> waypoints = new List<Vector3>();

    
    public void OnDrawGizmos()
    {
        
        if (transform.childCount > 0 )
        {
            PopulateWaypoints();
        }
        Gizmos.color = Color.cyan;
        for (int i = 1; i < waypoints.Count; i++)
        {
            Vector3 prev = waypoints[i - 1];
            Vector3 next = waypoints[i % waypoints.Count];
            Gizmos.DrawLine(prev, next);
            Gizmos.DrawSphere(prev, 1);
            Gizmos.DrawSphere(next, 1);
        }
    }

	// Use this for initialization
	void Start () {
        if (transform.childCount > 0)
        {
            PopulateWaypoints();            
        }
	}

    private void PopulateWaypoints()
    {
        waypoints.Clear();
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            waypoints.Add(transform.GetChild(i).position);
        }
    }
}
