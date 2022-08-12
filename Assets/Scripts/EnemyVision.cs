using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private GameObject currentGameObject;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float circleRadius;
    [SerializeField] private float maxDistance;

    private EnemyController _enemyController;


    private Vector2 origin;
    private Vector2 direction;

    private float currentHitDistance;


    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        origin = transform.position;
        
        if (!_enemyController.IsFacingRight)
        {
            direction = Vector2.left;
        }
        else if (_enemyController.IsFacingRight)
        {
            direction = Vector2.right;
        }


        RaycastHit2D hit = Physics2D.CircleCast(origin, circleRadius, direction, maxDistance,playerLayer);

        if (hit)
        {
            currentGameObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            if (currentGameObject.CompareTag("Player"))
            {
                _enemyController.StartChasingPlayer();
            }
        }
        else
        {
            currentGameObject = null;
            currentHitDistance = maxDistance;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawSphere(origin + direction * currentHitDistance, circleRadius);
    }
}

