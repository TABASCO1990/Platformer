using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private Transform _groundDetected;

    private float _degreeRotationLeft = 180;
    private float _degreeRotationRight = 0;
    private float _hitPower = 6.6f;
    private float _lengthRay = 1.0f;
    private bool _isRotateLeft = true;

    private void Update()
    {
        Move();
        PatrollingGround();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    private void PatrollingGround()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetected.position, Vector2.down, _lengthRay);

        if (groundInfo.collider == false)
        {
            if (_isRotateLeft == true)
            {
                transform.localRotation = Quaternion.Euler(transform.rotation.x, _degreeRotationRight, transform.rotation.z);
                _isRotateLeft = false;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(transform.rotation.x, _degreeRotationLeft, transform.rotation.z);
                _isRotateLeft = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * _hitPower, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
