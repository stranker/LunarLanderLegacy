using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
    public int distanceToTarget;
    public int maxDistance = 15;
    public int minDistance = 6;

    private void Start()
    {
        GetComponent<Camera>().farClipPlane = maxDistance;
    }

    // Update is called once per frame
    void Update () {
        FollowTarget();
        AltitudeControl();
    }

    private void AltitudeControl()
    {
        if(target)
        {
            float altitude = target.GetComponent<Spaceship>().altitude;
            if (altitude < 100)
            {
                distanceToTarget = minDistance;
            }
            else
            {
                distanceToTarget = maxDistance;
            }
            GetComponent<Camera>().orthographicSize = distanceToTarget;
        }
    }

    private void FollowTarget()
    {
        if (target)
        {
            Vector3 position = Vector3.zero;
            position = target.transform.position;
            position.z = -GetComponent<Camera>().farClipPlane / 2;
            transform.position = position;
        }
    }
}
