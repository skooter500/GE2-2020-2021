using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject bullet;
    public GameObject enemy;
    public TMPro.TextMeshPro text;

    void Update()
    {        
        text.text = "Health: " + playerStats.health + "\n" + 
            
            "Ammo: " + playerStats.ammo + "\n" +
            "State: " + GetComponent<StateMachine>().currentState.GetType().Name + "\n" +
            "Global State: " + GetComponent<StateMachine>().globalState.GetType().Name;

    }
}