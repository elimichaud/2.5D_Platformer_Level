using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceWeapon : MainWeapon
{
    [SerializeField]
    private float maceAttackValue = 5f;

    [SerializeField]
    private float maceShellEnemyMod = 1f;

    [SerializeField]
    private float maceSlimeEnemyMod = 4f;
    
    [SerializeField]
    private float maceMimicEnemyMod = 2f;

    void Start() {
        attackValue = maceAttackValue;
        shellEnemyMod = maceShellEnemyMod;
        slimeEnemyMod = maceSlimeEnemyMod;
        mimicEnemyMod = maceMimicEnemyMod;
        name = "Mace";
    }
}
