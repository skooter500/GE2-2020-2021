using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    public Transform target;

    public float speed = 1;
    public float time = 10;
    // Start is called before the first frame update
    void Start()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        speed = dist / time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toSphere = target.position - transform.position;
        Debug.Log(toSphere);
        //transform.position += toSphere;

        //Debug.Log(toSphere.magnitude);
        //Debug.Log(Vector3.Distance(target.position, transform.position));

        if (toSphere.magnitude > 5)
        {
            Debug.Log("Idle");
        }
        else
        {
            Debug.Log("Attack");
        }

        Vector3 n = toSphere.normalized; // Doesnt change toSphere
        toSphere.Normalize(); // Changes toSphere
        n = Vector3.Normalize(toSphere); 

        Debug.Log(n);

        //transform.forward = toSphere; 

        //transform.LookAt(target);

        //transform.Translate(0, 0, speed * Time.deltaTime); 

        Vector3 newPos = transform.position + (n * speed * Time.deltaTime); // Using vectors
        //transform.position = newPos;

        float theta = Mathf.Acos(Vector3.Dot(toSphere, transform.forward) / toSphere.magnitude) * Mathf.Rad2Deg;

        theta = Vector3.Angle(toSphere, transform.forward);

        Debug.Log("Angle: " + theta);
        if (theta < 45)
        {
            Debug.Log("INSIDE FOV");
        }
        else
        {
            Debug.Log("OUTSIDE FOV");
        }

        Vector3 a = new Vector3(0, 0, 1);
        Vector3 b = new Vector3(0, 1, 0);
        Vector3 c = Vector3.Cross(b, a);
        Debug.Log(c);
        

    }
}
