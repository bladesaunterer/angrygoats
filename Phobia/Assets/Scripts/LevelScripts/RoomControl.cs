using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using System.Linq;

public class RoomControl : MonoBehaviour {

    public GameObject minimapUI;

	private const int HORIZ_TILING = 100;
	private const int VERT_TILING = 80;
	
	public static Dictionary<Vector2, string> directions = new Dictionary<Vector2, string> {
		{new Vector2(0, 1), "North Door"},
		{new Vector2(1, 0), "East Door"},
		{new Vector2(0, -1), "South Door"},
		{new Vector2(-1, 0), "West Door"}
	};

	public static Vector2[] vectors = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0)};
	
	public static Vector3 IndexToPosition(Vector2 index) {
		return new Vector3(index.x*HORIZ_TILING, 0f, index.y*VERT_TILING);
	}
	
	
	public Vector2 Index { get; set; }
	private GameObject floorSecret;
	public Dictionary<Vector2, RoomControl> adjRoomsDict = new Dictionary<Vector2, RoomControl>();
	
	public bool spawnWebs = true;
	public bool spawnEnemies = true;
	
	public List<GameObject> freeCells = new List<GameObject>();
	public List<GameObject> enemies = new List<GameObject>();
	
	
	public GameObject Floor {
		get {
			return floorSecret;
		}
		set {
			if (floorSecret != null) {
				GameObject.Destroy(floorSecret);
			}
			floorSecret = (GameObject)GameObject.Instantiate(value, transform.position, Quaternion.identity);
			floorSecret.transform.parent = this.transform;
		}
	}
	
	
	public void SetAdj(Vector2 dir, RoomControl adjRoom) {
		adjRoomsDict[dir] = adjRoom;
		
		GameObject thisDoor = transform.Find("Doors/" + directions[dir]).gameObject;
		GameObject adjDoor = adjRoomsDict[dir].transform.Find("Doors/" + directions[dir*-1]).gameObject;

		thisDoor.GetComponent<DoorControl>().goalDoor = adjDoor; // link the goal
		transform.Find("Door Blockers/" + directions[dir]).gameObject.SetActive(false); // disable the blocker
	}
	
	public void AddGraph() {
		AstarData data = AstarPath.active.astarData;
		// This creates a Grid Graph
		GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;
		// Setup a grid graph with some values
		gg.width = 50;
		gg.depth = 40;
		gg.center = transform.position + new Vector3(0, -5, 0);
		// Updates internal size from the above values
		gg.UpdateSizeFromWidthDepth();
		
		GraphCollision gc = new GraphCollision();
		gc.collisionCheck = true;
		gc.heightCheck = true;
		gc.diameter = 2F;
		gc.heightMask = LayerMask.GetMask(new string[] {"Rock","Room","Floor"});
		gc.mask = LayerMask.GetMask(new string[] {"Rock","Room"});
		gg.maxClimb = 8F;
		gg.collision = gc;
	}
	
	public void PopulateCells() {
		foreach (Transform child in Floor.transform) {
			if (child.gameObject.layer == LayerMask.NameToLayer("Floor")) {
				freeCells.Add(child.gameObject);
			}
		}
	}
	
	public void AddEnemy(GameObject enemy) {
		
		GameObject chosenCell = freeCells[Random.Range(0, freeCells.Count)];
		freeCells.Remove(chosenCell);
		

		GameObject thisEnemy = (GameObject)GameObject.Instantiate(enemy, (chosenCell.transform.position + new Vector3(0, 2, 0)), Quaternion.identity);
		enemies.Add(thisEnemy);
		thisEnemy.GetComponent<EnemyControl>().home = chosenCell.transform;
		thisEnemy.GetComponent<AIPath>().target = chosenCell.transform;

	}
	
	public void AddWebs(GameObject web, int amount) {
		
		for (int i = 0; i < amount; i++) {
			
			GameObject chosenCell = freeCells[Random.Range(0, freeCells.Count)];
			freeCells.Remove(chosenCell);

			GameObject thisWeb = (GameObject)GameObject.Instantiate(web, (chosenCell.transform.position + new Vector3(0, 1.1f, 0)), Quaternion.identity);
		}
	}
	
	
	public void EnemiesGoHome() {
		
		foreach (GameObject enemy in enemies) {
			if (enemy != null) {
				enemy.GetComponent<AIPath>().target = enemy.GetComponent<EnemyControl>().home;
				if (enemy.GetComponent<EnemySingleShot>() != null) {
					enemy.GetComponent<EnemySingleShot>().shouldShoot = false;
				}
			}
		}

	}
	
	public void EnemiesHuntPlayer() {
		
		foreach (GameObject enemy in enemies) {
			if (enemy != null) {
				enemy.GetComponent<AIPath>().target = GameObject.FindWithTag("Player").transform;
				if (enemy.GetComponent<EnemySingleShot>() != null) {
					enemy.GetComponent<EnemySingleShot>().shouldShoot = true;
				}
			}
		}

	}

    public void UpdateMinimap()
    {
        // Triggers minimap update
        minimapUI.GetComponent<MinimapScript>().PlayerEntersRoom(Index, adjRoomsDict.Keys.ToList());
    }
}