using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks
{
    // Local rigidbody variable to hold a reference to the attached Rigidbody2D component
    private Rigidbody2D rb;
    private PhotonView _photonView;
    public float movementSpeed = 1000.0f;

    private Animator anim;
    //public Animator anim;
    private float MoveSpeed = 5f;
    [SerializeField] public int health = 2;

    public GameObject bomb;
    private Vector2 movement;

    private float timer;
    private bool timerBool = false;

    public bool isReady = false;


    void Awake()
    {
        // Setup Rigidbody for frictionless top down movement and dynamic collision
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();

        if (_photonView.IsMine)
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

        if (movement.y == 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        if (movement.x == 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }


        anim.SetFloat("horizontal", movement.x);
        anim.SetFloat("vertical", movement.y);


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

    public void Ready()
    {
        WaitingScene.instance.Invoke("GameWait",1);
        WaitingScene.instance.unready.SetActive(true);
        WaitingScene.instance.ready.SetActive(false);
        WaitingScene.instance._readyPlayerCount += 1;
        isReady = true;
        
    }

    public void Unready()
    {
        WaitingScene.instance.Invoke("GameWait",1);
        WaitingScene.instance.unready.SetActive(false);
        WaitingScene.instance.ready.SetActive(true);
        WaitingScene.instance._readyPlayerCount -= 1;
        isReady = false;
    }
}
