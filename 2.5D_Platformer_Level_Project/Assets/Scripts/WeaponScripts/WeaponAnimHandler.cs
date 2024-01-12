using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimHandler : StateMachineBehaviour
{
    private PlayerController controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        controller = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        controller.setWeaponAttackState(true);
        controller.drainAttackEnergy();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        controller.setWeaponAttackState(false);
    }
}
