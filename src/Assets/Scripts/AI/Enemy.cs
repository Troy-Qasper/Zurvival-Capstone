using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int HP = 100;
    private Animator animator;

    private NavMeshAgent navAgent;
    //public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    [SerializeField]
    private string currentState;
    //private StateMachine stateMachine;
    public Pathing pathing;
    private GameObject player;
    /*[Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;
    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;*/
    public bool isDead;

    public enum collisionType { head, body}

    public collisionType damageType;

    private void Start()
    {
        /*stateMachine= GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialized();
        player = GameObject.FindGameObjectWithTag("Player");*/
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
/*        if(navAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }*/
        //CanSeePlayer();
        //currentState = stateMachine.activeState.ToString();
    }

    public void DealDamageToEnemy(int damageInflicted)
    {
        HP -= damageInflicted;

        if (HP <= 0)
        {
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0)
            {
                animator.SetTrigger("DEATH_1");
            }
            else
            {
                animator.SetTrigger("DEATH_2");
            }
            isDead = true;
            //SoundManager.Instance.zombieChannel.PlayOneShot(SoundManager.Instance.zombieDeathSound);
            SoundManager.Instance.PlaySFX(SoundManager.Instance.zombieDeathSound);
            StartCoroutine(DestroyZombie(gameObject));
        }
        else
        {
            animator.SetTrigger("DAMAGE");

            SoundManager.Instance.PlaySFX(SoundManager.Instance.zombieHurtSound);
        }
    }

    //Gizmos for radius on attack, patrol, chase range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f);
    }

    /*public bool CanSeePlayer()
    {
        if(player != null)
        {
            //is player close enough to enemy
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDir = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDir, transform.forward);
                if(angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDir);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out  hitInfo, sightDistance))
                    {
                        Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                        return true;
                    }                  
                }
            }
        }
        return false;
    }*/
    /*public void Die()
    {

    }*/
    private IEnumerator DestroyZombie(GameObject gameObject)
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
