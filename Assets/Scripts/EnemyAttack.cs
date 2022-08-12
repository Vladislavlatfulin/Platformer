using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private float damage = 20f;
    [SerializeField] private float timeToDamage = 1f;

    private float _damegeTime;
    private bool _isDamage = true;

    private void Start()
    {
        _damegeTime = timeToDamage;
    }

    private void Update()
    {
        if (!_isDamage)
        {
            _damegeTime -= Time.deltaTime;
            if (_damegeTime <= 0)
            {
                _damegeTime = timeToDamage;
                _isDamage = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && _isDamage)
        {
            playerHealth.RediceHealth(damage);
            _isDamage = false;
        }
    }
}
