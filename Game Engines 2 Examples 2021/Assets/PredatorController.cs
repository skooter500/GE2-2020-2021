using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override void Enter()
    {
        owner.GetComponent<Pursue>().target = owner.GetComponent<PredatorController>().prey.GetComponent<Boid>();
        owner.GetComponent<Pursue>().enabled = true;
    }

    public override void Think()
    {
        GameObject bullet = GameObject.Instantiate(owner.GetComponent<PredatorController>().bullet, owner.transform.position, owner.transform.rotation);
        
        if (Vector3.Distance(
            owner.GetComponent<PredatorController>().prey.transform.position,
            owner.transform.position) < 10)
        {
            owner.ChangeState(new FleeState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
    }
}

public class FleeState : State
{
    public override void Enter()
    {
        owner.GetComponent<Flee>().targetGameObject = owner.GetComponent<PredatorController>().prey;
        owner.GetComponent<Flee>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<PredatorController>().prey.transform.position,
            owner.transform.position) > 30)
        {
            owner.ChangeState(new AttackState());
        }
    }
    public override void Exit()
    {
        owner.GetComponent<Flee>().enabled = false;
    }
}

public class PredatorController : MonoBehaviour
{
    public GameObject prey;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new AttackState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
