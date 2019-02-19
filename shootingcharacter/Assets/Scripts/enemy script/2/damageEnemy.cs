using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageEnemy : MonoBehaviour
{
    public GameObject Emery;
    public GameObject Ragdoll;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemydeadzone")
        {
            Emery.SetActive(false);
            Ragdoll.SetActive(true);
            Instantiate(Ragdoll, transform.position, transform.rotation);
        }
    }
}
