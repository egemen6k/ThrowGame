using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManipulation : MonoBehaviour
{
    [SerializeField]
    private float _speed = 50;
    private float _fov = 0;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        _fov += Time.deltaTime * _speed;
        if (_fov <= 80)
        {
            _cam.fieldOfView = _fov;
        }
    }
}
