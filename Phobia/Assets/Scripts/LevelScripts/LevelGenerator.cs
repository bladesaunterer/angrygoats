using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using System.Linq;
using System;

/// <summary>
/// Purpose: The main class of each level. This class generates a random room based on prefab and integer inputs.<para/>
/// </summary>
public class LevelGenerator : MonoBehaviour {
	
	public string seed = "";
	public int actseed = -1;
	
	public int roomsToSpawn = 20;
	public GameObject roomPrefab; // This is the thing with lighting and walls and doors etc
	public GameObject startPrefab; // The floor of the first room
	public List<GameObject> floorPrefabs; // The list of rooms that can be spawned (all equally likely)

	public int totalEnemies = 60;
	public int maxEnemiesPerRoom = 6;

	public List<GameObject> enemies; // The list of enemies to spawn
	public List<int> enemyCommonness; // The frequency of the enemies in the above list

	public GameObject bossFloor; // Floor of the boss room
	public GameObject boss;

	public int minWebs;
	public int maxWebs;
	public GameObject web;

	public GameObject trash; // Just for developer use. Spawns between rooms so that we can easily see which rooms are linked

	// Currently spawned rooms and their position on the grid
	// The key is not the actual co-ordinates of the room. The room just right of the start room is (1, 0) etc...
	Dictionary<Vector2, RoomControl> roomsDict = new Dictionary<Vector2, RoomControl>(); 
	public GameObject minimapUI;
	
	// This things sorts out all the pathfinding stuff
	public AstarPath aStarGrids;
	
	/// Start generates the rooms adds enemies, boss, webs and minimap
	void Start () {
		
		// These variables will change their meaning over the course of the method bu
		RoomControl thisRoom;
		RoomControl adjRoom;
		RoomControl chosenRoom;
		Vector2 roomVector;
		Vector2 roomVectorRel;

		if (seed == "") {
			// Generate seed if there was none given
			actseed = System.Environment.TickCount;
		} else {
			// Assign seed based on specified values
			string[] parts = seed.ToString().Split('#');
			actseed = Int32.Parse(parts[0]);
			roomsToSpawn = Int32.Parse(parts[1]);
			totalEnemies = Int32.Parse(parts[2]);
			maxEnemiesPerRoom = Int32.Parse(parts[3]);
			minWebs = Int32.Parse(parts[4]);
			maxWebs = Int32.Parse(parts[5]);
		}
		UnityEngine.Random.seed = actseed;
		
		// Reference to the minimap, so it can be generated in unison with the actual floor
		MinimapScript minimapScript = minimapUI.GetComponent<MinimapScript>();

		// Start room
		thisRoom = ((GameObject)Instantiate(roomPrefab, new Vector3(0, 0, 0), Quaternion.identity)).GetComponent<RoomControl>();
		thisRoom.minimapUI = minimapUI;
		thisRoom.Floor = startPrefab; // pick set floor

		thisRoom.Index = new Vector2(0, 0);
		minimapScript.GenerateMapBlock(thisRoom.Index);

		roomsDict.Add(thisRoom.Index, thisRoom);

		thisRoom.gameObject.name = "Room " + 0;
		thisRoom.spawnEnemies = false; // first room should be safe
		thisRoom.spawnWebs = false;

		thisRoom.gameObject.transform.Find("Lights").gameObject.SetActive(true);

		// Normal rooms
		// First room and boss room are counted in "roomsToSpawn", but aren't done in this loop.
		for (int i = 1; i < roomsToSpawn - 1; i++) {

			int a = 0;
			do { // Find empty room to put the new room adjacent to
				adjRoom = RandomRoom();
				if (a > 1000) {
					throw new System.Exception("Infinite loop occuring.");
				}
				a++;
			} while (!IsNextToEmpty(adjRoom));

			roomVectorRel = RandomEmpty(adjRoom);
			roomVector = adjRoom.Index + roomVectorRel;

			thisRoom = ((GameObject)Instantiate(roomPrefab, RoomControl.IndexToPosition(roomVector), Quaternion.identity)).GetComponent<RoomControl>();
			thisRoom.minimapUI = minimapUI;
			thisRoom.Floor = floorPrefabs[UnityEngine.Random.Range(0, floorPrefabs.Count)];
			thisRoom.Index = roomVector;
			minimapScript.GenerateMapBlock(thisRoom.Index);
			thisRoom.gameObject.name = "Room " + i;

			SetAdj(adjRoom, roomVectorRel, thisRoom);

			roomsDict.Add(thisRoom.Index, thisRoom);
		}

		// Boss room
		int b = 0;
		do { // Find empty room to put the new room adjacent to
			adjRoom = RandomRoom();
			if (b > 1000) {
				throw new System.Exception("Infinite loop occuring.");
			}
			b++;
		} while (!IsNextToEmpty(adjRoom));

		roomVectorRel = RandomEmpty(adjRoom);
		roomVector = adjRoom.Index + roomVectorRel;

		thisRoom = ((GameObject)Instantiate(roomPrefab, RoomControl.IndexToPosition(roomVector), Quaternion.identity)).GetComponent<RoomControl>();
		thisRoom.minimapUI = minimapUI;
		thisRoom.Floor = bossFloor;
		thisRoom.Index = roomVector;
		minimapScript.GenerateMapBlock(thisRoom.Index);
		thisRoom.gameObject.name = "Boss Room";

		thisRoom.spawnEnemies = false;
		
		SetAdj(adjRoom, roomVectorRel, thisRoom);

		roomsDict.Add(thisRoom.Index,thisRoom);
		
		// Special stuff for each boss. Should be refactored
		if (boss.transform.name == "Boss3") {
			int temp1 = UnityEngine.Random.Range (-18, 18);
			int temp2 = UnityEngine.Random.Range (-10, 10);
			boss = (GameObject)Instantiate (boss, thisRoom.transform.position + new Vector3 (temp1, 2, temp2), Quaternion.identity);
			BossThree bs3 = boss.GetComponent<BossThree> ();
			bs3.roomCont = thisRoom.GetComponent<RoomControl> ();
		} else if (boss.transform.name == "FlyBoss" || boss.transform.name == "FlyBossTest") {
			boss = (GameObject)Instantiate (boss, thisRoom.transform.position + new Vector3 (0, 4, 0), Quaternion.identity);
			boss.GetComponent<EnemyControl>().home = thisRoom.transform;
			boss.GetComponent<AIPath>().enabled = false; // Flying bosses movement is disable until the player enters the room
		} else {
			boss = (GameObject)Instantiate (boss, thisRoom.transform.position + new Vector3 (0, 2, 0), Quaternion.identity);
		}
		boss.GetComponent<AIPath>().target = GameObject.FindWithTag("Player").transform;
		thisRoom.AddBoss (boss);
		
		// link rooms
		// The room adding loop above does not allow loops. Here extra doors are added so that there can be loops.
		// Depending on level layout, extra doors may not be addable.
		// Loop finds two adjacent rooms. If there is no door already there, a door is added and loop var incremented by one
		// If there already was a door, increment by 0.1
		// This means there will never be an infinite loop
		for (float i = 0f; i < roomsToSpawn/3.3f; i+=0.1f) {
			chosenRoom = RandomRoom();
			
			Vector2 chosen = RandomNotEmpty(chosenRoom);
			if (!chosenRoom.adjRoomsDict.ContainsKey(chosen)) {
				i+=0.9f;
			}
			SetAdj(chosenRoom, chosen, get(chosenRoom.Index + chosen));
		}

		minimapScript.PlayerEntersRoom(new Vector2(0, 0), get(new Vector2(0, 0)).adjRoomsDict.Keys.ToList());

		// add pathfinding graph and webs
		foreach (RoomControl room in roomsDict.Values) {
			room.PopulateCells();
			room.AddGraph();
			if (room.spawnWebs) {
				room.AddWebs(web, UnityEngine.Random.Range(minWebs, maxWebs));
			}
		}

		// Sets up the pathfinding based on the generated map
		AstarPath.active.Scan();
		
		// Modify total enemies so that it is a legal value
		int maxLegalEnemies = (roomsToSpawn-2) * maxEnemiesPerRoom;
		if (totalEnemies > maxLegalEnemies) {
			totalEnemies = maxLegalEnemies;
		}
		if (totalEnemies < 0) {
			totalEnemies = 0;
		}
		
		
		// Populate rooms
		// A room is randomly chosen with equal probability, so long as the room doesn't have too many already
		// A raffle is held on the enemy types to decide which enemy type spawns.
		// enemyCommonness decides how many tickets that enemy type has
		// Winning ticket is chosen and we iterate over the enemy types till we find the winner
		
		int totalTickets = enemyCommonness.Sum();
		
		for (int i = 0; i < totalEnemies; i++) {
			
			int c = 0;
			do {
				chosenRoom = RandomRoom();
				if (c > 1000) {
					throw new System.Exception("Infinite loop occurring.");
				}
				c++;
			} while (chosenRoom.enemies.Count >= maxEnemiesPerRoom || chosenRoom.spawnEnemies == false);

			int winningTicket = UnityEngine.Random.Range(0, totalTickets);
			
			int hopeful = 0;
			int sum = enemyCommonness[hopeful];
			
			while (sum <= winningTicket) {
				hopeful++;
				sum += enemyCommonness[hopeful];
			}
			
			chosenRoom.AddEnemy(enemies[hopeful]);
		}
		// Generate seed for recreate
		seed = actseed.ToString() + "#" + roomsToSpawn.ToString() + "#" + totalEnemies.ToString()
			+ "#" + maxEnemiesPerRoom.ToString() + "#" + minWebs.ToString() + "#" + maxWebs.ToString() + "#" + Application.loadedLevelName;
		aStarGrids.Scan();
	}

	// Does there exist no room in the location given?
	public bool IsEmpty(Vector2 index) {
		return !roomsDict.ContainsKey(index);
	}

	// Give me the room in the location given
	private RoomControl get(Vector2 pos) {
		RoomControl output = null;
		roomsDict.TryGetValue(pos, out output);
		return output;
	}

	// Does this room have an adjacent place for a room that is not occupied by a room already?
	private bool IsNextToEmpty(RoomControl room) {
		for (int index = 0; index < 4; index++) {
			if (IsEmpty(room.Index + RoomControl.vectors[index])) {
				return true;
			}
		}
		return false;
	}

	private RoomControl RandomRoom() {
		return roomsDict.ElementAt(UnityEngine.Random.Range(0, roomsDict.Count)).Value;
	}

	// Vector point in a direction that is empty if you start from this room
	private Vector2 RandomEmpty(RoomControl room) {
		if (!IsNextToEmpty(room)) {
			throw new System.InvalidOperationException("Can not give empty room if not next to empty");
		}

		Vector2 vect;
		int d = 0;
		do {
			vect = RoomControl.vectors[UnityEngine.Random.Range(0, 4)];
			if (d > 1000)
			{
				throw new System.Exception("Infinite loop occurring.");
			}
			d++;
		} while (!IsEmpty(room.Index + vect));
		return vect;
	}

	// Returns: 0-3  signalling a direction which can give an empty room
	private Vector2 RandomNotEmpty(RoomControl room) {
		Vector2 vect;
		int d = 0;
		do {
			vect = RoomControl.vectors[UnityEngine.Random.Range(0, 4)];
			if (d > 1000)
			{
				throw new System.Exception("Infinite loop occurring.");
			}
			d++;
		} while (IsEmpty(room.Index + vect));
		return vect;
	}

	// Set up the bidirectional relationship between to rooms that will later ensure the doors are linked
	public void SetAdj(RoomControl room, Vector2 dir, RoomControl adjRoom) {

		room.SetAdj(dir, adjRoom);
		adjRoom.SetAdj(dir * -1, room);

		GameObject.Instantiate(trash, (room.transform.position + adjRoom.transform.position) / 2, Quaternion.identity);
	}
}
