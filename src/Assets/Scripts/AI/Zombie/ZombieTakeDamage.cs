using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTakeDamage : MonoBehaviour
{
    public enum collisionType { head, body }
    public collisionType damageType;

    public Enemy enemy;
    public void ZombieHit(int value)
    {
        /*try
        {
            enemy.HP -= value;
            if(enemy.HP <= 0)
            {
                enemy.Die();
            }
        }
        catch
        {
            print("enemy is not available");
        }*/
    }
}
