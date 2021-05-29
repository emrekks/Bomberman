using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    private Rigidbody2D rb;
    private PhotonView _photonView;
    public float movementSpeed = 1000.0f;

    //public Animator anim;
    private float MoveSpeed = 5f;
    [SerializeField] public int health = 2;

    public GameObject bomb;
    private Vector2 movement;

    private float timer;
    private bool timerBool = false;



    void Awake()
    {
        // Setup Rigidbody for frictionless top down movement and dynamic collision
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

      
        if (!_photonView.IsMine)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (Input.GetKeyDown(KeyCode.Space) && timerBool == false)
        {
            PlacingBomb();
            timerBool = true;
        }

        if (timerBool)
        {
            timer += Time.deltaTime;

            if (timer >= 3f)
            {
                timerBool = false;
                timer = 0f;
            }
        }



        //anim.SetFloat("Horizontal", movement.x);
        //anim.SetFloat("Vertical", movement.y);
        //anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * MoveSpeed * Time.fixedDeltaTime);
    }



    void PlacingBomb()
    {
        PhotonNetwork.Instantiate("Bomb", transform.position, transform.rotation);
    }
}
