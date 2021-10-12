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
            if (other.transform.tag == "Ball")
            {
                _scorable = GetComponent<ICollision>().onBall();
            }

            if (other.transform.tag == "Player")
            {
                _scorable = GetComponent<ICollision>().onPlayer();
            }
        }
    }
}
