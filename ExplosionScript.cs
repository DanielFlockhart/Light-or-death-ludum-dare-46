using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionScript : MonoBehaviour
{
    public float explosion_rad = 10f;
    public bool isExplodable = true;
    public float timeToExplodable = 25f;
    public float explodeDmg = 80f;
    public float laserDmg = 120f;
    public float laser_dist = 10f;
    public bool isLaser = false;
    public float timeToLaser = 25f;
    public GameObject player;
    public LayerMask lmsk;
    public VisualEffect explosionEffect;
    public GameObject laserobj;
    private float enabledtimed =2f;
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeToExplodable -= Time.deltaTime;
        if (timeToExplodable <= 0)
        {
            isExplodable = true;

        }
        timeToLaser -= Time.deltaTime;
        if (timeToLaser <= 0)
        {
            isLaser = true;
            
        }
        else
        {
            isLaser = false;
            
        }
        if (isLaser == true)
        {
            enabledtimed -= Time.deltaTime;
        }
        if(enabledtimed <=0)
        {
            laserobj.GetComponent<Renderer>().enabled = false;
            enabledtimed = 2f;
        }
    }
    public void explode()
    {
        if (isExplodable == true)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in enemys)
            {
                float distance = Vector3.Distance(obj.transform.position, player.transform.position);
                if (distance <= explosion_rad)
                {
                    Debug.Log("Destroying");
                    obj.GetComponent<EnemyController>().takeDamageEnemy(explodeDmg);
                    obj.GetComponentInChildren<VisualEffect>().Play();

                }
            }
            timeToExplodable = 25f;
            isExplodable = false;
        }

    }
    public void laser()
    {
        if (isLaser == true) // Added this
        {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, lmsk))
            { // Ensure layer mask not avoid raycast
                Debug.Log(hit.collider.name); // Set shield to ignore raycast
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<EnemyController>().takeDamageEnemy(laserDmg);
                }
            }

            timeToLaser = 10f;
            isLaser = false;
        }


    }
}
