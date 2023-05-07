using UnityEngine;

public class Player : MonoBehaviour
{
    const string MoveX = "Horizontal";

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;

    private float _groundCheckRadius = 0.1f;
    private float _directionMovement;    
    private float _degreeRotationLeft = 180;
    private float _degreeRotationRight = 0;
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
    }

    private void Move()
    {
        _directionMovement = Input.GetAxisRaw(MoveX);             

        _rigidbody2D.velocity = new Vector2(_directionMovement * _speed, _rigidbody2D.velocity.y);        
    }

    private void Jump()
    {
        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundMask);

        if (Input.GetKey(KeyCode.Space) && _isGround)        
            _rigidbody2D.velocity = Vector2.up * _jumpForce;     
    }

    private void Flip()
    {
        if(_directionMovement > 0)
            transform.localRotation = Quaternion.Euler(transform.rotation.x, _degreeRotationRight, transform.rotation.z);

        if (_directionMovement < 0)
            transform.localRotation = Quaternion.Euler(transform.rotation.x, _degreeRotationLeft, transform.rotation.z);
    }
}
