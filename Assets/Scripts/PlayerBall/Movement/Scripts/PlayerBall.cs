using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isPressed = false;
    public float releaseTime = 0.15f;
    public Rigidbody Hook;
    private float maxDragDistance = 2f;
    public GameObject nextBall;
    private LineRenderer _lr;
    private Vector3 mousePos;
    public AudioClip _clip;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.Log("Rigidbody is NULL.");
        }

        _lr = GetComponent<LineRenderer>();
        if (_lr == null)
        {
            Debug.Log("LR is NULL.");
        }
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

        if (_lr.positionCount > 1)
        {
            _lr.SetPosition(1, _rb.position);
        }
    }

    private void OnMouseDown()
    {
        _isPressed = true;
        _rb.isKinematic = true;
        _lr.positionCount = 2;
        _lr.SetPosition(0, _rb.position);   
    }

    private void OnMouseUp()
    {
        _lr.enabled = false;
        _isPressed = false;
        _rb.isKinematic = false;
        StartCoroutine(Release());
        this.enabled = false;
        AudioSource.PlayClipAtPoint(_clip,_rb.position);
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint>().breakForce = 0f;

        yield return new WaitForSeconds(4f);

       if (nextBall != null)
        {
            nextBall.SetActive(true);
        }

        Destroy(this.gameObject,3f);
    }
}
