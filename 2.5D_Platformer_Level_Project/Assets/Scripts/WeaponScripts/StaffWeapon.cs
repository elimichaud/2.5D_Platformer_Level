using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffWeapon : MainWeapon
{
    [SerializeField]
    private float staffAttackValue = 0f;

    [SerializeField]
    private float staffShellEnemyMod = 1f;

    [SerializeField]
    private float staffSlimeEnemyMod = 0f;

    void Start() {
        attackValue = staffAttackValue;
        shellEnemyMod = staffShellEnemyMod;
        slimeEnemyMod = staffSlimeEnemyMod;
        name = "Staff";
    }
}