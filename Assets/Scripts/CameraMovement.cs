using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float smoothing = 1f;
    public Vector2 minClamp;
    public Vector2 maxClamp;

    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minClamp.x, maxClamp.x);
        targetPos.y = Mathf.Clamp(targetPos.y, maxClamp.y, minClamp.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
