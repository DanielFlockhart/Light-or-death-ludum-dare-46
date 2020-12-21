using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class playerscript : MonoBehaviour
{
    
    public GameObject shield;
    public VisualEffect DamageEfect;
    public GameObject sword;
    public GameObject orb;
    public GameObject laser;
    private Rigidbody cc;
    public GameManager manager;
    public Slider healthBar;
    public Slider sheildbar;
    public Slider powerbar;
    public Slider laserbar;
    public Animator anim;
    public float PlayerHealth = 100f;
    public float speed = 150f;
    public float playTime;
    public float damage = 20f;
    public float RegenTime;
    public float fireRate = 10f;
    private float nextTimeToFire = 1f;
    private float cspeed;
    private float speedpercent;
    public float sword_range;
    private bool isdamaged =false;
    Vector3 mousePos;
    public float regen;
    public float sheildcooldown = 10;
    private float timetosheild;
    public float timetopower;
    public float timetopowers;
    public float timetolaser;
    public float timetolasers;

    public bool isShielding = false;
    public bool isAttacking = false;
    private bool cansheild = false;
    private float stime =2f;

    void Start()
    {
        cc = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //anim.Play("idle", -1);
        //anim.SetBool("isidle", true);
        timetosheild = sheildcooldown;
        timetopowers = manager.GetComponent<ExplosionScript>().timeToExplodable;
        timetolasers = manager.GetComponent<ExplosionScript>().timeToLaser;
    }

    void Update()
    {
        
        timetopower = manager.GetComponent<ExplosionScript>().timeToExplodable;
        timetolaser = manager.GetComponent<ExplosionScript>().timeToLaser;
        powerbar.value = timetopower / timetopowers;
        laserbar.value = timetolaser/ timetolasers;
        if (timetosheild <= 0)
        {
            cansheild = true;
        }
        if (timetosheild > 0)
        {
            cansheild = false;
        }
        if (isShielding)
        {
            stime -= Time.deltaTime;
        }
        if (stime <= 0)
        {
            //cansheild = false;
            isShielding = false;
            shield.GetComponent<MeshRenderer>().enabled = false;
            stime = 2;
        }
        timetosheild -= Time.deltaTime;
        sheildbar.value = timetosheild / 10f;
        //Dammageeffect();
        if (Input.GetKey("r"))
        {
            resetGame();
        }

        if (Input.GetMouseButtonDown(0))//Left
        {
            Debug.Log("pressed");
            if (Time.time >= nextTimeToFire)
            {
                mousePos = Input.mousePosition;
                nextTimeToFire = Time.time + 1f / fireRate;
                Debug.Log("isattacking");
                attack();



                anim.SetBool("isattacking", true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isattacking", false);
        }


        if (Input.GetMouseButtonDown(1))
        {//right
            block();
        }
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Shield Up");
            isShielding = false;
            shield.GetComponent<Renderer>().enabled = false;
        }
        if (Input.GetKey("q"))
        {
            powerMove();
        }
        if (Input.GetKeyDown("e"))
        {
            LaserBeam();
            if (timetolaser <= 0)
            {
                laser.GetComponent<MeshRenderer>().enabled = true;
                anim.SetBool("isshooting", true);
            }
            
        }
        if (Input.GetKeyUp("e"))
        {
            laser.GetComponent<MeshRenderer>().enabled = false;
            anim.SetBool("isshooting", false);
        }

    }

    IEnumerator Dammageeffect()
    {
        if (isdamaged)
        {
            
            isdamaged = false;
        }
        if(isdamaged == false)
        {
            
            
        }
        yield return new WaitForSeconds(0.2f);

    }
    void FixedUpdate()
        
    {
        cspeed = cc.velocity.magnitude;
        Debug.Log(cspeed);
        healthBar.value = PlayerHealth / 550;
        speedpercent = cspeed / 7;
        anim.SetFloat("speedPercent", speedpercent);
        if (PlayerHealth > 550)
        {
            PlayerHealth = 550;
        }
        playTime += Time.deltaTime;
        RegenTime += Time.deltaTime;
        float mH = Input.GetAxis("Horizontal")*-1;
        float mV = Input.GetAxis("Vertical");
        cc.velocity = new Vector3(mH * speed * -1, cc.velocity.y, mV * speed );
        Vector3 currentvelocity = (cc.velocity);
        currentvelocity.y = 0;
        transform.rotation = Quaternion.LookRotation(currentvelocity);
        
        orb.GetComponent<OrbScript>().moveOrb(currentvelocity);
        if (PlayerHealth <= 0)
        {
            manager.GetComponent<GameManager>().end();
        }
        if (RegenTime >= 3)
        {
            PlayerHealth += regen;
            RegenTime = 0;
        }

    }
    public void takeDamage(float damage)
    {
        PlayerHealth -= damage;
        isdamaged = true;
        gameObject.GetComponentInChildren<VisualEffect>().Play();
    }
    void attack()
    {
        
            Debug.Log("fuckreackt");
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);

            }
            float distance = Vector3.Distance(hit.collider.gameObject.transform.position, transform.position);
        if (hit.collider.gameObject.tag == "Enemy" && distance <= sword_range)
        {
            Vector3 direction = hit.collider.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

            //sword.GetComponent<Renderer>().enabled = true;
            if (hit.collider.gameObject.GetComponent<EnemyController>().isAttackable == true)
            {
                if(isShielding == true)
                {

                    hit.collider.gameObject.GetComponent<EnemyController>().takeDamageEnemy(damage / 5f);

                }
                else
                {
                    hit.collider.gameObject.GetComponent<EnemyController>().takeDamageEnemy(damage);

                }
            }
        }
        
    }
    void resetGame()
    {
        manager.reset();
    }
    void block()
    {
        if (cansheild)
        {
            isShielding = true;
            timetosheild = sheildcooldown;
            shield.GetComponent<MeshRenderer>().enabled = true;
            cansheild = false;
        }

        
    }
    void powerMove()
    {
        manager.GetComponent<ExplosionScript>().explode();
    }
    void LaserBeam()
    {
        manager.GetComponent<ExplosionScript>().laser();
        
    }
}
