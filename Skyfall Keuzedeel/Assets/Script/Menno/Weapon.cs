using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    // Weapon script with raycast //

    [Header("Weapon Options")]
    // Damage dealt by weapon //
    public float damage = 10.0f;

    // Range of weapon
   // public float range = 100f;

    // Firerate of weapon //
    public float fireRate = 15.0f;
    public float nextTimeToFire = 0.0f;

    // Ammo of weapon
    public int maxAmmo = 10;
    public int currentAmmo;

    // Reload time of weapon
    public float reloadTime = 1.0f;
    public bool isReloading = false;

    [Header("Particles")]
    public GameObject muzzleFlash;
    public GameObject impactEffect;

    [Header("Other")]

    // FPS Camera of player //
    public Camera fpsCam;

    // Firepoint of the weapon //
    public GameObject firePoint;

    // Hud //
    public TMP_Text ammoUI;


    public GameObject bulletPrefab;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        //Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
    

    void Update()
    {
        // Ammo UI //
        //ammoUI.text = currentAmmo + " / " + maxAmmo;
    }

    public void Shoot()
    {
        // Ammo //
        currentAmmo --;

        // Muzzle particle is played //
       /* GameObject muzzleFlashOn  = Instantiate(muzzleFlash, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform);
        Destroy(muzzleFlashOn, 0.12f);*/

        Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);

/*
        // Raycast //
        Ray ray = new Ray(firePoint.transform.position, firePoint.transform.TransformDirection(Vector3.forward * range));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            // See in console what is hit //
             Debug.Log(hit.transform.name);

            // Enemy takes damage //
            Enemy3D enemy = hit.transform.GetComponent<Enemy3D>();
            if (enemy != null)
            {
               enemy.TakeDamage(damage);
            }

            Player3D player3D = hit.transform.GetComponent<Player3D>();
            if (player3D != null)
            {
                player3D.TakeDamage(damage);
            }
            

            // Impact particle is played //
            GameObject impactEnemyGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactEnemyGO, 1f); */
        }
    } 

