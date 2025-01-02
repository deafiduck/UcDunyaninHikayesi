using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectScript : MonoBehaviour
{
    public GetKey getKey; 
    public GameObject particleEffect;
    public GameObject cylinder;

    void Start()
    {
        // getKey bileþenini farklý bir nesnede ise, bunu ayarlamanýz gerekebilir
        if (getKey == null)
        {
            getKey = FindObjectOfType<GetKey>(); // Bu, sahnedeki herhangi bir GetKey bileþenini bulur
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character") && getKey != null && getKey.keyTaken)
        {
            particleEffect.SetActive(true);
            cylinder.SetActive(false);
        }
    }
}
