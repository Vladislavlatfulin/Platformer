using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    private Finish _finish;
    private Animator _animator;

    private void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _animator = GetComponent<Animator>();
    }

    public void ActivateLeverArm()
    {
        _animator.SetTrigger("Activate");
        _finish.Activate();
    }
}
