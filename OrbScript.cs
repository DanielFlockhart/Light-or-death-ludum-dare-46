using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class OrbScript : MonoBehaviour
{
    private float speed = 5f;
    public float OrbHealth = 20f;
    public float MaxRadius = 30f;
    public float MinRadius = 10f;
    public Image health;
    public GameManager manager;
    private Transform target;
    private NavMeshAgent agent;
    Rigidbody oc;
    public GameObject player;
    public float orbspeed = 2f;
    public Vector3 temp;
    
    void Start()
    {
        target = player.transform;
        oc = GetComponent<Rigidbody>();

    }
    
    void Update()
    {
        health.fillAmount = OrbHealth / 550;
        if(OrbHealth <= 0)
        {
            manager.GetComponent<GameManager>().end();
        }
        temp = player.transform.position - transform.position;
        
        var lookRotation = Quaternion.LookRotation(temp);
        transform.rotation = lookRotation;
        
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .02f);

    }
    public void orbDamage(float damage)
    {
        OrbHealth -= damage;
    }
    public void moveOrb(Vector3 movement)
    {
        oc.AddForce(transform.forward * orbspeed);
    }
}
