using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    private float radius = 2f;

    private float timer;

    public GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
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
                    players[i].SetActive(false);
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
