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

    private Vector3 mousePos, releasePos, firstpos, liveVelVector;

    public GameObject nextBall;

    private LineRenderer _lr;

    public AudioClip _clip;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_lr = GetComponent<LineRenderer>();
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

        liveVelVector = firstpos - releasePos;

        //if (_lr.positionCount > 1)
        //{
         //   _lr.SetPosition(1, _rb.position);
        //}
    }

    private void OnMouseDown()
    {
        _isPressed = true;
        _rb.isKinematic = true;
        //_lr.positionCount = 2;
        //_lr.SetPosition(0, _rb.position);   
    }

    private void OnMouseUp()
    {
        releasePos = _rb.position;
        //_lr.enabled = false;
        _isPressed = false;
        _rb.isKinematic = false;
        AudioSource.PlayClipAtPoint(_clip, _rb.position);
        StartCoroutine(Release(firstpos - releasePos));
        this.enabled = false;
    }

    IEnumerator Release(Vector3 velocityVector)
    {
        _rb.velocity = velocityVector * 13;
        Debug.Log("First pos: " + firstpos);
        Debug.Log("Release pos: " + releasePos);
        yield return new WaitForSeconds(4f);

       if (nextBall != null)
        {
            nextBall.SetActive(true);
        }

        Destroy(this.gameObject,3f);
    }
}
