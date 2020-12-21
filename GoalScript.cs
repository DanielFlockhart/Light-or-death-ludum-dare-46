using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public LayerMask m_LayerMask;
    public GameManager manager;
    public int score = 0;
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
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
