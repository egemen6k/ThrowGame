using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawProjectile : MonoBehaviour
{
    PlayerBall playerBall;
    LineRenderer lineRenderer;

    // Number of points on the line
    public int numPoints = 50;

    // distance between those points on the line
    public float timeBetweenPoints = 0.1f;

    // The physics layers that will cause the line to stop being drawn
    public LayerMask CollidableLayers;
    void Start()
    {
        playerBall = GetComponent<PlayerBall>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }


    void Update()
    {
        lineRenderer.positionCount = numPoints;

        List<Vector3> points = new List<Vector3>();

        Vector3 startingPosition = new Vector3(0, -4, 0);
        Vector3 startingVelocity = playerBall.liveVelVector;

        if (startingVelocity != Vector3.zero)
        {
            lineRenderer.enabled = true;
        }

        if (playerBall.isShot)
        {
            lineRenderer.enabled = false;
        }

        for (float t = 0; t < numPoints; t += timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            //number of colliding points on created sphere with physics---its array type value
            if (Physics.OverlapSphere(newPoint, 2 , CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }

        lineRenderer.SetPositions(points.ToArray());
    }
}
