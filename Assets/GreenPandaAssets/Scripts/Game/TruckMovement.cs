using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class TruckMovement : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    private float distanceTravelled;    

    private Transform originalTransform;

    private void Start()
    {
        originalTransform = transform;
    }

    private void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        transform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
        
    }
}
