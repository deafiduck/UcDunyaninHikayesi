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
        // getKey bile�enini farkl� bir nesnede ise, bunu ayarlaman�z gerekebilir
        if (getKey == null)
        {
            getKey = FindObjectOfType<GetKey>(); // Bu, sahnedeki herhangi bir GetKey bile�enini bulur
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
