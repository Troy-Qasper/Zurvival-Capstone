using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class AmmoBoxPickup : MonoBehaviour
    {
    //changeable ammo amounts for different types of weapons
        public int ammoInBox = 200;
        public AmmoType ammoType;

    //ammo types
        public enum AmmoType
        {
            RifleAmmo,
            PistolAmmo,
            SMGAmmo
        }
    }
