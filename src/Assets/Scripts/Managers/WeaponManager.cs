using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; set; }

    public List<GameObject> weaponSlots;

    public GameObject activeWeapon;

    [Header("Ammo")]
    public int totalRifleAmmo = 0;
    public int totalPistolAmmo = 0;
    public int totalSMGAmmo = 0;

    [Header("General Throwables Settings")]
    public float throwForce = 75f;  
    public GameObject throwableSpawn;
    public float forceMultiplier = 0;
    public float forceMultiplierLimiter = 2f;

    [Header("Lethals")]
    public int lethalCount = 0;
    public int maxLethal = 2;
    public Throwables.ThrowableType equippedLethalGrenadeType;
    public GameObject grenadePrefab;

    [Header("Utility")]
    public int utilityCount = 0;
    public int maxUtility = 2;
    public Throwables.ThrowableType equippedUtilityGrenadeType;
    public GameObject smokeGrenadePrefab;

    

    private void Start()
    {
        activeWeapon = weaponSlots[0];

        equippedLethalGrenadeType = Throwables.ThrowableType.None;
        equippedUtilityGrenadeType = Throwables.ThrowableType.None;
    }

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
        foreach(GameObject weaponSlot in weaponSlots)
        {
            if(weaponSlot == activeWeapon)
            {
                weaponSlot.SetActive(true);
            }
            else
            {
                weaponSlot.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveSlotSwitch(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveSlotSwitch(1);
        }

        if(Input.GetKey(KeyCode.G) || Input.GetKey(KeyCode.T))
        {
            forceMultiplier += Time.deltaTime;

            if(forceMultiplier > forceMultiplierLimiter)
            {
                forceMultiplier = forceMultiplierLimiter;
            }
        }

        if(Input.GetKeyUp(KeyCode.G))
        {
            if(lethalCount > 0)
            {
                ThrowLethal();
            }

            forceMultiplier = 0;
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            if (utilityCount > 0)
            {
                ThrowUtility();
            }

            forceMultiplier = 0;
        }
    }

    
    //sets active weapon and drops current weapon if one is equipped in that slot and sets transform location where old weapon was
    public void PickupWeapon(GameObject weaponPickup)
    {
        CurrentWeaponDrop(weaponPickup);

        weaponPickup.transform.SetParent(activeWeapon.transform, false);

        Weapon weapon = weaponPickup.GetComponent<Weapon>();

        weaponPickup.transform.localPosition = new Vector3(weapon.spawnPos.x, weapon.spawnPos.y, weapon.spawnPos.z);
        weaponPickup.transform.localRotation = Quaternion.Euler(weapon.spawnRot.x, weapon.spawnRot.y, weapon.spawnRot.z);

        weapon.isActiveWeapon = true;

        weapon.animator.enabled = true;
    }

    private void CurrentWeaponDrop(GameObject weaponPickup)
    {
        if(activeWeapon.transform.childCount > 0)
        {
            var weaponToDrop = activeWeapon.transform.GetChild(0).gameObject;

            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false;

            weaponToDrop.transform.SetParent(weaponPickup.transform.parent);
            weaponToDrop.transform.localPosition = weaponPickup.transform.localPosition;
            weaponToDrop.transform.localRotation = weaponPickup.transform.localRotation;
        }
    }

    //controls switching between weapons, can be expanded if more than 2 weapons are desired (may implement later for drawbacks like decreased movement speed).
    public void ActiveSlotSwitch(int slotNumber)
    {
        if(activeWeapon.transform.childCount > 0)
        {
            Weapon currentWeapon = activeWeapon.transform.GetChild(0).GetComponent<Weapon>();
            currentWeapon.isActiveWeapon = false;
        }

        activeWeapon = weaponSlots[slotNumber];

        if(activeWeapon.transform.childCount > 0)
        {
            Weapon newWeapon = activeWeapon.transform.GetChild(0).GetComponent<Weapon>();
            newWeapon.isActiveWeapon = true;
        }
    }

    //comment
    internal void PickupAmmoSpawn(AmmoBoxPickup ammo)
    {
        switch (ammo.ammoType)
        {
            case AmmoBoxPickup.AmmoType.PistolAmmo:
                totalPistolAmmo += ammo.ammoInBox;
                Destroy(ammo.gameObject);
                break;
            case AmmoBoxPickup.AmmoType.RifleAmmo:
                totalRifleAmmo += ammo.ammoInBox;
                Destroy(ammo.gameObject);
                break;
            case AmmoBoxPickup.AmmoType.SMGAmmo:
                totalSMGAmmo += ammo.ammoInBox;
                Destroy(ammo.gameObject);
                break;
        }
    }

    //comment
    internal void DecreaseTotalAmmo(int bulletsRemoved, Weapon.WeaponType thisWeaponType)
    {
        switch (thisWeaponType)
        {
            case Weapon.WeaponType.Pistol1911:
                totalPistolAmmo -= bulletsRemoved;
                break;
            case Weapon.WeaponType.AK_47:
                totalRifleAmmo -= bulletsRemoved;
                break;
            case Weapon.WeaponType.M4_8:
                totalRifleAmmo -= bulletsRemoved;
                break;
            case Weapon.WeaponType.Uzi:
                totalSMGAmmo -= bulletsRemoved;
                break;
        }
    }


    public int CheckAmmoRemainingIn(Weapon.WeaponType thisWeaponType)
    {
        switch (thisWeaponType)
        {
            case Weapon.WeaponType.AK_47:
                return Instance.totalRifleAmmo;
            case Weapon.WeaponType.Pistol1911:
                return Instance.totalPistolAmmo;
            case Weapon.WeaponType.M4_8:
                return Instance.totalRifleAmmo;
            case Weapon.WeaponType.Uzi:
                return Instance.totalSMGAmmo;
            default:
                return 0;
        }
    }
    #region || ---- Throwables ---- ||
    public void PickupThrowable(Throwables throwables)
    {
        switch (throwables.throwableType)
        {
            case Throwables.ThrowableType.HE_Grenade:
                PickupThrowableAsLethal(Throwables.ThrowableType.HE_Grenade);
                break;
            case Throwables.ThrowableType.Smoke_Grenade:
                PickupThrowableAsUtility(Throwables.ThrowableType.Smoke_Grenade);
                break;
        }
    }

    private void PickupThrowableAsUtility(Throwables.ThrowableType utility)
    {
        if (equippedUtilityGrenadeType == utility || equippedUtilityGrenadeType == Throwables.ThrowableType.None)
        {
            equippedUtilityGrenadeType = utility;

            if (utilityCount < maxUtility)
            {
                utilityCount += 1;
                Destroy(InteractionManager.Instance.hoveredThrowable.gameObject);
                HUDController.Instance.UpdateThrowableCountUI();
            }
            else
            {
                print("Utility limit reached");
            }
        }
    }

    private void PickupThrowableAsLethal(Throwables.ThrowableType lethal)
    {
        if(equippedLethalGrenadeType == lethal || equippedLethalGrenadeType == Throwables.ThrowableType.None)
        {
            equippedLethalGrenadeType = lethal;

            if(lethalCount < maxLethal)
            {
                lethalCount += 1;
                Destroy(InteractionManager.Instance.hoveredThrowable.gameObject);
                HUDController.Instance.UpdateThrowableCountUI();
            }
            else
            {
                print("Lethal limit reached");
            }
        }
    }
    private void ThrowLethal()
    {
        GameObject lethalPrefab = GetPrefabOfThrowable(equippedLethalGrenadeType);

        GameObject throwable = Instantiate(lethalPrefab, throwableSpawn.transform.position, Camera.main.transform.rotation);
        Rigidbody rb = throwable.GetComponent<Rigidbody>();

        rb.AddForce(Camera.main.transform.forward * (throwForce * forceMultiplier), ForceMode.Impulse);

        throwable.GetComponent<Throwables>().isThrown = true;

        lethalCount -= 1;

        if(lethalCount <= 0)
        {
            equippedLethalGrenadeType = Throwables.ThrowableType.None;
        }

        HUDController.Instance.UpdateThrowableCountUI();
    }

    private void ThrowUtility()
    {
        GameObject utilityPrefab = GetPrefabOfThrowable(equippedUtilityGrenadeType);

        GameObject throwable = Instantiate(utilityPrefab, throwableSpawn.transform.position, Camera.main.transform.rotation);
        Rigidbody rb = throwable.GetComponent<Rigidbody>();

        rb.AddForce(Camera.main.transform.forward * (throwForce * forceMultiplier), ForceMode.Impulse);

        throwable.GetComponent<Throwables>().isThrown = true;

        utilityCount -= 1;

        if (utilityCount <= 0)
        {
            equippedUtilityGrenadeType = Throwables.ThrowableType.None;
        }

        HUDController.Instance.UpdateThrowableCountUI();
    }

    private GameObject GetPrefabOfThrowable(Throwables.ThrowableType throwableType)
    {
        switch (throwableType)
        {
            case Throwables.ThrowableType.HE_Grenade:
                return grenadePrefab;
            case Throwables.ThrowableType.Smoke_Grenade:
                return smokeGrenadePrefab;
        }
        return new();
    }

    #endregion
}
