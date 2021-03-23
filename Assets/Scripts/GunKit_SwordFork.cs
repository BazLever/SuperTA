using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunKit_SwordFork : MonoBehaviour
{
    //This fork of the Gun Kit essentially rips out the need for ammo. It's fairly basic, and will probably break if you try to make it like a gun.

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

    int ammo;
    float fireRateTimer;
    float reloadTimer;

    bool canFire;
    bool reloading;

    public Animator gunAnim;

    //Sound stuff
    public AudioSource gunshotSound;
    public AudioSource goreHit;

    float ranNum;

    public CameraShake cameraShake;

    public GameManager gameManager;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canFire = false;
        fireRateTimer = fireRate;

        ammo = maxAmmo;
    }


    void Update()
    {
        //Shooting Code
        {
            if (canFire == false)
            {
                fireRateTimer += Time.deltaTime;
                if (fireRateTimer > fireRate)
                {
                    gunAnim.SetBool("New Bool", false);
                    gunAnim.SetBool("SwordHit", false);

                    canFire = true;

                }
            }


            if (fireMode == 0)
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();
                }
            }
            else if (fireMode == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
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
                        Debug.Log("Reload Begun");
                        reloading = true;
                    }
                }

                if (reloading == true)
                {
                    Reload();
                }
            }
        }

        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * weaponRange);

    }


    void Shoot()
    {
        if (canFire == true) //&& ammo != 0)
        {
            
            fireRateTimer -= fireRate;
            canFire = false;
            //ammo -= 1;
            ranNum = Random.Range(0.7f, 1.0f);
            gunshotSound.pitch = ranNum;

            gunshotSound.Play();


            RaycastHit hit;
            float distanceOfRay = weaponRange;

            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, distanceOfRay))
            {
                Debug.Log("Raycast hit!");
                if (hit.transform.tag == "Enemy")
                {
                    //hit.transform.gameObject.GetComponent<Enemy>().health -= gunDamage;
                    hit.transform.gameObject.GetComponent<Enemy>().DeathBySword();
                    gunAnim.SetBool("SwordHit", true);
                    goreHit.Play();
                    StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
                }
                

            } else
            {
                gunAnim.SetBool("New Bool", true);
            }

            Debug.Log("Shot fired!");
        }
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
        }

    }
}
