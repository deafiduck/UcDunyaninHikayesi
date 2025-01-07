using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectScript : MonoBehaviour
{
    public GetKey getKey;
    public ParticleSystem particleEffect;
    public GameObject cylinder;
    public static int completedCylinders = 0;  // Tüm silindirler için ortak sayaç
    public ParticleSystem portal;  

    private bool isActivated = false;  // Tekrar tetiklenmeyi önlemek için
    private void Start()
    {
        if (getKey == null)
        {
            getKey = FindObjectOfType<GetKey>();
        }
        if (portal != null)
        {
            portal.Stop();  // Baþlangýçta portal kapalý
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

            if (completedCylinders >= 4)  // Tüm silindirler tamamlandýysa
            {
                ActivatePortal();
            }
        }
    }

    void ActivatePortal()
    {
        if (portal != null)
        {
            portal.Play();  // Portalý etkinleþtir
            Debug.Log("Portal Açýldý!");
        }
    }
}
