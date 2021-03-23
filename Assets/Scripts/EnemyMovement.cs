using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent enemy;

    public Animator knightAnim;

    bool isChasingPlayer;


    void Start()
    {
        //enemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        


        //enemy.SetDestination(player.transform.position);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.SetDestination(player.transform.position);
            knightAnim.SetBool("isChasing", true);
        }
    }
}
