using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject GameOverObject;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator _animator;
    [SerializeField] private float totalHealth = 100f;

    private float _health;

    private void Start()
    {
        _health = totalHealth;
        InitHealth();
        
    }

    public void RediceHealth(float damage)
    {
        hitSound.Play();
        _health -= damage;
        _animator.SetTrigger("TakeDamage");
        InitHealth();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void InitHealth()
    {
        healthSlider.value = _health / totalHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        GameOverObject.SetActive(true);

    }
}
