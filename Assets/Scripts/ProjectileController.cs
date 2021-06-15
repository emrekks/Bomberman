using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private float radius = 1.5f;

    private float timer;

    public GameObject[] players;
    public GameObject[] Breakablewall;
    private PlayerController pc_;
    private Animator anim;
    private bool once = true;
    private bool playerDead = false;
    private BoxCollider2D collider2d;
    private AudioSource ads;
    private bool boom = false;


    // Start is called before the first frame update
    void Start()
    {
        ads = GameObject.FindGameObjectWithTag("BombSound").GetComponent<AudioSource>();
        collider2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        players = GameObject.FindGameObjectsWithTag("Player");
        Breakablewall = GameObject.FindGameObjectsWithTag("BreakableWall");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 0.5f)
        {
            collider2d.isTrigger = false;
        }

        if (timer >= 1.5f && timer <=2)
        {
            for (int i = 0; i < players.Length; i++)
            {

                if (Mathf.Abs(Vector3.Distance(transform.position, players[i].transform.position)) <= radius)
                {
                    
                    pc_ = players[i].GetComponent<PlayerController>();

                    if (pc_.health >= 1 && once)
                    {
                        once = false;
                        pc_.health--;
                    }

                    else if (pc_.health <= 0 && !playerDead)

                    {
                        players[i].SetActive(false);

                        PhotonRoom.room.deadPlayer--;
                        PhotonRoom.room.WinnerScene();
                        playerDead = true;
                    }
                }
            }



            for (int i = 0; i < Breakablewall.Length; i++)
            {
                if (Mathf.Abs(Vector3.Distance(transform.position, Breakablewall[i].transform.position)) <= radius)
                {
                    Breakablewall[i].SetActive(false);
                }
            }
            anim.SetBool("Explonation", true);
            if (!boom)
            {
                ads.Play();
                boom = true;
            }


            StartCoroutine(Explosion(gameObject));
        }
    }

    private IEnumerator Explosion(GameObject a)
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Explonation", false);
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
