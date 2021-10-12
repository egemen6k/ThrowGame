using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : MonoBehaviour,ITouchInput
{
    IVisual ivisual;
    Vector3 direction;
    Vector3 _startPos3D;
    private void Start()
    {
        ivisual = GetComponent<IVisual>();
        if (ivisual == null)
        {
            Debug.LogError("Visual Implementation is null");
        }
    }

    public Vector3 GetTouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos3D = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            ivisual.OnClicked();
        }

        else if (Input.GetMouseButton(0))
        {
            ivisual.OnDrag();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 _endPos3D = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            direction = _startPos3D - _endPos3D;
            ivisual.OnRelease();
        }
        else
        {
            direction = Vector3.zero;
        }
        return direction;
    }
}
