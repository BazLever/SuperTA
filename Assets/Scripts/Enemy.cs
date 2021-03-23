using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    //------------------------------------------------------------------------------
    // Place this on an object to have it be destoryable by the player
    //------------------------------------------------------------------------------

    //Changeables
    public int health;
    int maxHealth;
    public int scoreOnDeath;

    //Drop on death
    public GameObject[] droppedOnDeath;
    public GameObject blood;
    public Transform enemyPos;

    public GameObject enemyMovement;

    GameObject gameManager;



    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        maxHealth = health;
    }


    void Update()
    {
        if (health <= 0)
        {
            Death();
        }


    }

    void Death()
    {
        //GameObject GO = Instantiate(droppedOnDeath, (new Vector3(enemyPos.position.x, (enemyPos.position.y - 1.5f), enemyPos.position.z)), Quaternion.identity) as GameObject;
        //Debug.Log("Item dropped at position" + enemyPos);
        hitByThing();
        GameObject GO = Instantiate(blood, (new Vector3(enemyPos.position.x, (enemyPos.position.y), enemyPos.position.z)), Quaternion.identity) as GameObject;
        gameManager.GetComponent<GameManager>().UpdateScore(scoreOnDeath);
        gameManager.GetComponent<GameManager>().enemiesToKill -= 1;
        Destroy(gameObject);
    }

    public void DeathByGun()
    {
        //GameObject GO = Instantiate(droppedOnDeath[0], (new Vector3(enemyPos.position.x, (enemyPos.position.y - 1.5f), enemyPos.position.z)), Quaternion.identity) as GameObject;
        //Debug.Log("Item dropped at position" + enemyPos);
        Death();
    }

    public void DeathBySword()
    {
        GameObject GO = Instantiate(droppedOnDeath[1], (new Vector3(enemyPos.position.x, (enemyPos.position.y - 1.5f), enemyPos.position.z)), Quaternion.identity) as GameObject;
        Debug.Log("Item dropped at position" + enemyPos);
        Death();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.GetComponent<GameManager>().FailState();
        }
    }

    public void hitByThing()
    {
        GameObject GO = Instantiate(blood, (new Vector3(enemyPos.position.x, (enemyPos.position.y), enemyPos.position.z)), Quaternion.identity) as GameObject;
    }
}
