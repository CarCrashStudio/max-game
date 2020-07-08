using DungeonGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public float iconSize = 1;
    public int dungeonSeed = 0;
    public Camera mainCamera;

    public RoomManager roomManager;

    DungeonGeneration.DungeonGeneration<GameObject> generation;

    private GameObject[] rooms;
    public GameObject player;

    [Header("Tile Masters")]
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

    //public GameObject largeChest;
    public GameObject smallChest;
    public GameObject chestUI;

    public GameObject roomTemplate;

    [Header("Spawnables")]
    public LootTable[] spawnableLoot;

    [Header("Minimum/Maximum Values")]
    public UnityEngine.Vector2 roomWidth = new UnityEngine.Vector2(5, 5);
    public UnityEngine.Vector2 roomHeight = new UnityEngine.Vector2(5, 5);
    public UnityEngine.Vector2 roomCount = new UnityEngine.Vector2(5, 5);
    public UnityEngine.Vector2 chestCount = new UnityEngine.Vector2(1, 2);

    [Header("Editor Properties")]
    public bool autoUpdate = false;

    private void Start()
    {
        roomManager.Start();
    }

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
        roomManager = new RoomManager();
        player.GetComponent<MaxAttributes>().manager = this;
        try
        {
            if (roomWidth.x > roomWidth.y)
                roomWidth.y = roomWidth.x;
            if (roomHeight.x > roomHeight.y)
                roomHeight.y = roomHeight.x;
            if (roomCount.x > roomCount.y)
                roomCount.y = roomCount.x;
            if (chestCount.x > chestCount.y)
                chestCount.y = chestCount.x;

            generation = new DungeonGeneration<GameObject>(northWall, southWall, eastWall, westWall, northwestCorner, northeastCorner, southwestCorner, southeastCorner, northDoor, southDoor, eastDoor, westDoor, floor, smallChest, dungeonSeed);

            Room<GameObject> room = generation.BuildDungeon(new DungeonGeneration.Vector2(0, 0), 
                                                            new DungeonGeneration.Vector2(roomWidth.x, roomWidth.y), 
                                                            new DungeonGeneration.Vector2(roomHeight.x, roomHeight.y), 
                                                            new DungeonGeneration.Vector2(roomCount.x, roomCount.y), 
                                                            new DungeonGeneration.Vector2(chestCount.x, chestCount.y));
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
        var pos = roomManager.currentRoom.GetComponent<Room>().roomPos;
        var size = roomManager.currentRoom.GetComponent<Room>().roomSize;

        float height = Mathf.Round(2f * mainCamera.orthographicSize / mainCamera.rect.height);
        float width = Mathf.Round(height * mainCamera.aspect / mainCamera.rect.width);

        UnityEngine.Vector2 min = new UnityEngine.Vector2(pos.x + (size.x - Mathf.Round(width / 2)), pos.y - Mathf.Round(height / 2));
        UnityEngine.Vector2 max = new UnityEngine.Vector2(pos.x + Mathf.Round(width / 2), pos.y - (size.y - Mathf.Round(height / 2)));
        if (min.x < pos.x)
            min.x = pos.x;
        if (min.y > pos.y)
            min.y = pos.y;

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
            room.Root = roomT;
            roomT.GetComponent<Room>().roomDets = room;

            roomT.tag = "room";
            roomT.transform.position = new UnityEngine.Vector2(roomT.GetComponent<Room>().roomPos.x, -roomT.GetComponent<Room>().roomPos.y);

            // draw all base tiles
            for (int y = room.Origin.Y; y < room.Height + room.Origin.Y; y++)
            {
                for (int x = room.Origin.X; x < room.Width + room.Origin.X; x++)
                {
                    var index = (x - room.Origin.X) + (y - room.Origin.Y) * room.Width;
                    GameObject tile = Instantiate(room.Tiles[index], roomT.transform);

                    tile.transform.position = new UnityEngine.Vector2((x * iconSize) + (iconSize / 2), -(y * iconSize) - (iconSize / 2));
                }
            }
            // draw all world objects above the base tiles
            for (int y = room.Origin.Y; y < room.Height + room.Origin.Y; y++)
            {
                for (int x = room.Origin.X; x < room.Width + room.Origin.X; x++)
                {
                    var index = (x - room.Origin.X) + (y - room.Origin.Y) * room.Width;
                    if (room.WorldObjects[index] != null)
                    {
                        GameObject tile = Instantiate(room.WorldObjects[index], roomT.transform);
                        tile.transform.position = new UnityEngine.Vector2((x * iconSize) + (iconSize / 2), -(y * iconSize) - (iconSize / 2));
                    }

                }
            }

            
            this.rooms[i] = roomT;
            i++;
        }
        roomManager.currentRoom = this.rooms[0];
        roomManager.rooms = this.rooms;

        foreach (var room in this.rooms)
        {
            FindDoors(room.GetComponent<Room>());
            FindChests(room.GetComponent<Room>());
        }
    }

    int FindRoom (Room room)
    {
        for(int i = 0; i < roomManager.rooms.Length; i++)
        {
            var r = roomManager.rooms[i].GetComponent<Room>();
            if (r.roomDets.ID == room.roomDets.ID)
                return i;
        }
        return -1;
    }
    public void FindDoors(Room room)
    {
        try
        {
            // get a list of all door objects
            var doors = room.roomDets.WorldObjects.Where(t => t == northDoor || t == southDoor || t == eastDoor || t == westDoor).ToList();

            // set tart clamps, target rooms, and other info
            foreach (var door in doors)
            {
                door.GetComponent<SceneChange>().roomManager = roomManager;
                door.GetComponent<SceneChange>().cam = mainCamera;

                // target rooms depends on which door type this is
                if (door == northDoor)
                {

                    door.GetComponent<SceneChange>().targetRoom = FindRoom(room.roomDets.ToNorth.Root.GetComponent<Room>());
                    door.GetComponent<SceneChange>().playerChange = new UnityEngine.Vector2(0, 5);
                    door.GetComponent<SceneChange>().cameraChange = new UnityEngine.Vector2(0, room.roomDets.ToNorth.Height);
                }
                else if (door == southDoor)
                {
                    door.GetComponent<SceneChange>().targetRoom = FindRoom(room.roomDets.ToSouth.Root.GetComponent<Room>());
                    door.GetComponent<SceneChange>().playerChange = new UnityEngine.Vector2(0, -5);
                    door.GetComponent<SceneChange>().cameraChange = new UnityEngine.Vector2(0, -room.roomDets.ToSouth.Height);
                }
                else if (door == eastDoor)
                {
                    door.GetComponent<SceneChange>().targetRoom = FindRoom(room.roomDets.ToEast.Root.GetComponent<Room>());
                    door.GetComponent<SceneChange>().playerChange = new UnityEngine.Vector2(5, 0);
                    door.GetComponent<SceneChange>().cameraChange = new UnityEngine.Vector2(room.roomDets.ToEast.Width, 0);
                }
                else if (door == westDoor)
                {
                    door.GetComponent<SceneChange>().targetRoom = FindRoom(room.roomDets.ToWest.Root.GetComponent<Room>());
                    door.GetComponent<SceneChange>().playerChange = new UnityEngine.Vector2(-5, 0);
                    door.GetComponent<SceneChange>().cameraChange = new UnityEngine.Vector2(-room.roomDets.ToWest.Width, 0);
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogError($"{ex.Message}\n{ex.StackTrace}");
        }
    }
    public void FindChests(Room room)
    {
        try
        {
            // get a list of all door objects
            var chests = room.roomDets.WorldObjects.Where(t => t == smallChest).ToList();

            // set tart clamps, target rooms, and other info
            foreach (var chest in chests)
            {
                // target rooms depends on which door type this is
                if (chest.GetComponent<Chest>().type == ChestType.SMALL)
                    chest.GetComponent<Chest>().inventory = new InventoryItem[3];
                else if (chest.GetComponent<Chest>().type == ChestType.LARGE)
                    chest.GetComponent<Chest>().inventory = new InventoryItem[6];

                chest.GetComponent<Chest>().player = player;
                chest.GetComponent<Chest>().chestUI = chestUI;

                for (int i = 0; i < chest.GetComponent<Chest>().inventory.Length - 1; i++)
                {
                    //choose a random loot table to pick from
                    int tableI = UnityEngine.Random.Range(0, spawnableLoot.Length - 1);
                    LootTable table = spawnableLoot[tableI];

                    // get a random loot object from a loot table
                    var loot = table.GetLoot();
                    if (loot != null)
                    {
                        chest.GetComponent<Chest>().inventory[i] = new InventoryItem();
                        chest.GetComponent<Chest>().inventory[i].item = loot.item;
                        chest.GetComponent<Chest>().inventory[i].quantity = loot.quantity;
                    }
                }
            }
        }
        catch (Exception ex)
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
}