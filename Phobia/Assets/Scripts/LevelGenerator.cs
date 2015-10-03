using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public List<GameObject> floorPrefabs;
    public GameObject wallPrefab;
    public int roomsToSpawn;
    List<Room> rooms = new List<Room>();
	
	private const int HORIZ_TILING = 100;
	private const int VERT_TILING = 80;

    // Use this for initialization
    void Start () {

        {
            Room thisRoom = new Room(this);
            thisRoom.Floor = floorPrefabs[Random.Range(0, floorPrefabs.Count)];
            thisRoom.Walls = wallPrefab;
            rooms.Add(thisRoom);
            thisRoom.Walls = (GameObject)Instantiate(thisRoom.Walls, new Vector3(0, 0, 0), Quaternion.identity);
			thisRoom.Floor = (GameObject)Instantiate(thisRoom.Floor, new Vector3(0, 0, 0), Quaternion.identity);
			thisRoom.Walls.name = "Room " + 0;
            thisRoom.Position = new Vector2(0,0);

			thisRoom.Walls.transform.Find("Lights").gameObject.SetActive(true);
			//GameObject.Find(thisRoom.Walls.name + "/Lights").SetActive(true);
        }

        for (int i = 1; i < roomsToSpawn; i++)
        {
            Room thisRoom = new Room(this);
            thisRoom.Floor = floorPrefabs[Random.Range(0, floorPrefabs.Count)];
            thisRoom.Walls = wallPrefab;


            Room adjRoom;

            do {
                adjRoom = rooms[Random.Range(0, rooms.Count)];
            } while (!adjRoom.IsNextToEmpty());


            adjRoom.SetAdj(adjRoom.randomEmpty(), thisRoom);

			thisRoom.Walls = (GameObject)Instantiate(thisRoom.Walls, new Vector3(HORIZ_TILING * thisRoom.Position.x, 0, VERT_TILING * thisRoom.Position.y), Quaternion.identity);
			thisRoom.Floor = (GameObject)Instantiate(thisRoom.Floor, new Vector3(HORIZ_TILING * thisRoom.Position.x, 0, VERT_TILING * thisRoom.Position.y), Quaternion.identity);
			thisRoom.Walls.name = "Room " + i;

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
        public GameObject Walls { get; set; }
        public GameObject Floor { get; set; }
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
            for (int index = 0; index < 4; index++)
            {
                if (parent.IsEmpty(Position+vectors[index]))
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
                    GameObject thisDoor = GameObject.Find(Walls.name + "/Doors/" + directions[i]);
                    GameObject adjDoor = GameObject.Find(adjRooms[i].Walls.name + "/Doors/" + directions[(i + 2) % 4]);

                    thisDoor.GetComponent<DoorControl>().goalDoor = adjDoor;
                    GameObject.Find(Walls.name + "/Door Blockers/" + directions[i]).SetActive(false);

                    adjDoor.GetComponent<DoorControl>().goalDoor = thisDoor;
                    GameObject.Find(adjRooms[i].Walls.name + "/Door Blockers/" + directions[(i + 2) % 4]).SetActive(false);
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
