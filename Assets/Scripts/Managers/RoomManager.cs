using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class RoomManager
{
    public GameObject currentRoom;
    public GameObject[] rooms;

    // Start is called before the first frame update
    public void Start()
    {
        currentRoom.SetActive(true);
        //rooms = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(r => r.tag == "room").ToArray();
        //foreach (var r in rooms)
            //if (r != currentRoom)
                //r.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRoom (GameObject room)
    {
        currentRoom.SetActive(false);

        currentRoom = room;

        currentRoom.SetActive(true);

    }
}
