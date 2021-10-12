using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickVisualizator : MonoBehaviour,IVisual
{
    [SerializeField]
    private Transform _stick;

    private MeshRenderer _mr,_smr;
    private LineRenderer _lr;
    private Vector3 _stickPositionHolder;
    //private float _stickSpeedModifier = 0.1f;

    private void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        if (_mr == null)
        {
            Debug.LogError("Player MeshRenderer is null");
        }

        _smr = _stick.GetComponent<MeshRenderer>();
        if (_smr == null)
        {
            Debug.LogError("Stick MeshRenderer is null");
        }

        _lr = GetComponent<LineRenderer>();
        if (_lr == null)
        {
            Debug.LogError("LineRenderer is null");
        }
    }

    public void OnClicked()
    {
        //ilk stick position değeri
        _stickPositionHolder = _stick.position;
    }

    public void OnDrag()
    {
        _mr.enabled = false;
        _smr.enabled = true;
        _lr.enabled = true;
        _lr.SetPosition(0, _stickPositionHolder);
        _lr.SetPosition(1, _stick.position);

        Vector3 _placeHolderPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        if (Vector3.Distance(_placeHolderPos,_stickPositionHolder)>5f)
        {
            _placeHolderPos = (_placeHolderPos - _stickPositionHolder).normalized;
        }
        _stick.position = _placeHolderPos;
    }

    public void OnRelease()
    {
        _stick.position = _stickPositionHolder;
        _mr.enabled = true;
        _smr.enabled = false;
        _lr.enabled = false;
    }

}