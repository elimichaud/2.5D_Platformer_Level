using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : MainWeapon
{
    [SerializeField]
    private float axeAttackValue = 20f;

    [SerializeField]
    private float axeShellEnemyMod = 1f;

    [SerializeField]
    private float axeSlimeEnemyMod = 0f;

    [SerializeField]
    private float axeMimicEnemyMod = 1f;

    void Start() {
        attackValue = axeAttackValue;
        shellEnemyMod = axeShellEnemyMod;
        slimeEnemyMod = axeSlimeEnemyMod;
        mimicEnemyMod = axeMimicEnemyMod;
        
        triggerName = "SlowAttack";

        name = "Axe";
    }
}
