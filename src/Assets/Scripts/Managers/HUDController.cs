using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // Start is called before the first frame update
    public static HUDController Instance { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI currentMagAmmoCountUI;
    public TextMeshProUGUI totalAmmoCountUI;
    public Image currentAmmoTypeUI;

    [Header("Weapon")]
    public Image currentActiveWeaponUI;
    public Image currentInactiveWeaponUI;

    [Header("Throwables")]
    public Image throwableUI;
    public TextMeshProUGUI throwableCountUI;

    public Image utilityUI;
    public TextMeshProUGUI utilityCountUI;

    public Sprite transparentSprite;
    public Sprite greySlot;

    public GameObject crossHair;

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
        Weapon activeWeapon = WeaponManager.Instance.activeWeapon.GetComponentInChildren<Weapon>();
        Weapon inactiveWeapon = GetInactiveWeaponSlot().GetComponentInChildren<Weapon>();

        if (activeWeapon)
        {
            currentMagAmmoCountUI.text = $"{activeWeapon.bulletsRemaining / activeWeapon.bulletsPerBurst}";
            totalAmmoCountUI.text = $"{WeaponManager.Instance.CheckAmmoRemainingIn(activeWeapon.thisWeaponType)}";

            Weapon.WeaponType model = activeWeapon.thisWeaponType;
            currentAmmoTypeUI.sprite = GetAmmoSprite(model);

            currentActiveWeaponUI.sprite = GetWeaponSprite(model);

            if(inactiveWeapon)
            {
                currentInactiveWeaponUI.sprite = GetWeaponSprite(inactiveWeapon.thisWeaponType);
            }
        }
        else
        {
            currentMagAmmoCountUI.text = "";
            totalAmmoCountUI.text = "";

            currentAmmoTypeUI.sprite = transparentSprite;
            currentActiveWeaponUI.sprite = transparentSprite;
            currentInactiveWeaponUI.sprite = transparentSprite;
        }
    }

    private Sprite GetWeaponSprite(Weapon.WeaponType model)
    {
        return model switch
        {
            Weapon.WeaponType.Pistol1911 => Resources.Load<GameObject>("Pistol1911_Weapon").GetComponent<SpriteRenderer>().sprite,
            //return Resources.Load<Sprite>("Pistol1911_Weapon");
            Weapon.WeaponType.AK_47 => Resources.Load<GameObject>("AK_47_Weapon").GetComponent<SpriteRenderer>().sprite,
            //return Resources.Load<Sprite>("AK_47_Weapon");
            _ => null,
        };
    }

    private Sprite GetAmmoSprite(Weapon.WeaponType model)
    {
        return model switch
        {
            Weapon.WeaponType.Pistol1911 => Resources.Load<GameObject>("Pistol_Ammo").GetComponent<SpriteRenderer>().sprite,
            //return Resources.Load<Sprite>("Pistol_Ammo");
            Weapon.WeaponType.AK_47 => Resources.Load<GameObject>("Rifle_Ammo").GetComponent<SpriteRenderer>().sprite,
            //return Resources.Load<Sprite>("Rifle_Ammo");
            _ => null,
        };
    }

    private GameObject GetInactiveWeaponSlot()
    {
        foreach(GameObject weaponSlot in WeaponManager.Instance.weaponSlots)
        {
            if(weaponSlot != WeaponManager.Instance.activeWeapon)
            {
                return weaponSlot;
            }
        }
        return null;
    }

    internal void UpdateThrowableCountUI()
    {
        throwableCountUI.text = $"{WeaponManager.Instance.lethalCount}";
        utilityCountUI.text = $"{WeaponManager.Instance.utilityCount}";

        switch (WeaponManager.Instance.equippedLethalGrenadeType)
        {
            case Throwables.ThrowableType.HE_Grenade:
                throwableUI.sprite = Resources.Load<GameObject>("Grenade")?.GetComponent<SpriteRenderer>()?.sprite;
                break;
            default:
                throwableUI.sprite = greySlot;
                break;
        }

        switch (WeaponManager.Instance.equippedUtilityGrenadeType)
        {
            case Throwables.ThrowableType.Smoke_Grenade:
                utilityUI.sprite = Resources.Load<GameObject>("Smoke_Grenade")?.GetComponent<SpriteRenderer>()?.sprite;
                break;
            default:
                utilityUI.sprite = greySlot;
                break;
        }
    }
    /*internal void UpdateThrowableCountUI()
    {
        throwableCountUI.text = $"{WeaponManager.Instance.lethalCount}";
        utilityCountUI.text = $"{WeaponManager.Instance.utilityCount}";

        switch (WeaponManager.Instance.equippedLethalGrenadeType)
        {
            case Throwables.ThrowableType.HE_Grenade:
                throwableUI.sprite = Resources.Load<GameObject>("Grenade").GetComponent<SpriteRenderer>().sprite;
                break;
        }

        switch (WeaponManager.Instance.equippedUtilityGrenadeType)
        {
            case Throwables.ThrowableType.Smoke_Grenade:
                utilityUI.sprite = Resources.Load<GameObject>("Smoke_Grenade").GetComponent<SpriteRenderer>().sprite;
                break;
        }
    }*/
}
