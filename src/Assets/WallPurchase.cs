using UnityEngine;
using System;

using static AmmoBoxPickup;
using static Weapon;

public class WallPurchase : MonoBehaviour
{
    public PurchaseOption[] purchaseOptions;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (PurchaseOption option in purchaseOptions)
            {
                if (option.CanPurchase())
                {
                    option.MakePurchase();
                    break; // Exit the loop after the first successful purchase
                }
            }
        }
    }
}

[System.Serializable]
public class PurchaseOption
{
    public string name;
    public int cost;
    public PurchaseType type;
    public WeaponType weaponType;
    public AmmoType ammoType;

    public bool CanPurchase()
    {
        return (GlobalRefs.Instance.playerPoints >= cost) && (type == PurchaseType.Weapon || type == PurchaseType.Ammo);
    }

    public void MakePurchase()
    {
        if (type == PurchaseType.Weapon)
        {
            GlobalRefs.Instance.playerPoints -= cost;
            GameObject weaponPrefab = GetWeaponPrefab(weaponType);
            UnityEngine.Object.Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity);
        }
        else if (type == PurchaseType.Ammo)
        {
            GlobalRefs.Instance.playerPoints -= cost;
            //WeaponManager.Instance.AddAmmo(ammoType, 10); // Adjust the amount as needed
        }

        // Update the HUD to reflect the changes in player points and inventory
        //HUDController.Instance.UpdatePointsUI(GlobalRefs.playerPoints);
        //HUDController.Instance.UpdateAmmoUI();
    }

    private GameObject GetWeaponPrefab(WeaponType weaponType)
    {
        // Implement logic to return the prefab of the specified weapon type
        // For example:
        switch (weaponType)
        {
            case WeaponType.Pistol1911:
                return Resources.Load<GameObject>("M1911");
            case WeaponType.AK_47:
                return Resources.Load<GameObject>("AK47");
            // Add cases for other weapon types as needed
            default:
                return null;
        }
    }
}

public enum PurchaseType
{
    Weapon,
    Ammo
}