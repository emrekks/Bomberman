using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private float radius = 2f;

    private float timer;

    public GameObject[] players;
    public GameObject[] Breakablewall;
    private PlayerController pc_;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Breakablewall = GameObject.FindGameObjectsWithTag("BreakableWall");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 3f)
        {
            for (int i = 0; i < players.Length; i++)
            {

                if (Mathf.Abs(Vector3.Distance(transform.position, players[i].transform.position)) <= radius)
                {
                    pc_ = players[i].GetComponent<PlayerController>();
                    if (pc_.health >= 1)
                    {
                        pc_.health = -1;
                    }
                    else if (pc_.health <= 0)
                    {
                        players[i].SetActive(false);
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
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
