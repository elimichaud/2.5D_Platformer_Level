using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceWeapon : MainWeapon
{
    [SerializeField]
    private float lanceAttackValue = 8f;

    [SerializeField]
    private float lanceShellEnemyMod = 1f;

    [SerializeField]
    private float lanceSlimeEnemyMod = 0f;

    [SerializeField]
    private float lanceMimicEnemyMod = 0.5f;

    void Start() {
        attackValue = lanceAttackValue;
        shellEnemyMod = lanceShellEnemyMod;
        slimeEnemyMod = lanceSlimeEnemyMod;
        mimicEnemyMod = lanceMimicEnemyMod;
        
        triggerName = "StabAttackTrigger";
        
        name = "Lance";
    }
}
