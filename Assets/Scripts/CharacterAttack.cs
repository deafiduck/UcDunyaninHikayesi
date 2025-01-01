using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    RaycastHit hit;

     int currentAmmo = 10000;  
     int maxAmmo = 10000;

    [SerializeField]
    float weaponRange; //atýþ mesafesi(ne kadar uzaða atýþ yapabiliriz

    [SerializeField]
    float rateofFire;  
    float nextFire = 0;

    public Transform shootPoint;
    public float damage = 10f;

    private void Update()
    {
        if (currentAmmo > 0 && Input.GetButton("Fire1")) 
        {
            Shoot();
        }
   
    }

    void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + rateofFire;
            currentAmmo--; 

            ShootRay();
        }

    }

    void ShootRay()
    {

        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange)) 
        {
            if (hit.transform.tag == "Enemy")
            {
                GhostHealth enemy = hit.transform.GetComponent<GhostHealth>();
                enemy.ReduceHealth(damage);
            }
  
            else
            {
                Debug.Log("Something else");
            }
        }
    }

}
