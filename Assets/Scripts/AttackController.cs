using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource attackSound;

    private bool _isAttack = false;
    public bool IsAttack { get => _isAttack; }

    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        attackSound.Play();
    //        _isAttack = true;
    //        playerAnimator.SetTrigger("Attack");
    //    }
    //}

    public void FinishAttack()
    {
        _isAttack = false;
    }

    public void Attack()
    {
        attackSound.Play();
        _isAttack = true;
        playerAnimator.SetTrigger("Attack");
    }
}
