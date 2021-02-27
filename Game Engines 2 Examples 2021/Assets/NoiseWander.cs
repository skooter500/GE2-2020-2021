using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseWander : SteeringBehaviour
{
    public float frequency = 0.3f;
    public float radius = 10.0f;

    public float theta = 0;
    public float amplitude = 80;
    public float distance = 5;

    public enum Axis { Horizontal, Vertical };

    public Axis axis = Axis.Horizontal;

    Vector3 target;
    Vector3 worldTarget;

    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
    }

    // Update is called once per frame
    public override Vector3 Calculate()
    {
        return Vector3.zero;
    }
}
