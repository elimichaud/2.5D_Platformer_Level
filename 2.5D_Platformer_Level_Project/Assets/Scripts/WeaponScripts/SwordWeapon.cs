using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : MainWeapon
{
    [SerializeField]
    private float swordAttackValue = 10f;

    [SerializeField]
    private float swordShellEnemyMod = 1f;

    [SerializeField]
    private float swordSlimeEnemyMod = 0f;

    [SerializeField]
    private float swordMimicEnemyMod = 0.5f;

    void Start() {
        attackValue = swordAttackValue;
        shellEnemyMod = swordShellEnemyMod;
        slimeEnemyMod = swordSlimeEnemyMod;
        mimicEnemyMod = swordMimicEnemyMod;
        name = "Sword";   
    }
}
