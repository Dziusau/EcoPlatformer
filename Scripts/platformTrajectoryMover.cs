using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformTrajectoryMover : MonoBehaviour
{
    [SerializeField] 
    private Transform pointA; // the start point
    [SerializeField]
    private Transform pointB; // the end point
    [SerializeField]
    protected float speed = 2f; // the speed of the platform

    private Vector3 target; // the current target point

    // Start is called before the first frame update
    void Start()
    {
        target = pointB.position; // start moving towards point B
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); // move the platform towards the current target

        if (transform.position == pointA.position) // if we've reached point A
        {
            target = pointB.position; // start moving towards point B
        }
        else if (transform.position == pointB.position) // if we've reached point B
        {
            target = pointA.position; // start moving towards point A
        }
    }
}
