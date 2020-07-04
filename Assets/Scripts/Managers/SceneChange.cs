using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public bool inRange;
    public bool changeScene;
    public bool changeRoom;

    public GameObject targetRoom;
    public GameObject roomManager;
    public Camera cam;

    public Vector2 cameraChange;
    public Vector3 playerChange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            Debug.Log(inRange);

            if (changeRoom)
                ChangeRoom(other);

            if (changeScene)
                ChangeScene();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            Debug.Log(inRange);
        }
    }

    void ChangeRoom (Collider2D other)
    {
        CameraMovement camMove = cam.GetComponent<CameraMovement>();
        camMove.maxClamp += cameraChange;
        camMove.minClamp += cameraChange;

        other.transform.position += playerChange;

        roomManager.GetComponent<RoomManager>().ChangeRoom(targetRoom);
    }

    void ChangeScene ()
    {

    }
}
