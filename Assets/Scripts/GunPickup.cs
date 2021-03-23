using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<WeaponManager>().CrosshairAppears();
            other.gameObject.GetComponent<WeaponManager>().SwordDrop();
            gameManager.GetComponent<GameManager>().StartTimer();
            Destroy(gameObject);
        }
    }
}
