using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 20f, _rotateSpeed = 1f, _force = 2f;

    private Rigidbody _rigidBody;

    private float _verticalInput, _horizontalInput;

    public bool usePhysicsEngine;

    private float _wallLimit = 24f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");

        MovePlayer();

        KeepPlayerInBounds();        
    }

    void MovePlayer()
    {
        if (usePhysicsEngine)
        {
            // If using physics, use "addForce" on the RigidBody (Moving) and "addTorque" (Rotating)
            // Conceptually, when using forces we apply a force and not a speed, try always to put correct names.
            _rigidBody.AddForce(Vector3.forward * (_force * _verticalInput));
            _rigidBody.AddTorque(Vector3.up * (_rotateSpeed * _horizontalInput), ForceMode.Force);
        }
        else
        {
            // Otherwise, use transform.Translate (For moving) and transform.Rotate

            transform.Translate(Vector3.forward * (Time.deltaTime * _moveSpeed * _verticalInput));
            transform.Rotate(Vector3.up * (_rotateSpeed * Time.deltaTime * _horizontalInput));
        }
    }

    void KeepPlayerInBounds()
    {
        // This is a very cheap solution. The other way is putting 4 colliders as walls, but it's heavier because you are invokating the physics engine.
        if (Mathf.Abs(transform.position.x) > 24 || Mathf.Abs(transform.position.z) > 24)
        {
            _rigidBody.velocity = Vector3.zero;

            if (transform.position.x > _wallLimit)
            {
                transform.position = new Vector3(_wallLimit, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -_wallLimit)
            {
                transform.position = new Vector3(-_wallLimit, transform.position.y, transform.position.z);
            }

            if (transform.position.z > _wallLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, _wallLimit);
            }

            if (transform.position.z < -_wallLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -_wallLimit);
            }
        }
    }
}
