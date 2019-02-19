using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    Animator anim;
    public Rigidbody[] rigid;
    public int health;

    public GameObject Player;  
    public float dist;
    public float radius=5;
    NavMeshAgent nav;

    void Start()
    {

        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(Player.transform.position, transform.position);
        if (dist > radius)
        {

            gameObject.GetComponent<Animator>().SetTrigger("idle");
        }
        

        if (dist <radius)
        {
 
            nav.SetDestination(Player.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("walk");
        }
       
      
    }

    public void TakeAwayHealth(int TakeAway)
    {
        health -= TakeAway;

        if (health <= 0)
        {
            Dead();

            gameObject.GetComponent<enemyAI> ().enabled = false;
        }
    }
    public void Dead()
    {
        foreach (Rigidbody rb in rigid)
        {
            rb.isKinematic = false;

        }
        anim.enabled = false;
    }
}
