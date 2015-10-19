using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using System.Linq;

/// <summary>
/// Purpose: This controls each room and it's interactions.<para/>
/// </summary>
public class RoomControl : MonoBehaviour {
    
	public GameObject minimapUI;

	// real distance between the centres of rooms
	private const int HORIZ_TILING = 100;
	private const int VERT_TILING = 80;
	
	// Four vectors for for direction strings that allow you to find the door going in that direction
	public static Dictionary<Vector2, string> directions = new Dictionary<Vector2, string> {
		{new Vector2(0, 1), "North Door"},
		{new Vector2(1, 0), "East Door"},
		{new Vector2(0, -1), "South Door"},
		{new Vector2(-1, 0), "West Door"}
	};

	// Four directions in case you want to iterate over them
	public static Vector2[] vectors = { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0)};
	
	// The real position of a room for a given index
	public static Vector3 IndexToPosition(Vector2 index) {
		return new Vector3(index.x*HORIZ_TILING, 0f, index.y*VERT_TILING);
	}
	
	
	public Vector2 Index { get; set; }
	public Dictionary<Vector2, RoomControl> adjRoomsDict = new Dictionary<Vector2, RoomControl>(); // The rooms which you can get to directly from this one
	
	public bool spawnWebs = true;
	public bool spawnEnemies = true;
	
	public List<GameObject> freeCells = new List<GameObject>(); // cubes without enemies on them
	public List<GameObject> enemies = new List<GameObject>();
	
	// Floor sets floorSecret but also instantiates it
	private GameObject floorSecret;
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
	
	// Link the relevant door from this room to the other given room
	public void SetAdj(Vector2 dir, RoomControl adjRoom) {
		adjRoomsDict[dir] = adjRoom;
		
		GameObject thisDoor = transform.Find("Doors/" + directions[dir]).gameObject;
		GameObject adjDoor = adjRoomsDict[dir].transform.Find("Doors/" + directions[dir*-1]).gameObject;

		thisDoor.GetComponent<DoorControl>().goalDoor = adjDoor; // link the goal
		transform.Find("Door Blockers/" + directions[dir]).gameObject.SetActive(false); // disable the blocker
	}
	
	// sets up pathfinding
	// To get walking/flying working with dynamic room generation, some complexity was required
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
	
	// instantiate a given enemy at a random location in a room
	public GameObject AddEnemy(GameObject enemy) {
		GameObject chosenCell = freeCells[Random.Range(0, freeCells.Count)];
		freeCells.Remove(chosenCell);
		
		
		GameObject thisEnemy = (GameObject)GameObject.Instantiate(enemy, (chosenCell.transform.position + new Vector3(0, 2, 0)), Quaternion.identity);
		enemies.Add(thisEnemy);
		
		thisEnemy.GetComponent<EnemyControl>().home = chosenCell.transform;
		thisEnemy.GetComponent<AIPath>().target = chosenCell.transform;
		return thisEnemy;
	}

	// Add enemy and make them stronger by a certain amount
	public GameObject AddEnemyWithRamp(GameObject enemy, double ramp) {
		double temp;
		GameObject chosenCell = freeCells[Random.Range(0, freeCells.Count)];
		freeCells.Remove(chosenCell);
		
		
		GameObject thisEnemy = (GameObject)GameObject.Instantiate(enemy, (chosenCell.transform.position + new Vector3(0, 2, 0)), Quaternion.identity);
		enemies.Add(thisEnemy);
		
		thisEnemy.GetComponent<EnemyControl>().home = chosenCell.transform;
		thisEnemy.GetComponent<AIPath>().target = chosenCell.transform;
		
		temp = (double)thisEnemy.GetComponent<EnemyHealth> ().currentHealth * ramp;
		thisEnemy.GetComponent<EnemyHealth> ().currentHealth = (int)temp;
		
		temp = (double)thisEnemy.GetComponent<EnemyHealth> ().startingHealth * ramp;
		thisEnemy.GetComponent<EnemyHealth> ().startingHealth = (int)temp;
		
		temp = (double)thisEnemy.GetComponent<EnemyAttack> ().damage * ramp;
		thisEnemy.GetComponent<EnemyAttack> ().damage = (int)temp;
		
		return thisEnemy;

	}
	
	public void AddBoss(GameObject boss){
		enemies.Add (boss);
	}
	
	public void AddWebs(GameObject web, int amount) {
		
		for (int i = 0; i < amount; i++) {
			
			GameObject chosenCell = freeCells[Random.Range(0, freeCells.Count)];
			freeCells.Remove(chosenCell);

			GameObject.Instantiate(web, (chosenCell.transform.position + new Vector3(0, 1.1f, 0)), Quaternion.identity);
		}
	}
	
	// When player leaves room or goes invisible, enemies stop chasing and shooting
	public void EnemiesGoHome() {
		
		foreach (GameObject enemy in enemies) {
			if (enemy != null) {
				if(enemy.GetComponent<AIPath>() != null) {	
					enemy.GetComponent<AIPath>().target = enemy.GetComponent<EnemyControl> ().home;
				}
				if (enemy.GetComponent<EnemySingleShot>() != null) {
					enemy.GetComponent<EnemySingleShot>().shouldShoot = false;
				}
				if (enemy.GetComponent<EnemyPewPew>() != null) {
					enemy.GetComponent<EnemyPewPew>().shoot = true;
				}
				if (enemy.GetComponent<EnemyCharge>() != null) {
					enemy.GetComponent<EnemyCharge>().enabled = false;
				}
				if (enemy.CompareTag("Boss")) {
					enemy.GetComponent<EnemyHealth>().currentHealth = enemy.GetComponent<EnemyHealth> ().startingHealth;
				}
			}
		}
	}
	
	// When player enters room or goes visible, enemies stop chasing and shooting
	public void EnemiesHuntPlayer() {
		
		foreach (GameObject enemy in enemies) {
			if (enemy != null) {
				if(enemy.GetComponent<AIPath>() != null) {	
					enemy.GetComponent<AIPath>().target = GameObject.FindWithTag("Player").transform;
				}
				if (enemy.GetComponent<EnemySingleShot>() != null) {
					enemy.GetComponent<EnemySingleShot>().shouldShoot = true;
				}
                if (enemy.GetComponent<EnemyPewPew>() != null) {
                    enemy.GetComponent<EnemyPewPew>().shoot = true;
                }
				if (enemy.GetComponent<EnemyCharge>() != null) {
					enemy.GetComponent<EnemyCharge>().enabled = true;
					enemy.GetComponent<EnemyCharge>().StartCharge();
				}
			}
		}

	}

	// Triggers minimap update
	public void UpdateMinimap() {
		minimapUI.GetComponent<MinimapScript>().PlayerEntersRoom(Index, adjRoomsDict.Keys.ToList());
	}
}