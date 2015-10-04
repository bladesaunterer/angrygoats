using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerControl : MonoBehaviour
{
	private const float DOOR_JUMP = 2;

	public float speed = 6f;            // The speed that the player will move at.
	public float webSlowFactor = 0.5f;
	private bool inWeb = false;

	public float fireRate;
	public GameObject shot;				// The special attack
	public Transform shotSpawn;			// Where the special attack will spwan 

	public Slider cooldownSlider;		// UI slider that represents cooldown
	public float cooldown;				// How much cool down
	public float regen;					// Regen of the cooldown per second 
	public float cost;					// How much each special attack costs

	public Slider healthSlider;			// UI slider that represents the health
	public float health;				// Health of the player
			
	public GameObject meleeAttack;
	public Transform meleeSpawn;
	public float meleeRate;				
	private float nextMelee;

	private float nextTime;

    private Vector3 movement;                   // The vector to store the direction of the player's movement.
    private Vector3 cameraPosition = new Vector3 (0, 30, -17);
    // private Vector3 cameraPosition = new Vector3(0, 30, -17);

    private int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    private float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    private Transform mainCameraTransform;

    void Awake ()
	{
        this.mainCameraTransform = Camera.main.GetComponentInParent<Transform>();
    }

	void Update(){
		//Updates every second
		if (Time.time > nextTime) {
			nextTime = Time.time + 1f;
			if (cooldown != 100f ){
				cooldown += regen;
			}
			//UpdateCoolDownSlider();
		}
		if (Input.GetKeyDown(KeyCode.K) && cooldown >= cost)
		{
			cooldown -= cost;
			//UpdateCoolDownSlider();
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}else if( Input.GetKeyDown(KeyCode.J) && Time.time > nextMelee)
		{
			nextMelee = Time.time + meleeRate;
			Instantiate(meleeAttack, meleeSpawn.position, meleeSpawn.rotation);
		}
	}

    void FixedUpdate()
    {
        if (IsMine())
        {
            // Store the input axes.
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            floorMask = LayerMask.GetMask("Floor");

            // Move the player around the scene.
            Move(h, v);

        }
	}

	//This will also attempt to rotate the player 
	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set (h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.

		float moveSpeed = speed;
		if (inWeb) {
			moveSpeed*=webSlowFactor;
		}
		movement = movement.normalized * moveSpeed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
		GetComponent<Rigidbody>().MovePosition (transform.position + movement);

		//Rotate the player
		if (h != 0f || v != 0f) {
			Rotate (h, v);
		}
	}

	//This method rotates player based on keyboard input
	void Rotate(float h, float v){
		Vector3 targetDirection = new Vector3 (h, 0f, v);
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (GetComponent<Rigidbody>().rotation, targetRotation, 15f * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation (newRotation);
	}

	//This method rotates the player based on 
	//Dean says to not delete this method. Not used, but is useful
    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            GetComponent<Rigidbody>().MoveRotation(newRotatation);
        }
    }
	
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Door"))
		{
            DoorControl doorMono = other.gameObject.GetComponent<DoorControl>();
            Debug.Log("Door fired: " + doorMono.gameObject.GetInstanceID());
            Vector3 goalPos = doorMono.goalDoor.transform.position + doorMono.goalDoor.transform.forward * DOOR_JUMP;
            goalPos.y = 0;
            this.transform.position = goalPos;
            if (IsLocalPlayer()) {
                GetComponent<NetworkTransform>().InvokeSyncEvent(0, null);
            }
			doorMono.goalDoor.GetComponent<DoorControl>().ownRoom.transform.Find("Lights").gameObject.SetActive(true);
            if (IsMine()) {
                mainCameraTransform.position = (doorMono.goalDoor.GetComponent<DoorControl>().ownRoom.transform.position) + cameraPosition;
			}
			doorMono.ownRoom.transform.Find("Lights").gameObject.SetActive(false);
		}
		else if (other.gameObject.CompareTag("Web"))
		{
			inWeb = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag ("Web"))
        {
			inWeb = false;
		}

        //if (IsMine()) {
        //        mainCameraTransform.position = (doorMono.goalRoom.transform.position) + cameraPosition;
        //}

	}
	
	void UpdateCoolDownSlider(){
		cooldownSlider.value = cooldown;
	}
	
	public void TakeDamage (float damage){
		health = + damage;
		//healthSlider.value = health;
	}

    public bool IsMine()
    {
        return GetComponentInParent<NetworkIdentity>() == null || this.GetComponentInParent<NetworkIdentity>().isLocalPlayer;
    }

    public bool IsLocalPlayer()
    {
        return GetComponentInParent<NetworkIdentity>() != null && this.GetComponentInParent<NetworkIdentity>().isLocalPlayer;
    }
}