using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override void Enter()
    {
        owner.GetComponent<Pursue>().target = owner.GetComponent<Fighter>().enemy.GetComponent<Boid>();
        owner.GetComponent<Pursue>().enabled = true;
    }

    public override void Think()
    {
        GameObject bullet = GameObject.Instantiate(owner.GetComponent<Fighter>().bullet, owner.transform.position + owner.transform.forward * 2, owner.transform.rotation);
        
        owner.GetComponent<Fighter>().ammo --;
        if (Vector3.Distance(
            owner.GetComponent<Fighter>().enemy.transform.position,
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
        owner.GetComponent<Flee>().targetGameObject = owner.GetComponent<Fighter>().enemy;
        owner.GetComponent<Flee>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<Fighter>().enemy.transform.position,
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

public class Alive:State
{
    public override void Think()
    {

        if (owner.GetComponent<Fighter>().health <= 0)
        {
            owner.ChangeState(new Dead());
            return;
        }

        if (owner.GetComponent<Fighter>().health <= 2)
        {
            owner.ChangeState(new FindHealth());
            return;
        }
        
        if (owner.GetComponent<Fighter>().ammo <= 0)
        {
            owner.ChangeState(new FindAmmo());
            return;
        }
    }
}

public class Dead:State
{
    public override void Enter()
    {
        owner.GetComponent<Pursue>().enabled = false;
        owner.GetComponent<Flee>().enabled = false;
        owner.GetComponent<StateMachine>().enabled = false;        
    }         
}

public class FindAmmo:State
{
    Transform ammo;
    public override void Enter()
    {
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
        // Find the closest ammo;
        Transform closest = ammos[0].transform;
        foreach(GameObject go in ammos)
        {
            if (Vector3.Distance(go.transform.position, owner.transform.position) <
                Vector3.Distance(closest.position, owner.transform.position))
                {
                    closest = go.transform;
                }
        }
        ammo = closest;
        owner.GetComponent<Arrive>().targetGameObject = ammo.gameObject;
        owner.GetComponent<Arrive>().enabled = true;
    }

    public override void Think()
    {
        // If the other guy already took tghe ammo
        if (ammo == null)
        {
            owner.ChangeState(new FindAmmo());
            return;
        }
        if (Vector3.Distance(owner.transform.position, ammo.position) < 5)
        {
            owner.GetComponent<Fighter>().ammo += 10;
            owner.RevertToPreviousState();
            GameObject.Destroy (ammo.gameObject);
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}

public class FindHealth:State
{
    Transform health;
    public override void Enter()
    {
        GameObject[] healths = GameObject.FindGameObjectsWithTag("Health");
        // Find the closest ammo;
        Transform closest = healths[0].transform;
        foreach(GameObject go in healths)
        {
            if (Vector3.Distance(go.transform.position, owner.transform.position) <
                Vector3.Distance(closest.position, owner.transform.position))
                {
                    closest = go.transform;
                }
        }
        health = closest;
        owner.GetComponent<Arrive>().targetGameObject = health.gameObject;
        owner.GetComponent<Arrive>().enabled = true;
    }

    public override void Think()
    {
        // If the other guy already took tghe ammo
        if (health == null)
        {
            owner.ChangeState(new FindAmmo());
            return;
        }
        if (Vector3.Distance(owner.transform.position, health.transform.position) < 5)
        {
            owner.GetComponent<Fighter>().health += 10;
            owner.RevertToPreviousState();
            GameObject.Destroy (health.gameObject);
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }
}

public class PredatorController : MonoBehaviour
{
    
    public void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Bullet")
        {
            GetComponent<Fighter>().health --;
            Destroy(c.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new AttackState());        
        GetComponent<StateMachine>().SetGlobalState(new Alive());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
