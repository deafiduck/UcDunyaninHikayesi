using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectScript : MonoBehaviour
{
    public GetKey getKey;
    public ParticleSystem particleEffect;
    public GameObject cylinder;
    public static int completedCylinders = 0;  // T�m silindirler i�in ortak saya�
    public ParticleSystem portal;  

    private bool isActivated = false;  // Tekrar tetiklenmeyi �nlemek i�in
    private void Start()
    {
        if (getKey == null)
        {
            getKey = FindObjectOfType<GetKey>();
        }
        if (portal != null)
        {
            portal.Stop();  // Ba�lang��ta portal kapal�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && getKey != null && getKey.keyTaken && !isActivated)
        {
            particleEffect.Play();  
            cylinder.SetActive(false);  
            isActivated = true;
            completedCylinders++;

            if (completedCylinders >= 4)  // T�m silindirler tamamland�ysa
            {
                ActivatePortal();
            }
        }
    }

    void ActivatePortal()
    {
        if (portal != null)
        {
            portal.Play();  // Portal� etkinle�tir
            Debug.Log("Portal A��ld�!");
        }
    }
}
