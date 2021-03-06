using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStats", menuName = "Player Stats", order = 51)]
public class PlayerStats:ScriptableObject
{
    public float health = 10;
    public float ammo = 10;
}