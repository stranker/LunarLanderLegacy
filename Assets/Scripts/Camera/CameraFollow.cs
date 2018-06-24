using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject target;
	
	// Update is called once per frame
	void Update () {
        if (target)
        {
            Vector3 position = Vector3.zero;
            position = target.transform.position;
            position.z = -GetComponent<Camera>().farClipPlane / 2;
            transform.position = position;
        }
    }
}
