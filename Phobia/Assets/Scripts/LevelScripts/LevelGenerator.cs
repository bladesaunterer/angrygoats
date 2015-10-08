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
    public GameObject roomPrefab;
	public GameObject startPrefab;
	public List<GameObject> floorPrefabs;
	
    public int totalEnemies = 60;
	public int maxEnemiesPerRoom = 6;
	public GameObject enemy;
    public GameObject bossFloor;
    public GameObject boss;
	
	
	public int minWebs;
	public int maxWebs;
	public GameObject web;
	
    public GameObject trash;

	Dictionary<Vector2,RoomControl> roomsDict = new Dictionary<Vector2,RoomControl>();
    
    void Start () {
		
		// These variables will change their meaning over the course of the method
		RoomControl thisRoom;
		RoomControl adjRoom;
		RoomControl chosenRoom;
		Vector2 roomVector;
		Vector2 roomVectorRel;
		
		
		// Start room
		
		thisRoom = ((GameObject)Instantiate(roomPrefab, new Vector3(0, 0, 0), Quaternion.identity)).GetComponent<RoomControl>();
		thisRoom.Floor = startPrefab; // pick random floor
		
		thisRoom.Index = new Vector2(0,0);
		
		roomsDict.Add(thisRoom.Index,thisRoom);
		
		thisRoom.gameObject.name = "Room " + 0;
		thisRoom.spawnEnemies = false;
		thisRoom.spawnWebs = false;

		thisRoom.gameObject.transform.Find("Lights").gameObject.SetActive(true);
		
		
		// normal rooms
		
        for (int i = 1; i < roomsToSpawn-1; i++) {
			
			int a = 0;
			do {
				adjRoom = RandomRoom();
				if (a > 1000) {
					throw new System.Exception();
				}
				a++;
			} while (!IsNextToEmpty(adjRoom));
			
			roomVectorRel = RandomEmpty(adjRoom);
			roomVector = adjRoom.Index + roomVectorRel;
			
			thisRoom = ((GameObject)Instantiate(roomPrefab, RoomControl.IndexToPosition(roomVector), Quaternion.identity)).GetComponent<RoomControl>();
			thisRoom.Floor = floorPrefabs[Random.Range(0, floorPrefabs.Count)];
			thisRoom.Index = roomVector;
			thisRoom.gameObject.name = "Room " + i;

			SetAdj(adjRoom, roomVectorRel, thisRoom);

			roomsDict.Add(thisRoom.Index,thisRoom);
        }
		
		// boss room
		
		int b = 0;
		do {
			adjRoom = RandomRoom();
			if (b > 1000) {
				throw new System.Exception();
			}
			b++;
		} while (!IsNextToEmpty(adjRoom));
		
		roomVectorRel = RandomEmpty(adjRoom);
		roomVector = adjRoom.Index + roomVectorRel;
			
		thisRoom = ((GameObject)Instantiate(roomPrefab, RoomControl.IndexToPosition(roomVector), Quaternion.identity)).GetComponent<RoomControl>();
		thisRoom.Floor = bossFloor;
		thisRoom.Index = roomVector;
		thisRoom.gameObject.name = "Boss Room";
		
		SetAdj(adjRoom, roomVectorRel, thisRoom);

		roomsDict.Add(thisRoom.Index,thisRoom);

		boss = (GameObject)Instantiate(boss, thisRoom.transform.position + new Vector3(0, 4, 0), Quaternion.identity);
		boss.GetComponent<AIPath>().target = GameObject.FindWithTag("Player").transform;
		
		
		// link rooms
		
		for (int i = 0; i < roomsToSpawn*3; i++) {
			chosenRoom = RandomRoom();
			
			Vector2 chosen = RandomNotEmpty(chosenRoom);
			if (!chosenRoom.adjRoomsDict.ContainsKey(chosen)) {
				i+=9;
			}
			SetAdj(chosenRoom, chosen, get(chosenRoom.Index + chosen));
		}
		
		// add pathfinding graph and webs
        foreach(RoomControl room in roomsDict.Values) {
			room.PopulateCells();
			room.AddGraph();
			if (room.spawnWebs) {
				room.AddWebs(web, Random.Range(minWebs, maxWebs));
			}
        }

        AstarPath.active.Scan();
		
		// populate rooms
		
		for (int i = 0; i < totalEnemies; i++) {
			
			int c = 0;
			do {
				chosenRoom = RandomRoom();
				if (c > 1000) {
					throw new System.Exception();
				}
				c++;
			} while (chosenRoom.enemies.Count >= maxEnemiesPerRoom || chosenRoom.spawnEnemies == false);
			
			chosenRoom.AddEnemy(enemy);
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
    private RoomControl get(Vector2 pos) {
		RoomControl output = null;
		roomsDict.TryGetValue(pos, out output);
		return output;
    }

	// does this room have an adjacent place for a room that is not occupied by a room already
	private bool IsNextToEmpty(RoomControl room) {
		for (int index = 0; index < 4; index++) {
			if (IsEmpty(room.Index+RoomControl.vectors[index])) {
				return true;
			}
		}
		return false;
	}
	
	private RoomControl RandomRoom() {
		return roomsDict.ElementAt(Random.Range(0, roomsDict.Count)).Value;
	}

	// returns: 0-3  signalling a direction which can give an empty room
	//          -1   if no such direction exists
	private Vector2 RandomEmpty(RoomControl room) {
		if (!IsNextToEmpty(room)) {
			throw new System.InvalidOperationException("Can not give empty room if not next to empty");
		}

		Vector2 vect;
		int d = 0;
		do {
			vect = RoomControl.vectors[Random.Range(0, 4)];
			if (d > 1000) {
				throw new System.Exception();
			}
			d++;
		} while (!IsEmpty(room.Index+vect));
		return vect;
	}

	// returns: 0-3  signalling a direction which can give an empty room
	private Vector2 RandomNotEmpty(RoomControl room) {
		Vector2 vect;
		int d = 0;
		do {
			vect = RoomControl.vectors[Random.Range(0, 4)];
			if (d > 1000) {
				throw new System.Exception();
			}
			d++;
		} while (IsEmpty(room.Index+vect));
		return vect;
	}

	// Set up the bidirectional relationship between to rooms that will later ensure the doors are linked
	public void SetAdj(RoomControl room, Vector2 dir, RoomControl adjRoom) {
		room.SetAdj(dir, adjRoom);
		adjRoom.SetAdj(dir*-1, room);
		
		GameObject.Instantiate(trash, (room.transform.position + adjRoom.transform.position)/2 , Quaternion.identity);
	}
}
