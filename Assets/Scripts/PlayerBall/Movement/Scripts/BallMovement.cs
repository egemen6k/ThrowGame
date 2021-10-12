using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private bool _hasThrown;
    private Rigidbody _rb;
    private Vector3 _direction;
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb != null)
        {
            gameObject.transform.position = new Vector3(0, -4, 0);
            _rb.isKinematic = true;
            _hasThrown = false;
        }
        else
        {
            Debug.LogError("Rigidbody is null");
        }
    }

    void Update()
    {
        //Speed limiting
        _rb.velocity = new Vector3(Mathf.Clamp(_rb.velocity.x, -25f, 25f), Mathf.Clamp(_rb.velocity.y, -25f, 25f), 0);

        if (!_hasThrown)
        {
            ITouchInput touchInput = GetComponent<ITouchInput>();
            if (touchInput != null)
            {
                _direction = touchInput.GetTouchInput();
            }

            if (!_direction.Equals(Vector3.zero))
            {
                IThrow Throw = GetComponent<IThrow>();
                if (Throw != null)
                {
                    Throw.ThrowBall(_direction);
                    _hasThrown = true;
                }
            }
        }
    }
}
