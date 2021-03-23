using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunKit : MonoBehaviour
{
    //Changeables
    public int gunDamage;
    public int maxAmmo;
    public float fireRate;
    public bool canReload;
    public float reloadSpeed;
    public int fireMode;
    public int weaponRange;
    public Camera playerCam;
    //Fire modes are as follows:
    // 0 - Full Auto
    // 1 - Semi Auto
    public bool isShotgun;

    public int ammo;
    float fireRateTimer;
    float reloadTimer;

    bool canFire;
    bool reloading;


    public Animator gunAnim;

    //UI Variables
    //Text ammoText;

    public Animator[] bulletUI;
    public GameObject noAmmo;


    //Sound stuff
    public AudioSource gunshotSound;

    public CameraShake cameraShake;

    public GameManager gameManager;



    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canFire = false;
        fireRateTimer = fireRate;

        //ammo = maxAmmo;

        //ammoText = GameObject.Find("AmmoCounter").GetComponent<Text>();
        UpdateAmmoText();
    }


    void Update()
    {
        //Shooting Code
        {
            if (canFire == false)
            {
                //gunAnim.SetBool("isShooting", false);
                fireRateTimer += Time.deltaTime;
                if (fireRateTimer > fireRate)
                {
                    gunAnim.SetBool("isShooting", false);
                    canFire = true;
                    
                }
            }


            if (fireMode == 0)
            {
                if (Input.GetMouseButton(0))
                {
                    //gunAnim.SetBool("isShooting", true);
                    Shoot();
                }
            }
            else if (fireMode == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //gunAnim.SetBool("isShooting", true);
                    Shoot();
                }
            }
        }

        //Reload Code
        {
            if (canReload == true)
            { 

                if (ammo != maxAmmo)
                {
                    if (Input.GetKeyDown(KeyCode.R) && reloading == false)
                    {
                        //gunAnim.SetBool("isShooting", false);
                        Debug.Log("Reload Begun");
                        reloading = true;
                        //ammoText.text = "..." + "/" + maxAmmo;
                        //gunAnim.SetBool("isReloading", true);
                    }
                }

                if (reloading == true)
                {
                    Reload();
                }
            }
        }

        //Animation Code
        /*
        {
            if (ammo <= 0)
            {
                gunAnim.SetBool("isShooting", false);
            }

            if (Input.GetMouseButton(1) && reloading == false)
            {
                if (Input.GetMouseButton(0) && ammo != 0)
                {
                    gunAnim.SetBool("isShooting", true);
                }
                gunAnim.SetBool("isAiming", true);
            }
            if (Input.GetMouseButtonUp(1))
            {
                gunAnim.SetBool("isAiming", false);
            }


            if (Input.GetKey(KeyCode.LeftShift))
            {

                gunAnim.SetBool("isSprinting", true);
            } 
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                gunAnim.SetBool("isSprinting", false);
            }

        }
        */

        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * weaponRange);

        //Ho boy, here we go again
        if (ammo == 0)
        {
            bulletUI[0].SetBool("isSpent", true);
            bulletUI[1].SetBool("isSpent", true);
            bulletUI[2].SetBool("isSpent", true);
            noAmmo.SetActive(true);
        }
        else if (ammo == 3)
        {
            bulletUI[0].SetBool("isSpent", false);
            bulletUI[1].SetBool("isSpent", false);
            bulletUI[2].SetBool("isSpent", false);
            noAmmo.SetActive(false);
        }
        else if (ammo == 2)
        {
            bulletUI[0].SetBool("isSpent", true);
            bulletUI[1].SetBool("isSpent", false);
            bulletUI[2].SetBool("isSpent", false);
            noAmmo.SetActive(false);
        }
        else if (ammo == 1)
        {
            bulletUI[0].SetBool("isSpent", true);
            bulletUI[1].SetBool("isSpent", true);
            bulletUI[2].SetBool("isSpent", false);
            noAmmo.SetActive(false);
        }


    }


    void Shoot()
    {

        

        if (canFire == true && ammo != 0)
        {
            StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
            gunAnim.SetBool("isShooting", true);
            fireRateTimer -= fireRate;
            canFire = false;
            ammo -= 1;
            gunshotSound.Play();


            RaycastHit hit;
            float distanceOfRay = weaponRange;

            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, distanceOfRay))
            {
                Debug.Log("Raycast hit!");
                if (hit.transform.tag == "Enemy")
                {
                    //hit.transform.gameObject.GetComponent<Enemy>().health -= gunDamage;
                    hit.transform.gameObject.GetComponent<Enemy>().DeathByGun();
                }
                
            }

            Debug.Log("Shot fired!");
        }
        UpdateAmmoText();
    }

    void Reload()
    {
        reloadTimer += Time.deltaTime;
        if (reloadTimer > reloadSpeed)
        {
            Debug.Log("Reload Completed");
            reloadTimer -= reloadSpeed;
            reloading = false;
            ammo = maxAmmo;
            UpdateAmmoText();
            //gunAnim.SetBool("isReloading", false);
        }
        
    }

    public void UpdateAmmoText()
    {
        if (ammo == 0)
        {
            bulletUI[0].SetBool("isSpent", true);
            bulletUI[1].SetBool("isSpent", true);
            bulletUI[2].SetBool("isSpent", true);
            noAmmo.SetActive(true);
        }
        else if (ammo == 3)
        {
            bulletUI[0].SetBool("isSpent", false);
            bulletUI[1].SetBool("isSpent", false);
            bulletUI[2].SetBool("isSpent", false);
            noAmmo.SetActive(false);
        }
        else if (ammo == 2)
        {
            bulletUI[0].SetBool("isSpent", true);
            bulletUI[1].SetBool("isSpent", false);
            bulletUI[2].SetBool("isSpent", false);
            noAmmo.SetActive(false);
        }
        else if (ammo == 1)
        {
            bulletUI[0].SetBool("isSpent", true);
            bulletUI[1].SetBool("isSpent", true);
            bulletUI[2].SetBool("isSpent", false);
            noAmmo.SetActive(false);
        }
        //ammoText.text = ammo + "/" + maxAmmo;
    }

}
