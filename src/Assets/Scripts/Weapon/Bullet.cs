using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Bullet : MonoBehaviour
    {
    //gets weapon damage value from Weapons script
        public int damageInflictedByBullet;

    //checks if the bullet collides with a tagged object
        private void OnCollisionEnter(Collision collision)
        {
            Transform hitTransform = collision.transform;
            if (hitTransform.CompareTag("Target"))
            {
                //print("hit " + collision.gameObject.name + " !");

                CreateBulletImpact(collision);

                Destroy(gameObject);
            }
            if (hitTransform.CompareTag("Wall"))
            {
                //print("hit a wall");

                CreateBulletImpact(collision);

                Destroy(gameObject);
            }
            if (hitTransform.CompareTag("Bottle"))
            {
                print("hit a bottle");
                hitTransform.GetComponent<BottleTarget>().Shatter();
            }
            if (hitTransform.CompareTag("Enemy"))
            {
                Debug.Log("Hit Enemy");
                Enemy enemy = hitTransform.GetComponent<Enemy>();
                if (collision.gameObject.GetComponent<Enemy>().isDead == false)
                {
                    enemy.DealDamageToEnemy(damageInflictedByBullet);
                }

                CreateBloodSprayEffect(collision);
                //enemy.gunBarrel.SetParent(collision.gameObject.transform);
                Destroy(gameObject);
            }
        /*if (hitTransform.CompareTag("ZombieBody"))
        {
            Enemy enemy = hitTransform.GetComponent<Enemy>();
            if (collision.gameObject.GetComponent<Enemy>().isDead == false)
            {
                Debug.Log("body shot");
                enemy.DealDamageToEnemy(damageInflictedByBullet);

            }
            CreateBloodSprayEffect(collision);
            //enemy.gunBarrel.SetParent(collision.gameObject.transform);
            Destroy(gameObject);

        }
        if (hitTransform.CompareTag("ZombieHead"))
        {
            Enemy enemy = hitTransform.GetComponent<Enemy>();
            if (collision.gameObject.GetComponent<Enemy>().isDead == false)
            {
                Debug.Log("Headshot");
                enemy.DealDamageToEnemy(damageInflictedByBullet * 2);
            }
            CreateBloodSprayEffect(collision);
            //enemy.gunBarrel.SetParent(collision.gameObject.transform);
            Destroy(gameObject);

        }*/
    }

    //bullet collision visuals
        private void CreateBloodSprayEffect(Collision objectHit)
        {
            ContactPoint contact = objectHit.contacts[0];

            GameObject zombieBloodSprayPrefab = Instantiate(
                GlobalRefs.Instance.zombieBloodSpray,
                contact.point,
                Quaternion.LookRotation(contact.normal));

            zombieBloodSprayPrefab.transform.SetParent(objectHit.gameObject.transform);
        }

        void CreateBulletImpact(Collision objectHit)
        {
            ContactPoint contact = objectHit.contacts[0];

            GameObject hole = Instantiate(
                GlobalRefs.Instance.bulletImpactEffect,
                contact.point,
                Quaternion.LookRotation(contact.normal));

            hole.transform.SetParent(objectHit.gameObject.transform);
        }
    }
