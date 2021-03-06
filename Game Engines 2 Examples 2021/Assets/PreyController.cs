using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class PatrolState : State
{
    public override void Enter()
    {
        owner.GetComponent<FollowPath>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<Fighter>().enemy.transform.position,
            owner.transform.position) < 10)
        {
            owner.ChangeState(new DefendState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<FollowPath>().enabled = false;
    }
}

public class DefendState : State
{
    public override void Enter()
    {
        owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
        owner.GetComponent<Pursue>().enabled = true;
    }

    public override void Think()
    {
        GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position, owner.transform.rotation);
        owner.GetComponent<Fighter>().playerStats.ammo --;
        if (Vector3.Distance(
            owner.GetComponent<Fighter>().enemy.transform.position,
            owner.transform.position) > 30)
        {
            owner.ChangeState(new PatrolState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
    }

}

public class PreyController : MonoBehaviour
{
    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Bullet")
        {
            GetComponent<Fighter>().playerStats.health --;
            Destroy(c.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new PatrolState());   
        GetComponent<StateMachine>().SetGlobalState(new Alive());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
