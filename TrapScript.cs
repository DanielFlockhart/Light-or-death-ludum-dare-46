using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public float mass;
    public Rigidbody rb;
    public float delta = 1.5f;
    public float speed = 2.0f;
    public float direction = 1;
    public float damage = 25f;
    private Quaternion startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion a = startPos;
        a.x += direction * (delta * Mathf.Sin(Time.time * speed));
        transform.rotation = a;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "player") {
            other.GetComponent<playerscript>().takeDamage(damage);
        }
        if(other.name == "orb"){
            other.GetComponent<OrbScript>().orbDamage(damage);
        }
    }
}
