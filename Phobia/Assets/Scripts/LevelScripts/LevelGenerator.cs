using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using System.Linq;

/**
 * 
 * Class which handles random level generation.
 * 
 **/
public class LevelGenerator : MonoBehaviour {
    
    public int roomsToSpawn = 20;
    public GameObject wallPrefab;
	public GameObject startPrefab;
	public List<GameObject> floorPrefabs;
	
    public int totalEnemies = 60;
	public int maxEnemiesPerRoom = 6;
	public GameObject enemy;
    public GameObject bossFloor;
    public GameObject boss;
	
    public GameObject trash;
	
	public int minWebs;
	public int maxWebs;
	public GameObject web;

	Dictionary<Vector2,Room> roomsDict = new Dictionary<Vector2,Room>();
    
    void Start () {
		
		Room thisRoom;
		Room adjRoom;
		Room chosenRoom;
		
		
		// Start room
		
		thisRoom = new Room(this);
		thisRoom.Floor = startPrefab; // pick random floor
		thisRoom.Walls = wallPrefab; // walls always come as part of the standard template
		thisRoom.Index = new Vector2(0,0);
		//rooms.Add(thisRoom);
		roomsDict.Add(thisRoom.Index,thisRoom);
		
		thisRoom.Instantiate(); // actually put it in the world
		thisRoom.Walls.name = "Room " + 0;
		thisRoom.spawnEnemies = false;
		thisRoom.spawnWebs = false;

		thisRoom.Walls.transform.Find("Lights").gameObject.SetActive(true);
		
		
		// normal rooms
		
        for (int i = 1; i < roomsToSpawn-1; i++) {

			thisRoom = new Room(this);
			thisRoom.Floor = floorPrefabs[Random.Range(0, floorPrefabs.Count)];
			thisRoom.Walls = wallPrefab;



			do {
				adjRoom = RandomRoom();
			} while (!adjRoom.IsNextToEmpty());


			adjRoom.SetAdj(adjRoom.RandomEmpty(), thisRoom);
			
			thisRoom.Instantiate();
			
			thisRoom.Walls.name = "Room " + i;

			roomsDict.Add(thisRoom.Index,thisRoom);
        }
		
		
		// boss room
		
		thisRoom = new Room(this);
		thisRoom.Floor = bossFloor;
		thisRoom.Walls = wallPrefab;

		do {
			adjRoom = RandomRoom();
		} while (!adjRoom.IsNextToEmpty());


		adjRoom.SetAdj(adjRoom.RandomEmpty(), thisRoom);

		thisRoom.Instantiate();
		
		thisRoom.Walls.name = "Boss Room";

		//rooms.Add(thisRoom);
		roomsDict.Add(thisRoom.Index,thisRoom);

		boss =  (GameObject)Instantiate(boss, thisRoom.Position + new Vector3(0, 4, 0), Quaternion.identity);
		boss.GetComponent<AIPath>().target = GameObject.FindWithTag("Player").transform;
		
		
		// link rooms
		
		for (int i = 0; i < roomsToSpawn*3; i++) {
			//chosenRoom = rooms[Random.Range(0, rooms.Count)];
			chosenRoom = RandomRoom();
			
			int chosen = chosenRoom.RandomNotEmpty();
			if (chosenRoom.adjRooms[chosen] == null ) {
				i+=9;
			}
			chosenRoom.SetAdj(chosen, get(chosenRoom.Index + Room.vectors[chosen]));
		}
		
		
        foreach(Room room in roomsDict.Values) {
            room.LinkDoors();
			room.PopulateCells();
			room.AddGraph();
			if (room.spawnWebs) {
				room.AddWebs(Random.Range(minWebs, maxWebs));
			}
        }

        AstarPath.active.Scan();
		
		// populate rooms
		
		for (int i = 0; i < totalEnemies; i++) {
			
			do {
				//chosenRoom = rooms[Random.Range(0, rooms.Count)];
				chosenRoom = RandomRoom();
			} while (chosenRoom.enemyCells.Count >= maxEnemiesPerRoom || chosenRoom.spawnEnemies == false);
			
			chosenRoom.AddEnemy();
		}
    }
	
	// Update is called once per frame
	void Update () {

    }


    // does there exist a room in the location given?
    public bool IsEmpty(Vector2 pos) {
		return !roomsDict.ContainsKey(pos);
    }
	
	// does there exist a room in the location given?
    private Room get(Vector2 pos) {
		Room output = null;
		roomsDict.TryGetValue(pos, out output);
		return output;
    }
	
	private Room RandomRoom() {
		return roomsDict.ElementAt(Random.Range(0, roomsDict.Count)).Value;
	}

    class Room {
	
		private const int HORIZ_TILING = 100;
		private const int VERT_TILING = 80;

        public static string[] directions = {"North Door", "East Door", "South Door", "West Door" };
        public static Vector2[] vectors = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0)};
	
        public LevelGenerator parent;
		
        public GameObject Walls { get; set; }
        public GameObject Floor { get; set; }
		
        public Vector2 Index { get; set; }
        public Vector3 Position {
			get {
				return new Vector3(HORIZ_TILING * Index.x, 0, VERT_TILING * Index.y);
			}
		}
        public Room[] adjRooms = new Room[4];
		
		public bool spawnWebs = true;
		public bool spawnEnemies = true;
		
		public List<GameObject> cells = new List<GameObject>();
		public List<GameObject> enemyCells = new List<GameObject>();
		public List<GameObject> webCells = new List<GameObject>();

        public Room(LevelGenerator levelGenerator) {
            this.parent = levelGenerator;
        }

        // does this room have an adjacent place for a room that is not occupied by a room already
        public bool IsNextToEmpty() {
            for (int index = 0; index < 4; index++) {
                if (parent.IsEmpty(Index+vectors[index])) {
                    return true;
                }
            }
            return false;
        }
		
		public void Instantiate() {
			Walls = (GameObject)GameObject.Instantiate(Walls, Position, Quaternion.identity);
			Floor = (GameObject)GameObject.Instantiate(Floor, Position, Quaternion.identity);
		}

        // Set up the bidirectional relationship between to rooms that will later ensure the doors are linked
        public void SetAdj(int adjPos, Room adjRoom) {
            adjRooms[adjPos] = adjRoom;
            adjRoom.adjRooms[(adjPos + 2) % 4] = this;

            adjRoom.Index = Index + vectors[adjPos];
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
					
					GameObject.Instantiate(parent.trash, (Position + adjRooms[i].Position)/2 , Quaternion.identity);
                }
            }
        }

        // returns: 0-3  signalling a direction which can give an empty room
        //          -1   if no such direction exists
        public int RandomEmpty() {
            if (!IsNextToEmpty()) {
                return -1;
            }

            int pos;
            do {
                pos = Random.Range(0, 4);
            } while (!parent.IsEmpty(Index+vectors[pos]));
            return pos;
        }

        // returns: 0-3  signalling a direction which can give an empty room
        public int RandomNotEmpty() {

            int pos;
            do {
                pos = Random.Range(0, 4);
            } while (parent.IsEmpty(Index+vectors[pos]));
            return pos;
        }
		
		public void AddGraph() {
			AstarData data = AstarPath.active.astarData;
			// This creates a Grid Graph
			GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;
			// Setup a grid graph with some values
			gg.width = 50;
			gg.depth = 40;
			gg.center = Position;
			// Updates internal size from the above values
			gg.UpdateSizeFromWidthDepth();
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

            GameObject thisEnemy = (GameObject)GameObject.Instantiate(parent.enemy, (chosenCell.transform.position + new Vector3(0, 4, 0)), Quaternion.identity);

            thisEnemy.GetComponent<AIPath>().target = GameObject.FindWithTag("Player").transform;

        }
		
		public void AddWebs(int amount) {
			
			for (int i = 0; i < amount; i++) {
				GameObject chosenCell;
				do {
					chosenCell = cells[Random.Range(0, cells.Count)];
				} while(webCells.Contains(chosenCell));
				webCells.Add(chosenCell);

				GameObject thisWeb = (GameObject)GameObject.Instantiate(parent.web, (chosenCell.transform.position + new Vector3(0, 1.1f, 0)), Quaternion.identity);
			}
        }
    }
}
