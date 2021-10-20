using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private Rigidbody _rb;
    public Rigidbody Hook;

    private bool _isPressed = false;

    public float releaseTime = 0.1f;
    private float maxDragDistance = 2f;
    private float _velocityMultiplier = 13f;

    private Vector3 mousePos, releasePos, firstpos;

    public Vector3 liveVelVector;

    public GameObject nextBall;

    public AudioClip _clip;

    public bool isShot = false;

    public ParticleSystem playerParticle;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        firstpos = _rb.position;

    }
    private void Update()
    {
        if (_isPressed == true)
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            if (Vector3.Distance(mousePos, Hook.position) > maxDragDistance)
            {
                _rb.position = Hook.position + (mousePos - Hook.position).normalized * maxDragDistance;
            }
            else
            {
                _rb.position = mousePos;
            }
        }

        liveVelVector = (firstpos - _rb.position) * _velocityMultiplier;
    }

    private void OnMouseDown()
    {
        _isPressed = true;
        _rb.isKinematic = true; 
    }

    private void OnMouseUp()
    {
        _isPressed = false;
        _rb.isKinematic = false;
        AudioSource.PlayClipAtPoint(_clip, _rb.position);
        StartCoroutine(Release(liveVelVector));
        this.enabled = false;
    }

    IEnumerator Release(Vector3 velocityVector)
    {
        _rb.velocity = velocityVector;
        isShot = true;
        yield return new WaitForSeconds(4f);

       if (nextBall != null)
        {
            nextBall.SetActive(true);
        }
        yield return new WaitForSeconds(3);
        Instantiate(playerParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
