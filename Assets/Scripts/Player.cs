using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float Speed;
    private Rigidbody rb;
    public Transform player_p;
    public float miny = 0f; // Minimum X position
    public float maxy = 4f; // Maximum X position
    public float forwardSpeed = 5f; // Forward speed of the ball
    public float acceleration = 1f; // Rate of acceleration
    public static float sidewaysSpeed = 10f; // Speed for moving left and right

    private Button moveRightButton; // Reference to the UI button for moving right
    private Button moveLeftButton; // Reference to the UI button for moving left

    // Start is called before the first frame update

    public void Awake()
    {
        sidewaysSpeed = PlayerPrefs.GetFloat("Sensitivity", 1);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        FindUIButtons();
    }

    private void FindUIButtons()
    {
        // Find UI buttons dynamically
        GameObject canvas = GameObject.Find("Canvas"); // Assuming the UI buttons are under a GameObject named "Canvas"
        if (canvas != null)
        {
            moveRightButton = canvas.transform.Find("MoveRightButton").GetComponent<Button>();
            moveLeftButton = canvas.transform.Find("MoveLeftButton").GetComponent<Button>();

            // Add listeners to the UI buttons
            moveRightButton.onClick.AddListener(MoveRight);
            moveLeftButton.onClick.AddListener(MoveLeft);
        }
        else
        {
            Debug.LogError("Canvas not found! Make sure the UI buttons are under a GameObject named 'Canvas'.");
        }
    }

    // FixedUpdate is called 0.2 frame default
    private void FixedUpdate()
    {
        forwardAcceleration();
    }

    // Update is called once per frame
    private void Update()
    {
        //horizontal_movement();

        Speed = rb.velocity.z;

        if (this.transform.position.z > 30f)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
    }

    // Accelerate the ball forward
    private void forwardAcceleration()
    {
        rb.velocity += Vector3.forward * acceleration;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Blocker" || collision.gameObject.tag == "Blocker2")
        {
            GameManager.GM.Game_Over();
            FindObjectOfType<AudioManager>().Stop_Sound("BG");
            FindObjectOfType<AudioManager>().Play_Sound("GameOver");
            StartCoroutine(PauseTime());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player.
        if (other.CompareTag("Diamondo5side"))
        {
            GameManager.GM.Score(10);
            FindObjectOfType<AudioManager>().Play_Sound("Coin_Collected");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("CubieBeveled"))
        {
            GameManager.GM.Score(50);
            FindObjectOfType<AudioManager>().Play_Sound("Coin_Collected");
            Destroy(other.gameObject);
        }
    }

    public static void SidewaysSpeed(float s)
    {
        sidewaysSpeed = s;
        PlayerPrefs.SetFloat("Sensitivity", s);
    }

    private void MoveRight()
    {
        rb.velocity = new Vector3(sidewaysSpeed, rb.velocity.y, rb.velocity.z);
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector3(-sidewaysSpeed, rb.velocity.y, rb.velocity.z);
    }

    private IEnumerator PauseTime()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
        Destroy(this.gameObject);
    }
}