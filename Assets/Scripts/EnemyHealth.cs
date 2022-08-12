using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Animator _animator;
    private float _health;


    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }

    public void RediceHealth(float damage)
    {
        _health -= damage;
        _animator.SetTrigger("TakeDamage");
        InitHealth();
        if(_health <= 0)
        {
            Die();
        }
    }

    private void InitHealth()
    {
        slider.value = _health / totalHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
