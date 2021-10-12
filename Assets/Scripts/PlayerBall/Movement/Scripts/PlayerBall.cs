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

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.Log("Rigidbody is NULL.");
        }
    }
    private void Update()
    {

        if (_isPressed == true)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            if (Vector3.Distance(mousePos, Hook.position) > maxDragDistance)
            {
                _rb.position = Hook.position + (mousePos - Hook.position).normalized * maxDragDistance;
            }
            else
            {
                _rb.position = mousePos;
            }
        }
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
        StartCoroutine(Release());
        this.enabled = false;
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
