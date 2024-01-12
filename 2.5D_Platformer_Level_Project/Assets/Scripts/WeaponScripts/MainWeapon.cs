using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    [System.NonSerialized]
    public float attackValue;

    [System.NonSerialized]
    public float shellEnemyMod;

    [System.NonSerialized]
    public float slimeEnemyMod;

    [System.NonSerialized]
    public float mimicEnemyMod;

    [System.NonSerialized]
    public bool isAttacking = false;

    [System.NonSerialized]
    public string triggerName = "AttackTrigger";

    [System.NonSerialized] 
    public string name = "name";
}
