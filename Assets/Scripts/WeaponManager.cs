using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    //So far, this only works with two weapons. Don't add more lest you edit this script to allow it to be so.

    public GameObject[] weapons;
    public GameObject gunTagger;

    public int ammoCount;
    public int ammoCountMax;

    bool isSwordEquiped;
    public float swordActiveTime;

    float timer;

    //UI Elements

    private GameObject swordTimer;
    private GameObject swordBackground;

    public GameObject gunElements;
    public GameObject swordElements;

    Image crosshair;

    public AudioSource ammoGain;
    public AudioSource gunPickup;

    bool hasWeapon;

    GameObject playerCam;

    int weaponRange = 0;


    void Start()
    {
        playerCam = GameObject.Find("PlayerCam");

        //Weapons
        weapons[0].SetActive(false);
        weapons[1].SetActive(false);

        hasWeapon = false;

        //UI
        swordTimer = GameObject.Find("SwordTimer");
        swordBackground = GameObject.Find("SwordBG");
        swordBackground.GetComponent<RectTransform>().localScale = new Vector3(swordActiveTime, 1, 0);

        swordElements.gameObject.SetActive(false);
        ammoCountMax = weapons[0].GetComponent<GunKit>().maxAmmo;

        crosshair = GameObject.Find("Crosshair").GetComponent<Image>();
    }

    void Update()
    {

        ammoCount = weapons[0].GetComponent<GunKit>().ammo;

        //Debuggers
        if (Input.GetKeyDown(KeyCode.P))
        {
            weapons[1].SetActive(false);
            weapons[0].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
        }


        if (isSwordEquiped == true)
        {
            //timer += Time.deltaTime;
            //swordTimer.GetComponent<RectTransform>().localScale = new Vector3(timer, 1, 0);
        }

        if (timer >= swordActiveTime)
        {
            SwordDrop();
            timer = 0f;
        }

        if (hasWeapon == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (isSwordEquiped == true)
                {
                    weaponRange = 100;
                    CheckIfCanHit();
                    SwordDrop();
                }
                else if (isSwordEquiped == false)
                {
                    weaponRange = 5;
                    CheckIfCanHit();
                    SwordPickup();
                }
            }
        }


        

        

            Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * weaponRange);

        crosshair.color = new Color32(255, 255, 255, 100);
        CheckIfCanHit();

    }


    public void SwordPickup()
    {
        isSwordEquiped = true;
        timer = 0;
        weapons[0].SetActive(false);
        weapons[1].SetActive(true);
    }

    public void SwordDrop()
    {
        hasWeapon = true;
        isSwordEquiped = false;
        SwordUIDisappears();
        weapons[1].SetActive(false);
        weapons[0].SetActive(true);
    }

    public void AmmoPickup()
    {
        //weapons[0].gameObject.GetComponent<GunKit>().ammo = weapons[0].gameObject.GetComponent<GunKit>().maxAmmo;
        if (weapons[0].gameObject.GetComponent<GunKit>().ammo != weapons[0].gameObject.GetComponent<GunKit>().maxAmmo)
        {
            weapons[0].gameObject.GetComponent<GunKit>().ammo += 1;
            ammoGain.Play();
        }
        weapons[0].gameObject.GetComponent<GunKit>().UpdateAmmoText();
    }

    public void CrosshairAppears()
    {
        gunElements.gameObject.SetActive(true);
        gunPickup.Play();
    }

    public void SwordUIAppears()
    {
        swordElements.gameObject.SetActive(true);
    }

    public void SwordUIDisappears()
    {
        swordElements.gameObject.SetActive(false);
    }

    void CheckIfCanHit()
    {
        RaycastHit hit;
        float distanceOfRay = weaponRange;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, distanceOfRay))
        {
            if (hit.transform.tag != "Enemy")
            {
                crosshair.color = new Color32(255, 255, 255, 100);
            }
            else
            {
                crosshair.color = new Color32(255, 0, 0, 100);
            }
        }
    }
}
