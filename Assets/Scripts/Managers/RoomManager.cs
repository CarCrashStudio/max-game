using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomManager : MonoBehaviour
{
    public GameObject startingRoom;

    // Start is called before the first frame update
    void Start()
    {
        startingRoom.SetActive(true);
        List<GameObject> otherRooms = FindObjectsOfType<GameObject>().Where(r => r.tag == "room").ToList();

        otherRooms.ForEach(r =>
        {
            if (r != startingRoom)
                r.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRoom (GameObject room)
    {
        Debug.Log("Hit");
        startingRoom.SetActive(false);

        startingRoom = room;

        startingRoom.SetActive(true);

    }
}
