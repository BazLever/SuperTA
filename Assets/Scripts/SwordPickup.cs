using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //other.gameObject.GetComponent<WeaponManager>().weapons[1].SetActive(true);
            //other.gameObject.GetComponent<WeaponManager>().weapons[0].SetActive(false);

            //other.gameObject.GetComponent<WeaponManager>().SwordUIAppears();
            other.gameObject.GetComponent<WeaponManager>().SwordPickup();
            Destroy(gameObject);
        }
    }
}
