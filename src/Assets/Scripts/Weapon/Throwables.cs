using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Throwables : MonoBehaviour
    {
        [SerializeField] float delay = 3f;
        [SerializeField] float damageRadius = 20f;
        [SerializeField] float explosionForce = 1200f;

        float countdown;

        bool hasExploded;
        public bool isThrown = false;

        public enum ThrowableType
        {
            None,
            HE_Grenade,
            Smoke_Grenade
        }
        public ThrowableType throwableType;

        private void Start()
        {
            countdown = delay;
        }

        private void Update()
        {
            if (isThrown)
            {
                countdown -= Time.deltaTime;
                if (countdown <= 0f && !hasExploded)
                {
                    Explode();
                    hasExploded = true;
                }
            }
        }

        private void Explode()
        {
            GetThrowableEffect();

            Destroy(gameObject);
        }

        private void GetThrowableEffect()
        {
            switch (throwableType)
            {
                case ThrowableType.HE_Grenade:
                    GrenadeEffect();
                    break;
                case ThrowableType.Smoke_Grenade:
                    SmokeGrenadeEffect();
                    break;
            }
        }

        private void SmokeGrenadeEffect()
        {
            //Visual effect of grenade
            GameObject smokeEffect = GlobalRefs.Instance.smokeGrenadeEffect;
            Instantiate(smokeEffect, transform.position, transform.rotation);

        //Play Sound
        SoundManager.Instance.PlaySFX(SoundManager.Instance.heGrenadeSound);

        //Physical effect of grenade
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
            foreach (Collider objectInRange in colliders)
            {
                Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
                if (rb != null)
                {

                }
            }
        }

        private void GrenadeEffect()
        {
            //Visual effect of grenade
            GameObject explosionEffect = GlobalRefs.Instance.grenadeExplosionEffect;
            Instantiate(explosionEffect, transform.position, transform.rotation);

            //Play Sound
            SoundManager.Instance.PlaySFX(SoundManager.Instance.heGrenadeSound);

            //Physical effect of grenade
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
            foreach (Collider objectInRange in colliders)
            {
                Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, damageRadius);
                }
                if (objectInRange.gameObject.GetComponent<Enemy>())
                {
                    objectInRange.gameObject.GetComponent<Enemy>().DealDamageToEnemy(100);
                }
            }
        }
    }
