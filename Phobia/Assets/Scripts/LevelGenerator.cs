using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public List<GameObject> roomPrefabs;
    public int roomsToSpawn;
    List<Room> rooms = new List<Room>();

    // Use this for initialization
    void Start () {

        {
            Room thisRoom = new Room(this);
            thisRoom.Objecto = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            rooms.Add(thisRoom);
            thisRoom.Objecto.name = "Room " + 0;
            thisRoom.Objecto = (GameObject)Instantiate(thisRoom.Objecto, new Vector3(0, 0, 0), Quaternion.identity);
            thisRoom.Position = new Vector2(0,0);
        }

        for (int i = 1; i < roomsToSpawn; i++)
        {
            Room thisRoom = new Room(this);
            thisRoom.Objecto = roomPrefabs[Random.Range(0, roomPrefabs.Count)];

            thisRoom.Objecto.name = "Room " + i;

            Room adjRoom;

            do {
                adjRoom = rooms[Random.Range(0, rooms.Count)];
            } while (!adjRoom.IsNextToEmpty());


            adjRoom.SetAdj(adjRoom.randomEmpty(), thisRoom);

            thisRoom.Objecto = (GameObject)Instantiate(thisRoom.Objecto, new Vector3(44 * thisRoom.Position.x, 0, 30 * thisRoom.Position.y), Quaternion.identity);

            rooms.Add(thisRoom);
        }

        foreach(Room room in rooms)
        {
            room.LinkDoors();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    class Room
    {
        public LevelGenerator parent;
        public GameObject Objecto { get; set; }
        public Room[] adjRooms = new Room[4];
        public Vector2 Position { get; set; }

        private string[] directions = {"North Door", "East Door", "South Door", "West Door" };
        private Vector2[] vectors = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0)};

        public Room(LevelGenerator levelGenerator)
        {
            this.parent = levelGenerator;
        }

        public bool IsNextToEmpty()
        {
            foreach (Room r in adjRooms)
            {
                if (r == null)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetAdj(int adjPos, Room adjRoom)
        {
            adjRooms[adjPos] = adjRoom;

            adjRoom.Position = Position + vectors[adjPos];
        }

        public void LinkDoors()
        {
            for (int i = 0; i < 4; i++)
            {
                if (adjRooms[i] != null) {
                    GameObject thisDoor = GameObject.Find(Objecto.name + "/Doors/" + directions[i]);
                    GameObject adjDoor = GameObject.Find(adjRooms[i].Objecto.name + "/Doors/" + directions[(i + 2) % 4]);

                    thisDoor.GetComponent<DoorControl>().goalDoor = adjDoor;
                    thisDoor.GetComponent<DoorControl>().goalRoom = adjRooms[i].Objecto;
                    GameObject.Find(Objecto.name + "/DoorBlockers/" + directions[i]).SetActive(false);

                    adjDoor.GetComponent<DoorControl>().goalDoor = thisDoor;
                    adjDoor.GetComponent<DoorControl>().goalRoom = Objecto;
                    GameObject.Find(adjRooms[i].Objecto.name + "/DoorBlockers/" + directions[(i + 2) % 4]).SetActive(false);
                }
            }
        }

        public int randomEmpty()
        {
            if (!IsNextToEmpty())
            {
                return -1;
            }

            int pos;
            do
            {
                pos = Random.Range(0, 4);
            } while (!parent.IsEmpty(Position+vectors[pos]));
            return pos;
        }
    }

    public bool IsEmpty(Vector2 pos)
    {
        foreach (Room room in rooms)
        {
            if (room.Position == pos)
            {
                return false;
            }
        }
        return true;
    }
}
