using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Purpose: The main controlling class for the player.<para/>
/// Authors: Chester Booker, Dean Ulbrick       <para/>
/// This class handles the players melee attacks, movements, animations and allows the camera to always be over the current room of the player.
/// </summary>
public class PlayerControl : MonoBehaviour
{
    Animator anim;

    private const float DOOR_JUMP = 2;

    public float speed = 6f;            // Player movement speed.

    public float webSlowFactor = 0.5f;
    private int inWeb = 0;

    public GameObject shot;             // The special attack object.
    public Transform shotSpawn;         // Location where the special attack will spawn. 

    public Slider cooldownSlider;       // UI slider that represents cooldown.
    public int cooldown;              // Initial cooldown value.
    public int regen;                 // Regen of the cooldown per second. 

    public GameObject meleeAttack;
    public Transform meleeSpawn;
    public float meleeRate;
    private float nextMelee;

    private float nextTime;

    private Vector3 movement;                   // The vector to store the direction of the player's movement.
    private Vector3 cameraPosition = new Vector3(0, 30, -17);

    private int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    private float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    private Transform mainCameraTransform;
    public GameObject currentRoom;

    void Start()
    {
        // Enables animations
        anim = GetComponent<Animator>();
    }

    void Awake()
    {
        // Enables the camera to track where the character is
        this.mainCameraTransform = Camera.main.GetComponentInParent<Transform>();
    }

    void Update()
    {
        // Regenerate the energy/mana bar after a specified amount of time
        if (Time.time > nextTime)
        {
            nextTime = Time.time + 1f;
            if (cooldown != 100f)
            {
                cooldown += regen;
            }
            UpdateCoolDownSlider();
        }

        // Perform a basic melee attack
        if (Input.GetKeyDown(KeyCode.J) && Time.time > nextMelee)
        {
            // Playsound, animate and attack
            PlayerSfxScript.playMeleeSound();
            anim.SetTrigger("Melee");
            // Pauses to sync up more with the animation
            Invoke("AttactInstantiate", 0.15f);
            nextMelee = Time.time + meleeRate;
        }
    }

    // Instantiate the basic melee collision object
    void AttactInstantiate()
    {
        Instantiate(meleeAttack, meleeSpawn.position, meleeSpawn.rotation);
    }

    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        floorMask = LayerMask.GetMask("Floor");

        // Move the player around the scene.
        Move(h, v);
        // Animate the player based on movement
        Animating(h, v);
    }

    // This will also attempt to rotate the player 
    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Reduce the players movement speed basic on if they are in a web
        if (inWeb > 0)
        {
            movement *= webSlowFactor;
        }

        // Move the player to it's current position plus the movement.
        GetComponent<Rigidbody>().MovePosition(transform.position + movement);

        // Rotate the player
        if (h != 0f || v != 0f)
        {
            Rotate(h, v);
        }
    }

    // This method rotates player based on keyboard input
    void Rotate(float h, float v)
    {
        Vector3 targetDirection = new Vector3(h, 0f, v);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, 15f * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }

    void OnTriggerEnter(Collider other)
    {
        // When walking through a door
        if (other.gameObject.CompareTag("Door"))
        {
            DoorControl doorMono = other.gameObject.GetComponent<DoorControl>();

            Vector3 goalPos = doorMono.goalDoor.transform.position + doorMono.goalDoor.transform.forward * DOOR_JUMP;
            goalPos.y = 0;

            doorMono.goalDoor.GetComponent<DoorControl>().EnterRoom();

            // Move the character to the room linked to the door they went through
            this.transform.position = goalPos;

            // Make the camera follow the characters position
            mainCameraTransform.position = (doorMono.goalDoor.GetComponent<DoorControl>().ownRoom.transform.position) + cameraPosition;

            // Updates the room references to the new room
            doorMono.ExitRoom();
            currentRoom = doorMono.goalDoor.GetComponent<DoorControl>().ownRoom;
			AchievementManager.Instance.enterRoom(currentRoom);
        }
        // Tracks webs the player is currently stepping on
        else if (other.gameObject.CompareTag("Web"))
        {
            inWeb++;
        }
    }

    // Remove web tracking when stepping off a web
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Web"))
        {
            if (inWeb > 0)
            {
                inWeb--;
            }
        }
    }

    void UpdateCoolDownSlider()
    {
        // Update cooldown slider with current cooldown value.
        cooldownSlider.value = cooldown;
    }

    public void SubtractCooldown(int value)
    {
        cooldown -= value;
        UpdateCoolDownSlider();
    }

    // Cause the player to idle animate if doing nothing, walk animate if in a web or run animate for other circumstances
    void Animating(float h, float v)
    {
        bool moving = h != 0f || v != 0f;
        anim.SetBool("Run", moving && inWeb == 0);
        anim.SetBool("Walk", moving && inWeb > 0);
    }

    // Triggers the other animations of the player (die, cast, hit..)
    public void InitiateAnimation(string animationName)
    {
        if (animationName == "Die")
        {
            anim.SetBool(animationName, true);
            anim.SetTrigger("DieTrig");
        }
        else
        {
            anim.SetTrigger(animationName);
        }
    }

    /*
    // CURRENTLY NOT USED BY IS KEEP FOR FUTURE USE
    // This method rotates the player based on 
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
    */

}