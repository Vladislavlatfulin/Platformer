using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackController attackController = animator.transform.root.GetComponent<AttackController>();
        attackController.FinishAttack();
    }


}
