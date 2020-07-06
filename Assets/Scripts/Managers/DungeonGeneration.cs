/* DUNGEON GENERATION
 * Author: Trey Hall
 * 
 * This API can be used to procedurally generate a series of rooms to form a dungeon, 
 * similar to Binding of Isaac
 * 
 * TODO:
 * Add option to generate mazes inside of rooms
 * Add option to generate hallways between rooms
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DungeonGeneration
{
    /// <summary>
    /// Vector2 is a class containing an x and y value pair
    /// </summary>
    public struct Vector2
    {
        private int x, y;
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2(float x, float y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }

        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
    }
    public partial class Room<T>
    {
        public T Root { get; set; }

        byte roomCount = 0;

        int id = 0;
        private Room<T> to_north = null;
        private Room<T> to_south = null;
        private Room<T> to_east = null;
        private Room<T> to_west = null;

        public int ID { get { return id; } set { id = value; } }
        public List<T> Tiles { get; set; } = new List<T>();
        public List<T> WorldObjects { get; set; } = new List<T>();
        public byte AttachedRoomCount
        {
            get
            {
                return roomCount;
            }
            set
            {
                roomCount = value;
            }
        }
        public int[] AttachedRoomIDs
        {
            get
            {
                return new int[]
                {
                    (ToNorth != null) ? ToNorth.ID : 0,
                    (ToSouth != null) ? ToSouth.ID : 0,
                    (ToEast != null) ? ToEast.ID : 0,
                    (ToWest != null) ? ToWest.ID : 0,
                };
            }
        }

        public Vector2 Origin { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        // Adjacent Rooms
        public Room<T> ToNorth { get { return to_north; } set { to_north = value; } }
        public Room<T> ToSouth { get { return to_south; } set { to_south = value; } }
        public Room<T> ToEast { get { return to_east; } set { to_east = value; } }
        public Room<T> ToWest { get { return to_west; } set { to_west = value; } }

        public Room(Vector2 origin, int width = 0, int height = 0)
        {
            Width = width;
            Height = height;
            Origin = origin;
        }

        public void GetRooms(ref List<Room<T>> rooms)
        {
            get_rooms(ref rooms, ref id);
        }

        private void get_rooms(ref List<Room<T>> rooms, ref int lastRoomID)
        {
            rooms.Add(this);
            if (AttachedRoomCount == 1)
                return;

            if (ToNorth != null && ToNorth.ID != lastRoomID)
                ToNorth.get_rooms(ref rooms, ref id);
            if (ToSouth != null && ToSouth.ID != lastRoomID)
                ToSouth.get_rooms(ref rooms, ref id);
            if (ToEast != null && ToEast.ID != lastRoomID)
                ToEast.get_rooms(ref rooms, ref id);
            if (ToWest != null && ToWest.ID != lastRoomID)
                ToWest.get_rooms(ref rooms, ref id);
        }

    }
    public class DungeonGeneration<T>
    {
        #region MODEL_CONSTANTS
        #region TILES
        private T NORTH_WALL { get; set; }
        private T SOUTH_WALL { get; set; }
        private T EAST_WALL { get; set; }
        private T WEST_WALL { get; set; }

        private T NORTHWEST_CORNER { get; set; }
        private T NORTHEAST_CORNER { get; set; }
        private T SOUTHWEST_CORNER { get; set; }
        private T SOUTHEAST_CORNER { get; set; }

        private T FLOOR { get; set; }
        #endregion
        #region WORLD_OBJECTS
        private T NORTH_DOOR { get; set; }
        private T SOUTH_DOOR { get; set; }
        private T EAST_DOOR { get; set; }
        private T WEST_DOOR { get; set; }

        private T SMALL_CHEST { get; set; }
        #endregion
        #endregion

        Random rand;

        int chestMin = 0, chestMax = 0;

        /// <summary>
        /// Initializes all model constant values for Walls/Corners/Floors/Etc.
        /// </summary>
        /// <param name="northWall"></param>
        /// <param name="southWall"></param>
        /// <param name="eastWall"></param>
        /// <param name="westWall"></param>
        /// <param name="northwestCorner"></param>
        /// <param name="northeastCorner"></param>
        /// <param name="southwestCorner"></param>
        /// <param name="southeastCorner"></param>
        /// <param name="floor"></param>
        public DungeonGeneration(T northWall, T southWall, T eastWall, T westWall,
                                  T northwestCorner, T northeastCorner, T southwestCorner, T southeastCorner,
                                  T northDoor, T southDoor, T eastDoor, T westDoor,
                                  T floor, T smallChest, int seed = 0)
        {
            NORTH_WALL = northWall;
            SOUTH_WALL = southWall;
            EAST_WALL = eastWall;
            WEST_WALL = westWall;

            NORTHWEST_CORNER = northwestCorner;
            NORTHEAST_CORNER = northeastCorner;
            SOUTHWEST_CORNER = southwestCorner;
            SOUTHEAST_CORNER = southeastCorner;

            FLOOR = floor;

            NORTH_DOOR = northDoor;
            SOUTH_DOOR = southDoor;
            EAST_DOOR = eastDoor;
            WEST_DOOR = westDoor;

            SMALL_CHEST = smallChest;

            rand = new Random(seed);
        }

        public Room<T> BuildDungeon(Vector2 origin, Vector2 width, Vector2 height, Vector2 rooms, Vector2 chests)
        {
            List<T> temp = new List<T>();

            chestMin = chests.X;
            chestMax = chests.Y;

            if (rooms.X < 1) rooms.X = 1;
            Room<T> room = new Room<T>(origin, 0, 0);
            int maxRooms = rand.Next(rooms.X, rooms.Y);

            //for (int r = 1; r <= rooms; r++)
            BuildRoom(1, maxRooms, origin.X, origin.Y, width.X, width.Y, height.X, height.Y, ref room, CardinalDirections.NORTH);

            return room;
        }

        // Recursive Function 
        // Base case: roomNum > maxRooms
        private int BuildRoom(int roomNum, int maxRooms, int startX, int startY, int minWidth, int maxWidth, int minHeight, int maxHeight, ref Room<T> lastRoom, CardinalDirections roomDir)
        {
            List<T> temp = new List<T>();

            // base case
            if (roomNum > maxRooms)
                return roomNum;


            int roomWidth = rand.Next(minWidth, maxWidth);
            int roomHeight = rand.Next(minHeight, maxHeight);
            int doorX = 0, doorY = 0;
            if (roomNum != 1)
                switch (roomDir)
                {
                    case CardinalDirections.NORTH:
                        startX = lastRoom.Origin.X;
                        startY = lastRoom.Origin.Y - roomHeight - 1;

                        doorY = startY + roomHeight - 1;
                        doorX = ((startX + roomWidth) / 2);
                        break;
                    case CardinalDirections.SOUTH:
                        startX = lastRoom.Origin.X;
                        startY = lastRoom.Origin.Y + lastRoom.Height;

                        doorY = startY;
                        doorX = ((startX + roomWidth) / 2);
                        break;
                    case CardinalDirections.EAST:
                        startX = lastRoom.Origin.X + lastRoom.Width;
                        startY = lastRoom.Origin.Y;

                        doorY = ((startY + roomHeight) / 2);
                        doorX = startX;
                        break;
                    case CardinalDirections.WEST:
                        startX = lastRoom.Origin.X - roomWidth;
                        startY = lastRoom.Origin.Y;

                        doorY = ((startY + roomHeight) / 2);
                        doorX = startX + roomWidth - 1;
                        break;
                }

            Room<T> room = new Room<T>(new Vector2(startX, startY), roomWidth, roomHeight);

            for (int y = startY; y < roomHeight + startY; y++)
            {
                for (int x = startX; x < roomWidth + startX; x++)
                {
                    temp.Add(placeCorners(startX, startY, x, y, roomWidth + startX, roomHeight + startY));
                    // clean up default values

                    temp.Add(placeWalls(startX, startY, x, y, roomWidth + startX, roomHeight + startY));
                    // clean up default values

                    temp.Add(placeFloors(startX, startY, x, y, roomWidth + startX, roomHeight + startY));
                    // clean up default values

                }
            }
            temp.RemoveAll(t => t == null);

            SetAdjacency(roomNum, roomDir, ref room, ref lastRoom);
            room.Tiles = temp;
            room.ID = roomNum;
            PrepareWorldObjects(room.Tiles.Count, room.WorldObjects);

            //Generate door for this room and respective door in last room
            if (roomNum != 1)
                placeDoors(startX, startY, roomWidth, roomHeight, doorX, doorY, lastRoom, roomDir, room.WorldObjects);

            // generate loot chests and place down
            placeChests(startX, startY, roomWidth, roomHeight, room.WorldObjects);

            var availability = FindAvailability(room);
            int roomsToAdd = rand.Next(0, availability.Count);

            if (roomsToAdd > maxRooms)
                roomsToAdd = maxRooms;
            int maxRoomNum = roomNum;
            for (int i = 1; i <= roomsToAdd; i++)
            {
                var dir = (CardinalDirections)Enum.Parse(typeof(CardinalDirections), i.ToString());
                // the startx and starty should be the position of the last door, offset by a certain amount in the direction ofthe axis not wall dominant.
                maxRoomNum = BuildRoom(i + maxRoomNum, maxRooms, startX, startY, minWidth, maxWidth, minHeight, maxHeight, ref room, dir);
            }

            return maxRoomNum;
        }
        private void PrepareWorldObjects (int count, List<T> worldObjects)
        {
            for (int i = 0; i < count; i++)
                worldObjects.Add(default);
        }
        private void SetAdjacency(int roomNum, CardinalDirections roomDir, ref Room<T> room, ref Room<T> lastRoom)
        {
            if (roomNum != 1)
            {
                switch (roomDir)
                {
                    case CardinalDirections.NORTH:
                        room.ToSouth = lastRoom;
                        lastRoom.ToNorth = room;
                        break;
                    case CardinalDirections.SOUTH:
                        room.ToNorth = lastRoom;
                        lastRoom.ToSouth = room;
                        break;
                    case CardinalDirections.EAST:
                        room.ToWest = lastRoom;
                        lastRoom.ToEast = room;
                        break;
                    case CardinalDirections.WEST:
                        room.ToEast = lastRoom;
                        lastRoom.ToWest = room;
                        break;
                }
            }
            else
                lastRoom = room;
        }
        private List<CardinalDirections> FindAvailability(Room<T> room)
        {
            // generate a random number of rooms to create
            // based on available walls
            List<CardinalDirections> available = new List<CardinalDirections>();
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case (int)CardinalDirections.NORTH:
                        if (room.ToNorth == null)
                            available.Add(CardinalDirections.NORTH);
                        break;
                    case (int)CardinalDirections.SOUTH:
                        if (room.ToSouth == null)
                            available.Add(CardinalDirections.SOUTH);
                        break;
                    case (int)CardinalDirections.EAST:
                        if (room.ToEast == null)
                            available.Add(CardinalDirections.EAST);
                        break;
                    case (int)CardinalDirections.WEST:
                        if (room.ToWest == null)
                            available.Add(CardinalDirections.WEST);
                        break;
                }
            }
            return available;
        }

        private T placeCorners(int startX, int startY, int x, int y, int roomWidth, int roomHeight)
        {
            // If the x and y value pair does not equate to a corner, then return the default
            // value for type T and stop execution for this function
            if ((x != startX && x != roomWidth - 1) && (y != startY && y != roomHeight - 1))
                return default;

            // if x and y equates to TopLeft Corner
            if (x == startX && y == startY)
            {
                //place north west corner
                return NORTHWEST_CORNER;
            }

            // if x and y equates to TopRight corner
            else if (x == roomWidth - 1 && y == startY)
            {
                //place north east corner
                return NORTHEAST_CORNER;

            }

            // if x and y equates to BottomLeft corner
            else if (y == roomHeight - 1 && x == startX)
            {
                //place south west corner
                return SOUTHWEST_CORNER;

            }

            // if x and y equates to BottomRight corner
            else if (y == roomHeight - 1 && x == roomWidth - 1)
            {
                //place south east corner
                return SOUTHEAST_CORNER;

            }
            else return default;
        }
        private T placeWalls(int startX, int startY, int x, int y, int roomWidth, int roomHeight)
        {
            // If the x and y value pair does not equate to a wall, then return the default
            // value for type T and stop execution for this function
            if ((x == startX || x == roomWidth - 1) && (y == startY || y == roomHeight - 1))
                return default;

            // If x is on the Left Side of the room
            if (x == startX)
            {
                // place west wall
                return WEST_WALL;
            }
            // If x is on the Right Side of the room
            else if (x == roomWidth - 1)
            {
                // place east wall
                return EAST_WALL;
            }
            // If y is on the Top Side of the room
            else if (y == startY)
            {
                // place north wall
                return NORTH_WALL;
            }
            // If Y is on the Bottom Side of the room
            else if (y == roomHeight - 1)
            {
                // place south wall
                return SOUTH_WALL;
            }
            else return default;
        }
        private T placeFloors(int startX, int startY, int x, int y, int roomWidth, int roomHeight)
        {
            if ((x >= startX + 1 && x <= roomWidth - 2) && (y >= startY + 1 && y <= roomHeight - 2))
                return FLOOR;
            else return default;
        }

        public enum CardinalDirections { NORTH, SOUTH, EAST, WEST }
        private void placeDoors(int startX, int startY, int roomWidth, int roomHeight, int doorX, int doorY, Room<T> lastRoom, CardinalDirections side, List<T> worldObjects)
        {
            switch (side)
            {
                case CardinalDirections.NORTH:
                    lastRoom.WorldObjects[(doorX - lastRoom.Origin.X) + (lastRoom.Height - 1) * roomWidth] = NORTH_DOOR;
                    worldObjects[(doorX - startX) + (doorY - startY) * roomWidth] = SOUTH_DOOR;
                    break;
                case CardinalDirections.SOUTH:
                    lastRoom.WorldObjects[(doorX - lastRoom.Origin.X) + (lastRoom.Height - 1) * roomWidth] = SOUTH_DOOR;
                    worldObjects[(doorX - startX) + (doorY - startY) * roomWidth] = NORTH_DOOR;
                    break;
                case CardinalDirections.EAST:
                    lastRoom.WorldObjects[(lastRoom.Width - 1) + (doorY - lastRoom.Origin.Y) * roomWidth] = EAST_DOOR;
                    worldObjects[(doorX - startX) + (doorY - startY) * roomWidth] = WEST_DOOR;
                    break;
                case CardinalDirections.WEST:
                    lastRoom.WorldObjects[(lastRoom.Width - 1) + (doorY - lastRoom.Origin.Y) * roomWidth] = WEST_DOOR;
                    worldObjects[(doorX - startX) + (doorY - startY) * roomWidth] = EAST_DOOR;
                    break;
            }
        }
        private void placeChests (int startX, int startY, int roomWidth, int roomHeight, List<T> worldObjects)
        {
            try
            {
                int chestX = 0, chestY = 0, chestCount = 0;
                chestCount = rand.Next(chestMin, chestMax);
                for (int i = 0; i < chestCount; i++)
                {
                    chestX = rand.Next(startX + 1, roomWidth - 1);
                    chestY = rand.Next(startY + 1, roomHeight - 1);

                    worldObjects[(chestX - startX) + (chestY - startY) * roomWidth] = SMALL_CHEST;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
