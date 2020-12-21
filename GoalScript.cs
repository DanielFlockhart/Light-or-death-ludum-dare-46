using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public LayerMask m_LayerMask;
    public GameManager manager;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        //Check when there is a new collider coming into contact with the box
        foreach (Collider col in hitColliders)
        {
            if (col.tag == "Player"){
                score++;
            }
        }
        if(score == 2)
        {
            manager.GetComponent<GameManager>().winGame();
        }
        score = 0;
    }
}
