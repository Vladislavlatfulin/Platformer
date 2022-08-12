using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float chasingSpeed = 3f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float timeToWait = 1.5f;
    [SerializeField] private float timeToChase = 1.5f;
    [SerializeField] private Transform modelEnemyTransform;

    private Transform _playerTransform;
    private Rigidbody2D _enemyBody;
    private Vector2 _leftBoundaryPosiyion;
    private Vector2 _rightBoundaryPosition;
    private Vector2 _nextPoint;
    private float _gravity = -9.8f;


    private float _walkSpeed;
    private float _timeWait;
    private float _chaseTime;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _chasingPlayer = false;
    private bool _playerWithCollider;
   
    public bool IsFacingRight { get => _isFacingRight; }


    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _timeWait = timeToWait;
        _chaseTime = timeToChase;
        _enemyBody = GetComponent<Rigidbody2D>();
        _leftBoundaryPosiyion = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosiyion + (Vector2.right) * walkDistance;
        _walkSpeed = patrolSpeed;
    }

    private void Update()
    {
        

        if (_chasingPlayer)
        {
            StartChaseTimer();
        }

        if (_isWait && !_chasingPlayer)
        {
            StartWaitTimer();
        }

        
        if (ShouldWait())
        {
            _isWait = true;
        }
    }

    private void FixedUpdate()
    {
        if (_chasingPlayer && _playerWithCollider)
        {
            return;
        }

         _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;
        

        if (_chasingPlayer)
        {
            ChasePlayer();
        }

        if (!_isWait && !_chasingPlayer)
        {
            Patrol();   
        }
    }

    private void Patrol()
    {
        if (!_isFacingRight)
        {
            _nextPoint.x *= -1;
        }
        _enemyBody.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void ChasePlayer()
    {
        float distance = DistanceToPlayer();
       if (distance < 0)
        {
            _nextPoint.x *= -1;
        }
       if (distance > 0.2f && !_isFacingRight)
        {
            Flip();
        }
       else if (distance < 0.2f && _isFacingRight)
        {
            Flip();
        }
        _enemyBody.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private float DistanceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x;
    }

    public void StartChasingPlayer()
    {
        _chasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = chasingSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(_leftBoundaryPosiyion, _rightBoundaryPosition);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = modelEnemyTransform.localScale;
        scale.x *= -1;
        modelEnemyTransform.localScale = scale;
    }

    private bool ShouldWait()
    {
        bool isOutLeftBoundary = transform.position.x <= _leftBoundaryPosiyion.x && !_isFacingRight;
        bool isOutRightBoundary = transform.position.x >= _rightBoundaryPosition.x && _isFacingRight;
        return isOutLeftBoundary || isOutRightBoundary;
    }

    private void StartWaitTimer()
    {
        _timeWait -= Time.deltaTime;
        if (_timeWait <= 0)
        {
            _isWait = false;
            Flip();
            _timeWait = timeToWait;
        }
    }

    private void StartChaseTimer()
    {
        _chaseTime -= Time.deltaTime;
        if(_chaseTime <= 0)
        {
            _chaseTime = timeToChase;
            _chasingPlayer = false;
            _walkSpeed = patrolSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            _playerWithCollider = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            _playerWithCollider = false;
        }
    }

}
