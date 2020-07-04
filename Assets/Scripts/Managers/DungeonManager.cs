using DungeonGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int dungeonSeed = 0;
    public Camera mainCamera;
    private RoomManager roomManager { get { return GetComponent <RoomManager>(); } }

    DungeonGeneration.DungeonGeneration<GameObject> generation;

    [SerializeField]
    private UnityEngine.Vector2 cameraClampMin { get { return mainCamera.GetComponent<CameraMovement>().minClamp; } }
    [SerializeField]
    private UnityEngine.Vector2 cameraClampMax { get { return mainCamera.GetComponent<CameraMovement>().maxClamp; } }

    public GameObject[] rooms;

    public GameObject northWall;
    public GameObject southWall;
    public GameObject eastWall;
    public GameObject westWall;

    public GameObject northeastCorner;
    public GameObject northwestCorner;
    public GameObject southwestCorner;
    public GameObject southeastCorner;

    public GameObject northDoor;
    public GameObject southDoor;
    public GameObject eastDoor;
    public GameObject westDoor;

    public GameObject floor;

    public GameObject roomTemplate;

    public float iconSize = 1;

    public int minimumRoomWidth = 5;
    public int maximumRoomWidth = 5;
    public int minimumRoomHeight = 5;
    public int maximumRoomHeight = 5;
    public int minimumRoomCount = 1;
    public int maximumRoomCount = 5;

    public bool autoUpdate = false;
    public void GenerateRandomSeed ()
    {
        var seedLength = UnityEngine.Random.Range(0, 10);
        string seed = "";
        for (int i = 0; i < seedLength; i++)
            seed += UnityEngine.Random.Range(0, 9).ToString();

        dungeonSeed = int.Parse(seed);
    }
    public void GenerateMap ()
    {
        try
        {
            generation = new DungeonGeneration<GameObject>(northWall, southWall, eastWall, westWall, northwestCorner, northeastCorner, southwestCorner, southeastCorner, northDoor, southDoor, eastDoor, westDoor, floor, dungeonSeed);

            Room<GameObject> room = generation.BuildDungeon(new DungeonGeneration.Vector2(0, 0), new DungeonGeneration.Vector2(minimumRoomWidth, maximumRoomWidth), new DungeonGeneration.Vector2(minimumRoomHeight, maximumRoomHeight), new DungeonGeneration.Vector2(minimumRoomCount, maximumRoomCount));
            List<Room<GameObject>> rooms = new List<Room<GameObject>>();
            rooms = rooms.OrderBy(r => r.ID).ToList();
            room.GetRooms(ref rooms);
            BuildAllRooms(rooms);
        }
        catch (System.Exception ex)
        {
        }
    }

    public void SetCameraPos ()
    {
        var pos = rooms[0].GetComponent<Room>().roomPos;
        var size = rooms[0].GetComponent<Room>().roomSize;

        mainCamera.orthographicSize = (size.y / 2) / 2;
        float height = Mathf.Round(2f * mainCamera.orthographicSize);
        float width = Mathf.Round(height * mainCamera.aspect);

        UnityEngine.Vector2 min = new UnityEngine.Vector2(pos.x + (size.x - Mathf.Round(width / 2)), pos.y - Mathf.Round(height / 2));
        UnityEngine.Vector2 max = new UnityEngine.Vector2(pos.x + Mathf.Round(width / 2), pos.y - (size.y - Mathf.Round(height / 2)));

        //if (min.x >= max.x && min.y > max.y)
        //{
        //    UnityEngine.Vector2 temp = min;
        //    min = max;
        //    max = temp;
        //}

        mainCamera.GetComponent<CameraMovement>().minClamp = min;
        mainCamera.GetComponent<CameraMovement>().maxClamp = max;
        mainCamera.transform.position = new Vector3(min.x, min.y, -1);
    }
    public void BuildAllRooms (List<Room<GameObject>> rooms)
    {
        this.rooms = new GameObject[rooms.Count];
        var i = 0;
        foreach (var room in rooms)
        {
            GameObject roomT = Instantiate(roomTemplate, transform);

            roomT.GetComponent<Room>().roomPos = new UnityEngine.Vector2(room.Origin.X, -room.Origin.Y);
            roomT.GetComponent<Room>().roomSize = new UnityEngine.Vector2(room.Width, room.Height);

            roomT.tag = "room";
            roomT.transform.position = new UnityEngine.Vector2(roomT.GetComponent<Room>().roomPos.x, -roomT.GetComponent<Room>().roomPos.y);

            for (int y = room.Origin.Y; y < room.Height + room.Origin.Y; y++)
            {
                for (int x = room.Origin.X; x < room.Width + room.Origin.X; x++)
                {
                    GameObject tile = Instantiate(room.Tiles[(x - room.Origin.X) + (y - room.Origin.Y) * room.Width], roomT.transform);

                    tile.transform.position = new UnityEngine.Vector2((x * iconSize) + (iconSize / 2), -(y * iconSize) - (iconSize / 2));
                }
            }
            room.Root = roomT;
            this.rooms[i] = roomT;
            i++;
        }
        foreach (var room in rooms)
            FindDoors(room);

        roomManager.startingRoom = this.rooms[0];
        SetCameraPos();
    }

    public void FindDoors(Room<GameObject> room)
    {
        try
        {
            // get a list of all door objects
            var doors = room.Tiles.Where(t => t == northDoor || t == southDoor || t == eastDoor || t == westDoor).ToList();

            // set tart clamps, target rooms, and other info
            foreach (var door in doors)
            {
                door.GetComponent<SceneChange>().roomManager = gameObject;
                door.GetComponent<SceneChange>().cam = mainCamera;

                // target rooms depends on which door type this is
                if (door == northDoor)
                {
                    door.GetComponent<SceneChange>().targetRoom = room.ToNorth.Root;
                    door.GetComponent<SceneChange>().playerChange = new UnityEngine.Vector2(0, 5);
                    door.GetComponent<SceneChange>().cameraChange = new UnityEngine.Vector2(0, room.ToNorth.Height);
                }
                else if (door == southDoor)
                {
                    door.GetComponent<SceneChange>().targetRoom = room.ToSouth.Root;
                    door.GetComponent<SceneChange>().playerChange = new UnityEngine.Vector2(0, -5);
                    door.GetComponent<SceneChange>().cameraChange = new UnityEngine.Vector2(0, -room.ToSouth.Height);
                }
                else if (door == eastDoor)
                {
                    door.GetComponent<SceneChange>().targetRoom = room.ToEast.Root;
                    door.GetComponent<SceneChange>().playerChange = new UnityEngine.Vector2(5, 0);
                    door.GetComponent<SceneChange>().cameraChange = new UnityEngine.Vector2(room.ToEast.Width, 0);
                }
                else if (door == westDoor)
                {
                    door.GetComponent<SceneChange>().targetRoom = room.ToWest.Root;
                    door.GetComponent<SceneChange>().playerChange = new UnityEngine.Vector2(-5, 0);
                    door.GetComponent<SceneChange>().cameraChange = new UnityEngine.Vector2(-room.ToEast.Width, 0);
                }
                Debug.Log("HIT");
            }
        }
        catch(Exception ex)
        {
            Debug.LogError($"{ex.Message}\n{ex.StackTrace}");
        }
    }

    public void DestroyMap ()
    {
        for (int j = 10; j >= 0; j--)
            for(int i = 0; i < transform.childCount; i++)
                DestroyImmediate(transform.GetChild(i).gameObject);
    }

    void Start()
    {
        //roomManager.ChangeRoom(rooms[0]);
    }
    void Update()
    {
        
    }
}