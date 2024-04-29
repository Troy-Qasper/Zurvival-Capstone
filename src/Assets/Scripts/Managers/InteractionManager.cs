using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBoxPickup hoveredAmmo = null;
    public Throwables hoveredThrowable = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        //Gun
        if(Physics.Raycast(ray, out hit))
        {
            
            GameObject objectHitByRaycast = hit.transform.gameObject;
            
            if (objectHitByRaycast.GetComponent<Weapon>() && objectHitByRaycast.GetComponent<Weapon>().isActiveWeapon == false)
            {
                if(hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = true;
                }
                hoveredWeapon = objectHitByRaycast.gameObject.GetComponent<Weapon>();
                hoveredWeapon.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject); 
                }
            }
            else
            {
                if (hoveredWeapon)
                {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }
            }
            //Ammo
            if (objectHitByRaycast.GetComponent<AmmoBoxPickup>())
            {
                if (hoveredAmmo)
                {
                    hoveredAmmo.GetComponent<Outline>().enabled = true;
                }
                hoveredAmmo = objectHitByRaycast.gameObject.GetComponent<AmmoBoxPickup>();
                hoveredAmmo.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupAmmoSpawn(hoveredAmmo);
                }
            }
            else
            {
                if (hoveredAmmo)
                {
                    hoveredAmmo.GetComponent<Outline>().enabled = false;
                }
            }
            if (objectHitByRaycast.GetComponent<Throwables>())
            {
                if (hoveredThrowable)
                {
                    hoveredThrowable.GetComponent<Outline>().enabled = true;
                }
                hoveredThrowable = objectHitByRaycast.gameObject.GetComponent<Throwables>();
                hoveredThrowable.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupThrowable(hoveredThrowable);
                }
            }
            else
            {
                if (hoveredThrowable)
                {
                    hoveredThrowable.GetComponent<Outline>().enabled = false;
                }
            }
        }
        else
        {
            if(hoveredWeapon)
                hoveredWeapon.GetComponent<Outline>().enabled = false;
            if(hoveredAmmo)
                hoveredAmmo.GetComponent <Outline>().enabled = false;
            if(hoveredThrowable)
                hoveredThrowable.GetComponent <Outline>().enabled = false;
        }
    }
}
