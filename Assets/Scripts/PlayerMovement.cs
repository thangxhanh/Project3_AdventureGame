using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private LayerMask _jumpableGround;
    [SerializeField] private float _coyoteTime = 0.1f; // The duration in seconds after leaving the ground where the player can still jump
    [SerializeField] private float _jumpBufferTime = 0.1f; // The duration in seconds before landing where the player's jump input is buffered

    private float _horizontalMovement = 0f;
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _jumpForce = 14f;
    private bool _hasDoubleJumped = false;

    private enum MovementState { Idle, Running, Jumping, Falling, DoubleJumping }
    [SerializeField] private MovementState _movementState = MovementState.Idle;

    [SerializeField] private AudioSource _soundSfx;
    [SerializeField] private AudioClip _jumpSound;

    private bool _isJumpBuffered = false;
    private bool _isGrounded = false;
    private float _coyoteTimer = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        UpdateAnimationState();
    }

    private void HandleInput()
    {
        // Buffer the jump input
        if (Input.GetButtonDown("Jump"))
        {
            _isJumpBuffered = true;
        }
    }
    private void HandleMovement()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");

        bool isCollidingWithWall = Physics2D.OverlapCircle(transform.position - new Vector3(0, _collider.bounds.extents.y, 0), .1f, _jumpableGround);

        if (isCollidingWithWall && ((_horizontalMovement > 0f && !_spriteRenderer.flipX) || (_horizontalMovement < 0f && _spriteRenderer.flipX)))
        {
            _horizontalMovement = 0f;
        }

        if (!_isGrounded && isCollidingWithWall)
        {
            _horizontalMovement = 0f;
        }

        _rigidbody.velocity = new Vector2(_horizontalMovement * _moveSpeed, _rigidbody.velocity.y);

        if (_horizontalMovement > 0f)
        {
            _movementState = MovementState.Running;
            _spriteRenderer.flipX = false;
        }
        else if (_horizontalMovement < 0f)
        {
            _movementState = MovementState.Running;
            _spriteRenderer.flipX = true;
        }
        else
        {
            _movementState = _isGrounded ? MovementState.Idle : MovementState.Falling;
        }

        // Check if the player is grounded
        _isGrounded = IsGrounded();

        // Update the coyote timer
        if (_isGrounded)
        {
            _coyoteTimer = _coyoteTime;
            _hasDoubleJumped = false; // Reset double jump ability when grounded
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }

        // Perform jump
        if (_isJumpBuffered)
        {
            if (_isGrounded)
            {
                Jump();
            }
            else if (!_hasDoubleJumped)
            {
                Jump();
                _hasDoubleJumped = true;
            }
            _isJumpBuffered = false;
        }
        else if (_isGrounded || _coyoteTimer > 0f)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (_isGrounded)
                {
                    Jump();
                }
                else if (!_hasDoubleJumped)
                {
                    Jump();
                    _hasDoubleJumped = true;
                }
            }
        }
    }

    private void Jump()
    {
        _soundSfx.PlayOneShot(_jumpSound);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);

        // Set the appropriate movement state
        if (!_isGrounded)
        {
            _hasDoubleJumped = true;
            _movementState = MovementState.DoubleJumping;
        }
        else
        {
            _movementState = MovementState.Jumping;
        }
    }

    private void UpdateAnimationState()
    {
        if (_rigidbody.velocity.y > .1f)
        {
            _movementState = _hasDoubleJumped ? MovementState.DoubleJumping : MovementState.Jumping;
        }
        else if (_rigidbody.velocity.y < -.1f)
        {
            _movementState = MovementState.Falling;
        }
        else if (_horizontalMovement != 0f)
        {
            _movementState = MovementState.Running;
        }
        else
        {
            _movementState = MovementState.Idle;
        }
        _animator.SetInteger("state", (int)_movementState);
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
    }
}
