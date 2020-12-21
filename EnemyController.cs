using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.VFX;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 80f;
    public float rotSpeed = 100f;
    public float moveSpeed = 1f;

    public float telp_maxRad = 20f;
    public float timeto_tel = 8f;
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    public bool isAttackable = false;
    public bool isBoss = false;
    public bool isTeleporter = false;
    public Animator anim;
    public bool isdead = false;
    

    private Transform target;
    private NavMeshAgent agent;
    public GameObject player;
    public GameObject Orb;
    public float damage = 10f;
    public float fireRate = 10f;
    private float nextTimeToFire = 0f;
    public float enemyHealth = 100f;
    public float maxh = 100f;
    private bool attackingOrb = false;
    public Image health;
    // Get nearest either play or orb
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        //anim = gameObject.getComponent<Animation>();
        Wander();
        if (isBoss)
        {
            damage = 25f;
            fireRate = 4f;
            enemyHealth = 400f;
            maxh = 400f;
        }
        

    }
    void Update()
    {
        if (isdead)
        {


            anim.SetBool("isdead", true);
            Destroy(gameObject, 0.5f);
        }
        if (enemyHealth <= 0)
        {
            isdead = true;
            Debug.Log("isdead");



        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        health.fillAmount = enemyHealth / maxh;
        timeto_tel -= Time.deltaTime;
        
        float distToOrb = Vector3.Distance(Orb.transform.position, transform.position);
        float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distToPlayer <= distToOrb)
        {
            target = player.transform;
            attackingOrb = false;
        }
        else
        {
            target = Orb.transform;
            attackingOrb = true;
        }

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            if (isTeleporter && timeto_tel <= 0 )
            {
                // Might wanna add a min range but for the mo this will do
                transform.position = new Vector3(UnityEngine.Random.Range(-telp_maxRad, telp_maxRad), 0, UnityEngine.Random.Range(-telp_maxRad, telp_maxRad));
                timeto_tel = 8f;
                gameObject.GetComponentInChildren<VisualEffect>().Play();
            }
            //anim.Play("EnemyWalk");
            Debug.Log("In Range");
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance +2.5)
            {
                //anim.Play("EnemyAttack");
                isAttackable = true;
                FaceTarget();
                if (attackingOrb == false)
                {

                    if (target != null && Time.time >= nextTimeToFire && target.GetComponent<playerscript>().isShielding == false)
                    {
                        nextTimeToFire = Time.time + 1f / fireRate;
                        target.GetComponent<playerscript>().takeDamage(damage);
                        anim.SetBool("isattacking", true);
                        
                    }

                }
                if (attackingOrb == true)
                {
                    if (target != null && Time.time >= nextTimeToFire)
                    {
                        nextTimeToFire = Time.time + 1f / fireRate;
                        target.GetComponent<OrbScript>().orbDamage(damage);
                    }
                }
            }
            else
            {

                anim.SetBool("isattacking", false);
            }
        }
        else
        {
            isAttackable = false;
        }
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(2, 6);
        int rotLorR = Random.Range(0, 3);
        int walkWait = Random.Range(2, 6);
        float walkTime = Random.Range(0.5f, 1);
        isWandering = true;
        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }
        if (rotLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }
    public void takeDamageEnemy(float damage)
    {
        Debug.Log("IS taking " + damage);
        enemyHealth -= damage;

    }
}

