using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour, ITouchInput
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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _startPos3D = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                    ivisual.OnClicked();
                    break;

                case TouchPhase.Moved:
                    ivisual.OnDrag();
                    break;

                case TouchPhase.Ended:
                    Vector3 _endPos3D = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                    direction = _startPos3D - _endPos3D;
                    ivisual.OnRelease();
                    break;
            }
        }
        else
        {
            direction = Vector3.zero;
        }
        return direction;
    }
}
