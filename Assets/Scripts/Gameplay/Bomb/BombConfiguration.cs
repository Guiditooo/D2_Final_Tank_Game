using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BombConfiguration", order = 1)]
public class BombConfiguration : ScriptableObject
{
    public int chaseSpeed;
    public int bounceForce;
}
