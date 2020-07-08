using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    public DungeonGeneration.Room<GameObject> roomDets;
    public GameObject enemyInRoom;
    public float maxEnemiesInRoom;
    public Vector2 roomSize;
    public Vector2 roomPos;

    private List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (enemyInRoom)
        {
            float minimumEnemiesInRoom = 0;
            if (maxEnemiesInRoom > 0) minimumEnemiesInRoom = 1;

            float enemyCount = Random.Range(minimumEnemiesInRoom, maxEnemiesInRoom);
            for (int i = 0; i < enemyCount; i++)
                Instantiate(enemyInRoom, new Vector3(Random.Range(roomPos.x, roomPos.x + (roomSize.x - 1)), Random.Range(roomPos.y, roomPos.y + (roomSize.y - 1)), 0), new Quaternion());
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var e in enemies)
        {
            e.SetActive(gameObject.activeInHierarchy);
        }
    }
}
