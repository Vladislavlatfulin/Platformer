using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float directionX;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;

    private FixedJoystick _joystick;
    private Finish _finish;
    private LeverArm _leverArm;
    private Rigidbody2D _playerBody;


    private float _horizontal = 0f;

    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isFacingRight = true;
    private bool _isFinish = false;
    private bool _isLeverArm = false;




    private void Start()
    {
        _joystick = GameObject.FindGameObjectWithTag("FixedJoystick").GetComponent<FixedJoystick>();
        _playerBody = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _leverArm = GameObject.FindGameObjectWithTag("LeverArm").GetComponent<LeverArm>();
    }

    private void Update()
    {
        //_horizontal = Input.GetAxis("Horizontal");
        _horizontal = _joystick.Horizontal;
        animator.SetFloat("DirectionX", Mathf.Abs(_horizontal));

        

        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void FixedUpdate()
    {
        _playerBody.velocity = new Vector2(_horizontal * directionX * 50f * Time.fixedDeltaTime, _playerBody.velocity.y);

        if (_isJump)
        {
            _playerBody.AddForce(new Vector2(0, 500f));
            _isGround = false;
            _isJump = false;
        }

        if (_horizontal > 0 && !_isFacingRight)
        {
            Flip();
        }

        else if (_horizontal < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    public void Interact()
    {
        if (_isFinish)
        {
            _finish.FinishLevel();
        }

        if (_isLeverArm)
        {
            _leverArm.ActivateLeverArm();
        }
    }

    public void Jump()
    {
        if (_isGround)
        {
            _isJump = true;
            jumpSound.Play();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { _isGround = true; }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = playerModelTransform.localScale;
        scale.x *= -1;
        playerModelTransform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            _isFinish = true;
        }

        if (collision.GetComponent<LeverArm>())
        {
            _isLeverArm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            _isFinish = false;
        }

        if (collision.GetComponent<LeverArm>())
        {
            _isLeverArm = false;
        }
    }
}
