using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour, IThrow
{
    [SerializeField]
    private float _forceMultiplier = 100;
    [SerializeField]
    private AudioClip _launchingSound;

    void IThrow.ThrowBall(Vector3 direction)
    {  
        Rigidbody _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = false;
        Vector3 _force = direction * _forceMultiplier;
        Vector3 Force = new Vector3(Mathf.Clamp(_force.x, -1000f, +1000f), Mathf.Clamp(_force.y, -1750, +1750), 0);
        _rb.AddForce(Force);
        AudioSource.PlayClipAtPoint(_launchingSound, transform.position);
    }
}
