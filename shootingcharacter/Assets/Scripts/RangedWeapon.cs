using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{

    ///////////////////////////
    public GameObject bullet;

    public Transform shotPoint;

    public GameObject shell;
    public Transform shellPosition;
    public int damage;
    /// ///////////////////////

    public GameObject muzzleFlash;
    public float duration = 0.1f;

    new private AudioSource audio;

    public void Start()
    {
        this.audio = gameObject.GetComponent<AudioSource>();
        
    }

    public void Shoot()
    {
      
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        this.audio.PlayOneShot(audio.clip);
        StartCoroutine(MuzzleFlash());

        bullet.GetComponent<Bullet>().damage = damage;
        AddShell();
    }
    void AddShell()
    {
        GameObject newShell = Instantiate(shell);
        newShell.transform.position = shellPosition.position;
       
        Quaternion rot = shellPosition.rotation;
        newShell.transform.rotation = rot;

        newShell.transform.parent = null;
        newShell.GetComponent<Rigidbody>().AddForce(-newShell.transform.forward * Random.Range(80, 120));
    }

    IEnumerator MuzzleFlash()
    {
        // Set muzzle flash to be visible for a brief moment.
        this.muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(this.duration);
        this.muzzleFlash.SetActive(false);

        // Rotate muzzle flash after it's hidden again.
        this.muzzleFlash.transform.Rotate(0.0f, 0.0f, 270.0f);
    }
}
