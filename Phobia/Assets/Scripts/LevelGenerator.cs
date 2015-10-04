using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
    
    public int roomsToSpawn = 20;
    public GameObject wallPrefab;
	public List<GameObject> floorPrefabs;
    public int totalEnemies = 60;
	public int maxEnemiesPerRoom = 6;
	public GameObject enemy;
	
    List<Room> rooms = new List<Room>();
	
	private const int HORIZ_TILING = 100;
	private const int VERT_TILING = 80;
    
    void Start () {
        {
            Room thisRoom = new Room(this);
            thisRoom.Floor = floorPrefabs[Random.Range(0, floorPrefabs.Count)]; // pick random floor
            thisRoom.Walls = wallPrefab; // walls always come as part of the standard template
            rooms.Add(thisRoom);
            thisRoom.Walls = (GameObject)Instantiate(thisRoom.Walls, new Vector3(0, 0, 0), Quaternion.identity); // actually put it in the world
			thisRoom.Floor = (GameObject)Instantiate(thisRoom.Floor, new Vector3(0, 0, 0), Quaternion.identity);
			thisRoom.Walls.name = "Room " + 0;
            thisRoom.Position = new Vector2(0,0);

			thisRoom.Walls.transform.Find("Lights").gameObject.SetActive(true);
        }

        for (int i = 1; i < roomsToSpawn; i++) {
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

        foreach(Room room in rooms) {
            room.LinkDoors();
			room.PopulateCells();
        }
		for (int i = 0; i < totalEnemies; i++) {
			
			Room chosenRoom;
			
			do {
				chosenRoom = rooms[Random.Range(0, rooms.Count)];
			} while (chosenRoom.enemyCells.Count >= maxEnemiesPerRoom);
			
			chosenRoom.AddEnemy();
		}




        AstarPath.active.Scan();

    }
	
	// Update is called once per frame
	void Update () {

    }


    // does there exist a room in the location given?
    public bool IsEmpty(Vector2 pos) {
        foreach (Room room in rooms) {
            if (room.Position == pos) {
                return false;
            }
        }
        return true;
    }

    class Room {
        public LevelGenerator parent;
        public GameObject Walls { get; set; }
        public GameObject Floor { get; set; }
        public Room[] adjRooms = new Room[4];
        public Vector2 Position { get; set; }
		public List<GameObject> enemyCells = new List<GameObject>();
		public List<GameObject> cells = new List<GameObject>();

        private string[] directions = {"North Door", "East Door", "South Door", "West Door" };
        private Vector2[] vectors = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0)};

        public Room(LevelGenerator levelGenerator) {
            this.parent = levelGenerator;
        }

        // does this room have an adjacent place for a room that is not occupied by a room already
        public bool IsNextToEmpty() {
            for (int index = 0; index < 4; index++) {
                if (parent.IsEmpty(Position+vectors[index])) {
                    return true;
                }
            }
            return false;
        }

        // Set up the bidirectional relationship between to rooms that will later ensure the doors are linked
        public void SetAdj(int adjPos, Room adjRoom) {
            adjRooms[adjPos] = adjRoom;

            adjRoom.Position = Position + vectors[adjPos];
        }

        // Open and link the doors between the appropriate rooms
        public void LinkDoors() {

            for (int i = 0; i < 4; i++) {
                if (adjRooms[i] != null) {
                    // find the game object
                    GameObject thisDoor = Walls.transform.Find("Doors/" + directions[i]).gameObject;
                    GameObject adjDoor = adjRooms[i].Walls.transform.Find("Doors/" + directions[(i + 2) % 4]).gameObject;

                    thisDoor.GetComponent<DoorControl>().goalDoor = adjDoor; // link the goal
                    Walls.transform.Find("Door Blockers/" + directions[i]).gameObject.SetActive(false); // disable the blocker

                    // and do the reverse
                    adjDoor.GetComponent<DoorControl>().goalDoor = thisDoor;
                    adjRooms[i].Walls.transform.Find("Door Blockers/" + directions[(i + 2) % 4]).gameObject.SetActive(false);
                }
            }
        }

        // returns: 0-3  signalling a direction which can give an empty room
        //          -1   if no such direction exists
        public int randomEmpty() {
            if (!IsNextToEmpty()) {
                return -1;
            }

            int pos;
            do {
                pos = Random.Range(0, 4);
            } while (!parent.IsEmpty(Position+vectors[pos]));
            return pos;
        }
		
		public void PopulateCells() {
			foreach (Transform child in Floor.transform) {
				foreach(Transform grandChild in child) {
					cells.Add(grandChild.gameObject);
				}
			}
			Debug.Log("" + cells.Count);
		}
		
		public void AddEnemy() {
			
			GameObject chosenCell;
			do {
				chosenCell = cells[Random.Range(0, cells.Count)];
			} while(enemyCells.Contains(chosenCell));
			enemyCells.Add(chosenCell);

            GameObject thisEnemy = (GameObject)Instantiate(parent.enemy, (chosenCell.transform.position + new Vector3(0, 1, 0)), Quaternion.identity);

            thisEnemy.GetComponent<AIPath>().target = GameObject.FindWithTag("Player").transform;

        }
    }
}
