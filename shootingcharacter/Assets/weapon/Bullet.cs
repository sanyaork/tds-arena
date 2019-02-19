using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Speed;
    Vector3 lastPos;
    public GameObject decal;

    public GameObject metalHitEffect;
    public GameObject sandHitEffect;
    public GameObject stoneHitEffect;
    public GameObject[] meatHitEffect;
    public GameObject woodHitEffect;
    public int damage;
    
    void Start()
    {
        lastPos = transform.position;
        Destroy(gameObject, 10);
    }

   
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        RaycastHit hit;

        Debug.DrawLine(lastPos, transform.position);

        if (Physics.Linecast(lastPos, transform.position, out hit))
        {
            if (hit.collider.sharedMaterial != null)
            {
                string materialName = hit.collider.sharedMaterial.name;
                switch (materialName)
                {
                    case "Metal":
                        SpawnDecal(hit, metalHitEffect);

                        break;
                    case "Sand":
                        SpawnDecal(hit, sandHitEffect);
                        break;
                    case "Stone":
                        SpawnDecal(hit, stoneHitEffect);
                        break;
                    case "Wood":
                        SpawnDecal(hit, woodHitEffect);
                        break;
                    case "Meat":
                        Meat(hit);
                        SpawnDecal(hit, meatHitEffect[Random.Range(0, meatHitEffect.Length)]);
                        break;

                }
            }
                Destroy(gameObject);           
        }
        lastPos = transform.position;
    }
    public void Meat(RaycastHit hit)
    {
        if (hit.transform.GetComponent<HitPos>() != null)
        {
            hit.transform.GetComponent<HitPos>().Damage(damage);
        }
    }

    void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnDecal.transform.SetParent(hit.collider.transform);
        spawnDecal.transform.position = hit.point + hit.normal * 0.01f;
        spawnDecal.transform.rotation = Quaternion.LookRotation(hit.normal); 
        Destroy(spawnDecal.gameObject, 10);
    }
}
