using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector3> wayPoints;
    public GameObject[] wayPointGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < wayPointGameObjects.Length; i ++)
        {
            wayPoints[i] = wayPointGameObjects[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            Gizmos.DrawSphere(wayPoints[i], 2);
            if (i != 0)
                Debug.DrawLine(wayPoints[i - 1], wayPoints[i], Color.black);
            if (i == wayPoints.Count - 1)
                Debug.DrawLine(wayPoints[wayPoints.Count -1], wayPoints[wayPoints.Count - wayPoints.Count], Color.black);
        }
    }
}
