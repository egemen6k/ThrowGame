using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ball : MonoBehaviour
{
    private bool _scorable = true;

    private void OnCollisionEnter(Collision other)
    {
        if (_scorable)
        {    
                _scorable = GetComponent<ICollision>().onBall();
        }
    }
}
