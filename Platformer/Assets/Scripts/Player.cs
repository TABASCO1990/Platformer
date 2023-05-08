using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    const string MoveX = "Horizontal";

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private Level _leve;

    private float _groundCheckRadius = 0.1f;
    private float _directionMovement;    
    private float _degreeRotationLeft = 180;
    private float _degreeRotationRight = 0;
    private float _hitPower = 6.6f;
    private Rigidbody2D _rigidbody2D;
    private bool _isGround;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();      
    }

    private void FixedUpdate()
    {
        Move();
        Flip();
        Jump();
        Lose();
    }

    private void Move()
    {
        _directionMovement = Input.GetAxisRaw(MoveX);
        
        _rigidbody2D.velocity = new Vector2(_directionMovement * _speed, _rigidbody2D.velocity.y);
        _animator.SetFloat(States.Walk, Mathf.Abs(_directionMovement));
    }

    private void Jump()
    {
        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundMask);

        if (Input.GetKey(KeyCode.Space) && _isGround)
        {
            _rigidbody2D.velocity = Vector2.up * _jumpForce;
        }
    }

    private void Flip()
    {
        if(_directionMovement > 0)
            transform.localRotation = Quaternion.Euler(transform.rotation.x, _degreeRotationRight, transform.rotation.z);

        if (_directionMovement < 0)
            transform.localRotation = Quaternion.Euler(transform.rotation.x, _degreeRotationLeft, transform.rotation.z);
    }

    private void Lose()
    {
        if(transform.position.y < -5)
            _leve.Restart();
    }

    public void TakeDamage()
    {
        _rigidbody2D.AddForce(Vector3.up * _hitPower, ForceMode2D.Impulse);
        transform.GetComponent<BoxCollider2D>().enabled = false;
        transform.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
