using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectScript : MonoBehaviour
{
    public GetKey getKey;
    public ParticleSystem particleEffect;
    public GameObject cylinder;

    void Start()
    {
        if (getKey == null)
        {
            getKey = FindObjectOfType<GetKey>();
        }
    }

    private void OnTriggerEnter(Collider other) // OnCollision yerine OnTrigger kullan (Collider ayarlarýna baðlý)
    {
        if (other.CompareTag("Player") && getKey != null && getKey.keyTaken)
        {
            Debug.Log("carptiiiii");
            particleEffect.Play(); 
            cylinder.SetActive(false);
           
        }
    }
}
