using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;


    public class Weapon : MonoBehaviour
    {
        public bool isActiveWeapon;
        public int weaponDamage;

        [Header("Shooting")]
        public bool isShooting, readyToShoot, isReloading;
        bool allowReset = true;
        public float shootingDelay = 2f;

        [Header("Burst")]
        public int bulletsPerBurst = 3;
        public int burstRemaining;

        [Header("Spread")]
        //spread intensity
        public float spreadIntensity;
        public float hipSpread;
        public float adsSpread;

        //bullet
        [Header("Bullet")]
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        public float bulletVel = 30;
        public float bulletLifeTime = 3f;

        //reload
        [Header("Reload")]
        public float reloadDuration;
        public int magSize, bulletsRemaining;

        public Vector3 spawnPos;
        public Vector3 spawnRot;

        public GameObject muzzleEffect;
        internal Animator animator;

        public bool isADS;
        //public SwayBobScript swayBob;
        public enum WeaponType
        {
            Pistol1911,
            AK_47,
            M4_8,
            Uzi
        }

        public WeaponType thisWeaponType;

    //shooting mode variants
        public enum ShootingMode
        {
            Single,
            Burst,
            Auto
        }

    //controls shooting mode for individual guns in inspector
        public ShootingMode currentShootingMode;

        private void Awake()
        {
            readyToShoot = true;
            burstRemaining = bulletsPerBurst;
            animator = GetComponent<Animator>();

            bulletsRemaining = magSize;

            spreadIntensity = hipSpread;
        }

        // Update is called once per frame
        void Update()
        {
        if (!PauseMenu.isPaused)
        {
            if (isActiveWeapon)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    StartADS();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    EndADS();
                }


                GetComponent<Outline>().enabled = false;

                

                if (currentShootingMode == ShootingMode.Auto)
                {
                    isShooting = Input.GetKey(KeyCode.Mouse0);
                }
                else if (currentShootingMode == ShootingMode.Single ||
                    currentShootingMode == ShootingMode.Burst)
                {
                    isShooting = Input.GetKeyDown(KeyCode.Mouse0);
                }

                if (Input.GetKeyDown(KeyCode.R) && bulletsRemaining < magSize && !isReloading && WeaponManager.Instance.CheckAmmoRemainingIn(thisWeaponType) > 0)
                {
                    Reload();
                }

                //Automatically reload weapon at 0 bullets remaining
                if (readyToShoot && !isShooting && !isReloading && bulletsRemaining <= 0)
                {
                    //Reload();
                }

                if (readyToShoot && isShooting && bulletsRemaining > 0)
                {
                    burstRemaining = bulletsPerBurst;
                    FireWeapon();
                }

            }
        }
        }
    //change
    //controls animations, ads, recoil, and crosshair activity
    private void StartADS()
        {
            animator.SetTrigger("startADS");
            isADS = true;
            HUDController.Instance.crossHair.SetActive(false);
            spreadIntensity = adsSpread;
        }
    //change
    private void EndADS()
        {
            animator.SetTrigger("endADS");
            isADS = false;
            HUDController.Instance.crossHair.SetActive(true);
            spreadIntensity = hipSpread;
        }
    //change
    private void FireWeapon()
        {
            bulletsRemaining--;

        if (bulletsRemaining == 0 && isShooting)
        {
            SoundManager.Instance.PlayEmptyMagSound(thisWeaponType);
        }

        muzzleEffect.GetComponent<ParticleSystem>().Play();

            if (isADS)
            {
                //ADS RECOIL
                animator.SetTrigger("RECOIL_ADS");

            }
            else
            {   //HIPFIRE RECOIL
                animator.SetTrigger("RECOIL");
            }
            RaycastHit hit;
            float range = 100f;
            if (Physics.Raycast(bulletSpawn.transform.position, bulletSpawn.transform.forward, out hit, range))
            {
                Debug.DrawLine(bulletSpawn.transform.position, hit.point, Color.red, 10f);
            }

            SoundManager.Instance.PlayShootingSound(thisWeaponType);

            readyToShoot = false;

            Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
            //Create/instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            Bullet b = bullet.GetComponent<Bullet>();
            b.damageInflictedByBullet = weaponDamage;

            bullet.transform.forward = shootingDirection;
            //shoot bullet
            bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVel, ForceMode.Impulse);
            //destroy bullet
            StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));

            if (allowReset)
            {
                Invoke("ResetShot", shootingDelay);
                allowReset = false;
            }

            if (currentShootingMode == ShootingMode.Burst && burstRemaining > 1)
            {
                burstRemaining--;
                Invoke("FireWeapon", shootingDelay);
            }
        }

        private void Reload()
        {
            SoundManager.Instance.PlayReloadSound(thisWeaponType);

            animator.SetTrigger("RELOAD");

            isReloading = true;
            Invoke("ReloadCompleted", reloadDuration);
        }

        private void ReloadCompleted()
        {
            if (WeaponManager.Instance.CheckAmmoRemainingIn(thisWeaponType) > magSize)
            {
                bulletsRemaining = magSize;
                WeaponManager.Instance.DecreaseTotalAmmo(bulletsRemaining, thisWeaponType);
            }
            else
            {
                bulletsRemaining = WeaponManager.Instance.CheckAmmoRemainingIn(thisWeaponType);
                WeaponManager.Instance.DecreaseTotalAmmo(bulletsRemaining, thisWeaponType);
            }
            isReloading = false;
        }

        private void ResetShot()
        {
            readyToShoot = true;
            allowReset = true;
        }
    //controls spread of weapons
        public Vector3 CalculateDirectionAndSpread()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(100);
            }

            Vector3 direction = targetPoint - bulletSpawn.position;

            float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
            float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

            return direction + new Vector3(0, y, z);
        }

    //destroys bullet to avoid clutter and loss in performance
        private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(bullet);
        }
    }
